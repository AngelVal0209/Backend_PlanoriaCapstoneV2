using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using System.Security.Claims;

namespace Backend_PlanoriaCapstone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProgresoController : ControllerBase
    {
        private readonly IProgresoService _progresoService;

        public ProgresoController(
            IProgresoService progresoService)
        {
            _progresoService = progresoService;
        }

        // =====================================
        // GET ALL USER PROGRESS
        // =====================================
        // GET api/progreso
        // Returns dashboard progress summary
        // for all user files
        // =====================================

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var userId = ObtenerUserId();

            if (userId == null)
                return Unauthorized("Token inválido.");

            var progresos =
                await _progresoService
                    .ObtenerTodosUsuarioAsync(
                        userId.Value);

            return Ok(new
            {
                success = true,
                data = progresos
            });
        }

        // =====================================
        // GET PROGRESS BY FILE
        // =====================================
        // GET api/progreso/{idArchivo}
        // Returns dashboard progress
        // for a specific file
        // =====================================

        [HttpGet("{idArchivo:int}")]
        public async Task<IActionResult>
            ObtenerPorArchivo(int idArchivo)
        {
            var userId = ObtenerUserId();

            if (userId == null)
                return Unauthorized("Token inválido.");

            var progreso =
                await _progresoService
                    .ObtenerProgresoAsync(
                        userId.Value,
                        idArchivo);

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
                data = progreso
            });
        }

        // =====================================
        // GET QUIZ AVERAGE
        // =====================================
        // GET api/progreso/{idArchivo}/promedio
        // Returns average quiz score
        // =====================================

        [HttpGet("{idArchivo:int}/promedio")]
        public async Task<IActionResult>
            ObtenerPromedio(int idArchivo)
        {
            var userId = ObtenerUserId();

            if (userId == null)
                return Unauthorized("Token inválido.");

            var promedio =
                await _progresoService
                    .ObtenerPromedioQuizAsync(
                        userId.Value,
                        idArchivo);

            return Ok(new
            {
                success = true,

                data = new
                {
                    idArchivo,
                    promedioQuiz = promedio
                }
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