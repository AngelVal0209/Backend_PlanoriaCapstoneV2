using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Dal;
using System.Security.Claims;

namespace Backend_PlanoriaCapstone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProgresoFlashcardController
        : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProgresoFlashcardController(
            AppDbContext context)
        {
            _context = context;
        }

        // =====================================
        // GET FLASHCARD PROGRESS
        // =====================================
        // GET api/progresoflashcard/{idFlashcard}
        // Returns user progress for a flashcard
        // =====================================

        [HttpGet("{idFlashcard:int}")]
        public async Task<IActionResult>
            Obtener(int idFlashcard)
        {
            var userId = ObtenerUserId();

            if (userId == null)
                return Unauthorized();

            var progreso =
                await _context.ProgresoFlashcards
                    .FirstOrDefaultAsync(p =>
                        p.IdUsuario == userId &&
                        p.IdFlashcard == idFlashcard);

            if (progreso == null)
            {
                return NotFound(new
                {
                    success = false,
                    message =
                        "Progreso no encontrado."
                });
            }

            return Ok(new
            {
                success = true,

                data = new
                {
                    progreso.Id,

                    progreso.IdFlashcard,

                    progreso.Completado,

                    progreso.VecesRepasada
                }
            });
        }

        // =====================================
        // GET ALL USER FLASHCARD PROGRESS
        // =====================================
        // GET api/progresoflashcard
        // Returns all flashcard progress
        // =====================================

        [HttpGet]
        public async Task<IActionResult>
            ObtenerTodos()
        {
            var userId = ObtenerUserId();

            if (userId == null)
                return Unauthorized();

            var progresos =
                await _context.ProgresoFlashcards
                    .Where(p =>
                        p.IdUsuario == userId)
                    .ToListAsync();

            return Ok(new
            {
                success = true,
                data = progresos
            });
        }

        // =====================================
        // HELPER
        // =====================================

        private int? ObtenerUserId()
        {
            var claim =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

            return int.TryParse(
                claim,
                out var id)
                ? id
                : null;
        }
    }
}