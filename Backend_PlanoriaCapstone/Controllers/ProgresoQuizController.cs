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
    public class ProgresoQuizController
        : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProgresoQuizController(
            AppDbContext context)
        {
            _context = context;
        }

        // =====================================
        // GET QUIZ PROGRESS
        // =====================================
        // GET api/progresoquiz/{idQuiz}
        // Returns user progress for a quiz
        // =====================================

        [HttpGet("{idQuiz:int}")]
        public async Task<IActionResult>
            Obtener(int idQuiz)
        {
            var userId = ObtenerUserId();

            if (userId == null)
                return Unauthorized();

            var progreso =
                await _context.ProgresoQuizzes
                    .FirstOrDefaultAsync(p =>
                        p.IdUsuario == userId &&
                        p.IdQuiz == idQuiz);

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

                    progreso.IdQuiz,

                    progreso.Puntaje,

                    progreso.Completado,

                    progreso.FechaRealizacion
                }
            });
        }

        // =====================================
        // GET ALL USER QUIZ PROGRESS
        // =====================================
        // GET api/progresoquiz
        // Returns all quiz progress
        // =====================================

        [HttpGet]
        public async Task<IActionResult>
            ObtenerTodos()
        {
            var userId = ObtenerUserId();

            if (userId == null)
                return Unauthorized();

            var progresos =
                await _context.ProgresoQuizzes
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