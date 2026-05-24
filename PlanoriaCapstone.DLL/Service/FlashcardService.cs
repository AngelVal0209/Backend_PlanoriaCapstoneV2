using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal;
using PlanoriaCapstone.DTOs.Flashcard;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Bll.Service
{
    public class FlashcardService
         : IFlashcardService
    {
        private readonly AppDbContext _context;

        private readonly IProgresoService
            _progresoService;

        public FlashcardService(
            AppDbContext context,
            IProgresoService progresoService)
        {
            _context = context;
            _progresoService = progresoService;
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
    }
}
