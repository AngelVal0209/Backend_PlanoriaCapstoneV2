using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal;
<<<<<<< HEAD
using PlanoriaCapstone.Dal.Interface;
=======
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864
using PlanoriaCapstone.DTOs.Flashcard;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Bll.Service
{
    public class FlashcardService
         : IFlashcardService
    {
<<<<<<< HEAD
        private readonly IFlashcardRepository _repository;
        private readonly AppDbContext _context;

        public FlashcardService(IFlashcardRepository repository, AppDbContext context)
        {
            _context = context;
            _repository = repository;
=======
        private readonly AppDbContext _context;

        private readonly IProgresoService
            _progresoService;

        public FlashcardService(
            AppDbContext context,
            IProgresoService progresoService)
        {
            _context = context;
            _progresoService = progresoService;
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864
        }

        public async Task<List<FlashcardResponseDTO>>
            ObtenerPorArchivoAsync(
                int idAnalisis)
        {
            return await _context.Flashcards
                .Where(f =>
                    f.IdAnalisis == idAnalisis)
                .Select(f =>
                    new FlashcardResponseDTO
                    {
                        IdFlashcard = f.IdFlashcard,
                        Pregunta = f.Pregunta,
                        Respuesta = f.Respuesta,
                        NivelDificultad =
                            f.NivelDificultad,
                        VecesEstudiada =
                            f.VecesEstudiada
                    })
                .ToListAsync();
        }

        public async Task<bool>
            ResponderAsync(
                int idUsuario,
                int idFlashcard,
                bool correcta,
                int tiempoRespuestaSegundos)
        {
<<<<<<< HEAD
            var resultado = await _repository
                .ResponderAsync(
                    idUsuario,
                    idFlashcard,
                    correcta,
                    tiempoRespuestaSegundos);

            if (!resultado)
                return false;

            // =====================================
            // ACTUALIZAR PROGRESO FLASHCARD
            // =====================================

            var progreso = await _context
                .ProgresoFlashcards
                .FirstOrDefaultAsync(p =>
                    p.IdUsuario == idUsuario &&
                    p.IdFlashcard == idFlashcard);

            if (progreso == null)
            {
                progreso = new ProgresoFlashcard
                {
                    IdUsuario = idUsuario,

                    IdFlashcard = idFlashcard,

                    Completado = correcta,

                    VecesRepasada = 1
                };

                _context.ProgresoFlashcards
                    .Add(progreso);
            }
            else
            {
                progreso.Completado = correcta;

                progreso.VecesRepasada += 1;
            }

            await _context.SaveChangesAsync();

=======
            var flashcard =
                await _context.Flashcards
                    .Include(f =>
                        f.AnalisisIA)
                    .FirstOrDefaultAsync(f =>
                        f.IdFlashcard == idFlashcard);

            if (flashcard == null)
                return false;

            var historial = new HistorialFlashcard
            {
                IdUsuario = idUsuario,
                IdFlashcard = idFlashcard,
                Correcta = correcta,
                TiempoRespuestaSegundos =
                    tiempoRespuestaSegundos,
                FechaRespuesta = DateTime.UtcNow
            };

            _context.HistorialFlashcards
                .Add(historial);

            flashcard.VecesEstudiada++;

            await _context.SaveChangesAsync();

            // AUTO-UPDATE PROGRESO
            if (flashcard.AnalisisIA != null)
            {
                var flashcardsCompletadas =
                    await _context
                        .HistorialFlashcards
                        .Where(hf =>
                            hf.IdUsuario == idUsuario
                            && hf.Flashcard != null
                            && hf.Flashcard.IdAnalisis
                                == flashcard.IdAnalisis)
                        .Select(hf => hf.IdFlashcard)
                        .Distinct()
                        .CountAsync();

                await _progresoService
                    .ActualizarProgresoAsync(
                        idUsuario,
                        flashcard.AnalisisIA
                            .IdArchivo,
                        flashcardsCompletadas,
                        0);
            }

>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864
            return true;
        }

        public async Task<FlashcardResponseDTO?>
            ObtenerPorIdAsync(
                int idFlashcard)
        {
            return await _context.Flashcards
                .Where(f =>
                    f.IdFlashcard == idFlashcard)
                .Select(f =>
                    new FlashcardResponseDTO
                    {
                        IdFlashcard = f.IdFlashcard,
                        Pregunta = f.Pregunta,
                        Respuesta = f.Respuesta,
                        NivelDificultad =
                            f.NivelDificultad,
                        VecesEstudiada =
                            f.VecesEstudiada
                    })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> VerificarAccesoAnalisisAsync(
            int idAnalisis,
            int idUsuario)
        {
            return await _context.AnalisisIA
                .AnyAsync(a =>
                    a.IdAnalisis == idAnalisis
                    && a.ArchivoSubido != null
                    && a.ArchivoSubido.IdUsuario == idUsuario);
        }

        public async Task<bool> VerificarAccesoFlashcardAsync(
            int idFlashcard,
            int idUsuario)
        {
            return await _context.Flashcards
                .AnyAsync(f =>
                    f.IdFlashcard == idFlashcard
                    && f.AnalisisIA != null
                    && f.AnalisisIA.ArchivoSubido != null
                    && f.AnalisisIA.ArchivoSubido.IdUsuario == idUsuario);
        }

        // =====================================
        // CREAR FLASHCARDS MANUALMENTE
        // =====================================
        public async Task<FlashcardResponseDTO> CrearManualAsync(CrearFlashcardDTO dto)
        {
            var flashcard = new Flashcard
            {
                IdAnalisis = dto.IdAnalisis,
                Pregunta = dto.Pregunta,
                Respuesta = dto.Respuesta,
                NivelDificultad = "MEDIO",
                VecesEstudiada = 0,
                FechaCreacion = DateTime.UtcNow
            };

            _context.Flashcards.Add(flashcard);

            await _context.SaveChangesAsync();

            return new FlashcardResponseDTO
            {
                IdFlashcard = flashcard.IdFlashcard,
                Pregunta = flashcard.Pregunta,
                Respuesta = flashcard.Respuesta,
                NivelDificultad = flashcard.NivelDificultad,
                VecesEstudiada = flashcard.VecesEstudiada
            };
        }

        // =====================================
        // GET ALL FLASHCARDS
        // =====================================

        public async Task<IEnumerable<FlashcardResponseDTO>> ObtenerTodosAsync()
        {
            return await _context.Flashcards
                .Select(f => new FlashcardResponseDTO
                {
                    IdFlashcard = f.IdFlashcard,
                    Pregunta = f.Pregunta,
                    Respuesta = f.Respuesta,
                    NivelDificultad = f.NivelDificultad,
                    VecesEstudiada = f.VecesEstudiada
                })
                .ToListAsync();
        }
    }
}
