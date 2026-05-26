using Backend_PlanoriaCapstone.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
<<<<<<< HEAD
using System.Security.Claims;
=======
using PlanoriaCapstone.DTOs.Progreso;
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864

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

<<<<<<< HEAD
        // =====================================
        // GET ALL USER PROGRESS
        // =====================================
=======
        // GET api/progreso/resumen
        // Returns a dashboard summary for the authenticated user
        [HttpGet("resumen")]
        public async Task<IActionResult> ObtenerResumen()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            var resumen = await _progresoService.ObtenerResumenAsync(userId.Value);
            return Ok(resumen);
        }

>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864
        // GET api/progreso
        // Returns dashboard progress summary
        // for all user files
        // =====================================

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
<<<<<<< HEAD
            var userId = ObtenerUserId();
=======
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864

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
<<<<<<< HEAD
            var userId = ObtenerUserId();
=======
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864

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
<<<<<<< HEAD
            var userId = ObtenerUserId();
=======
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864

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

<<<<<<< HEAD
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
=======
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            await _progresoService.ActualizarProgresoAsync(
                userId.Value,
                dto.IdArchivo,
                dto.FlashcardsCompletadas,
                dto.QuizzesCompletados);

            return Ok(new { success = true, mensaje = "Progreso actualizado correctamente." });
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864
        }
    }
}