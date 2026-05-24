using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal;
using PlanoriaCapstone.Dal.Interface;
using PlanoriaCapstone.Models;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;

using System.Text;

namespace PlanoriaCapstone.Bll.Service
{
    public class ArchivoService : IArchivoService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IArchivoRepository _archivoRepository;
        private readonly IIAService _iaService;
        private readonly AppDbContext _context;
        private readonly ILogger<ArchivoService> _logger;

        public ArchivoService(
            IWebHostEnvironment environment,
            IArchivoRepository archivoRepository,
            IIAService iaService,
            AppDbContext context,
            ILogger<ArchivoService> logger)
        {
            _environment = environment;
            _archivoRepository = archivoRepository;
            _iaService = iaService;
            _context = context;
            _logger = logger;
        }

        public async Task<ArchivoSubido> SubirArchivoAsync(int idUsuario, IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                throw new Exception("Archivo inválido");

            var extension = Path.GetExtension(archivo.FileName).ToLower();

            var carpeta = Path.Combine(_environment.WebRootPath, "assets", "uploads");
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            var nombreUnico = Guid.NewGuid() + extension;
            var ruta = Path.Combine(carpeta, nombreUnico);

            using (var stream = new FileStream(ruta, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            // 1. EXTRAER TEXTO
            string texto = string.Empty;
            if (extension == ".txt")
            {
                texto = await File.ReadAllTextAsync(ruta);
            }
            else if (extension == ".pdf")
            {
                var sb = new StringBuilder();
                try
                {
                    using (var pdfReader = new PdfReader(ruta))
                    using (var pdfDocument = new PdfDocument(pdfReader))
                    {
                        for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
                        {
                            var page = pdfDocument.GetPage(i);
                            sb.AppendLine(PdfTextExtractor.GetTextFromPage(page));
                        }
                    }
                    texto = sb.ToString();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al extraer texto del PDF {FileName}", archivo.FileName);
                    throw new Exception($"No se pudo extraer texto del PDF. Asegúrate de que no sea un PDF escaneado o protegido.", ex);
                }
            }
            else
            {
                throw new Exception("Formato de archivo no soportado. Por favor sube un archivo .txt o .pdf.");
            }

            // 2. IA GEMINI
            const int MaxCaracteres = 12000;
            if (texto.Length > MaxCaracteres)
            {
                _logger.LogWarning("Texto del archivo truncado de {Original} a {Max} caracteres para Gemini.", texto.Length, MaxCaracteres);
                texto = texto.Substring(0, MaxCaracteres);
            }

            var analisis = await _iaService.AnalizarTextoAsync(texto);

            // 3. GUARDAR ARCHIVO EN TRANSACCIÓN
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var nuevoArchivo = new ArchivoSubido
                {
                    IdUsuario = idUsuario,
                    NombreArchivo = archivo.FileName,
                    UrlArchivo = $"/assets/uploads/{nombreUnico}",
                    TipoArchivo = extension,
                    TamanoMB = Math.Round((decimal)archivo.Length / 1024 / 1024, 2),
                    FechaSubida = DateTime.UtcNow,
                    Estado = "PROCESADO"
                };

                await _archivoRepository.CrearAsync(nuevoArchivo);
                await _archivoRepository.GuardarCambiosAsync();

                // 4. GUARDAR ANALISISIA
                var analisisEntity = new AnalisisIA
                {
                    IdArchivo = nuevoArchivo.IdArchivo,
                    Resumen = Truncate(analisis.Resumen, 1000),
                    TemasDetectados = Truncate(string.Join(",", analisis.TemasDetectados ?? new List<string>()), 500),
                    EstadoProceso = "COMPLETADO",
                    FechaAnalisis = DateTime.UtcNow
                };

                _context.AnalisisIA.Add(analisisEntity);
                await _context.SaveChangesAsync();

                // 5. FLASHCARDS
                if (analisis.Flashcards != null)
                {
                    foreach (var fc in analisis.Flashcards)
                    {
                        _context.Flashcards.Add(new Flashcard
                        {
                            IdAnalisis = analisisEntity.IdAnalisis,
                            Pregunta = Truncate(fc.Pregunta, 500),
                            Respuesta = Truncate(fc.Respuesta, 1000),
                            NivelDificultad = "MEDIO",
                            VecesEstudiada = 0,
                            FechaCreacion = DateTime.UtcNow
                        });
                    }
                }

                // 6. QUIZ
                var quiz = new Quiz
                {
                    IdAnalisis = analisisEntity.IdAnalisis,
                    Titulo = "Quiz generado por IA",
                    Descripcion = "Evaluación automática",
                    FechaCreacion = DateTime.UtcNow
                };

                _context.Quizzes.Add(quiz);
                await _context.SaveChangesAsync();

                if (analisis.Quizzes != null)
                {
                    foreach (var q in analisis.Quizzes)
                    {
                        _context.PreguntasQuiz.Add(new PreguntaQuiz
                        {
                            IdQuiz = quiz.IdQuiz,
                            Pregunta = Truncate(q.Pregunta, 500),
                            OpcionA = Truncate(q.Opciones?.ElementAtOrDefault(0), 300),
                            OpcionB = Truncate(q.Opciones?.ElementAtOrDefault(1), 300),
                            OpcionC = Truncate(q.Opciones?.ElementAtOrDefault(2), 300),
                            OpcionD = Truncate(q.Opciones?.ElementAtOrDefault(3), 300),
                            RespuestaCorrecta = Truncate(q.RespuestaCorrecta, 50),
                            Explicacion = Truncate(q.Explicacion, 1000)
                        });
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return nuevoArchivo;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                if (File.Exists(ruta))
                {
                    File.Delete(ruta);
                }
                throw new Exception("Error al guardar el análisis en la base de datos.", ex);
            }
        }

        public async Task<IEnumerable<ArchivoSubido>> ObtenerArchivosUsuarioAsync(int idUsuario)
            => await _archivoRepository.ObtenerPorUsuarioAsync(idUsuario);

        public async Task<ArchivoSubido?> ObtenerArchivoPorIdAsync(int idArchivo)
            => await _archivoRepository.ObtenerPorIdAsync(idArchivo);

        public async Task<bool> EliminarArchivoAsync(int idArchivo, int idUsuario)
        {
            var archivo = await _archivoRepository.ObtenerPorIdAsync(idArchivo);

            if (archivo == null) return false;

            if (archivo.IdUsuario != idUsuario)
                throw new UnauthorizedAccessException("No tienes permiso para eliminar este archivo.");

            archivo.Estado = "ELIMINADO";
            await _archivoRepository.ActualizarAsync(archivo);
            await _archivoRepository.GuardarCambiosAsync();

            return true;
        }

        private string Truncate(string? value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}
