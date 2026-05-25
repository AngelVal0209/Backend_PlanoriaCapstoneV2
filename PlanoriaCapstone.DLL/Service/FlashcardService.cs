using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal;
using PlanoriaCapstone.Dal.Interface;
using PlanoriaCapstone.DTOs.Flashcard;
using PlanoriaCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Bll.Service
{
    public class FlashcardService
         : IFlashcardService
    {
        private readonly IFlashcardRepository _repository;
        private readonly AppDbContext _context;

        public FlashcardService(IFlashcardRepository repository, AppDbContext context)
        {
            _context = context;
            _repository = repository;
        }

        // =====================================
        // OBTENER FLASHCARDS
        // =====================================

        public async Task<IEnumerable<Flashcard>>
            ObtenerPorArchivoAsync(
                int idAnalisis)
        {
            return await _repository
                .ObtenerPorArchivoAsync(
                    idAnalisis);
        }

        // =====================================
        // RESPONDER FLASHCARD
        // =====================================

        public async Task<bool>
            ResponderAsync(
                int idUsuario,
                int idFlashcard,
                bool correcta,
                int tiempoRespuestaSegundos)
        {
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

            return true;
        }

        // =====================================
        // OBTENER FLASHCARD POR ID
        // =====================================

        public async Task<Flashcard?>
            ObtenerPorIdAsync(
                int idFlashcard)
        {
            return await _repository
                .ObtenerPorIdAsync(
                    idFlashcard);
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
