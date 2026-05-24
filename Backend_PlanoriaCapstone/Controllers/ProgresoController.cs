using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.Progreso;
using System.Security.Claims;

namespace Backend_PlanoriaCapstone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProgresoController : ControllerBase
    {
        private readonly IProgresoService _progresoService;

        public ProgresoController(IProgresoService progresoService)
        {
            _progresoService = progresoService;
        }

        // GET api/progreso/resumen
        // Returns a dashboard summary for the authenticated user
        [HttpGet("resumen")]
        public async Task<IActionResult> ObtenerResumen()
        {
            var userId = ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            var resumen = await _progresoService.ObtenerResumenAsync(userId.Value);
            return Ok(resumen);
        }

        // GET api/progreso
        // Returns the complete progress summary for all files of the authenticated user
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var userId = ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            var progresos = await _progresoService.ObtenerTodosUsuarioAsync(userId.Value);
            return Ok(progresos);
        }

        // GET api/progreso/{idArchivo}
        // Returns progress for a specific file of the authenticated user
        [HttpGet("{idArchivo:int}")]
        public async Task<IActionResult> ObtenerPorArchivo(int idArchivo)
        {
            var userId = ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            var progreso = await _progresoService.ObtenerProgresoAsync(userId.Value, idArchivo);
            if (progreso == null)
                return NotFound("Progreso no encontrado para este archivo.");

            return Ok(progreso);
        }

        // GET api/progreso/{idArchivo}/promedio
        // Returns the average quiz score for a specific file of the authenticated user
        [HttpGet("{idArchivo:int}/promedio")]
        public async Task<IActionResult> ObtenerPromedio(int idArchivo)
        {
            var userId = ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            var promedio = await _progresoService.ObtenerPromedioQuizAsync(userId.Value, idArchivo);
            return Ok(new { idArchivo, promedioQuiz = promedio });
        }

        // PUT api/progreso
        // Updates the progress counters for a specific file
        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarProgresoDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            await _progresoService.ActualizarProgresoAsync(
                userId.Value,
                dto.IdArchivo,
                dto.FlashcardsCompletadas,
                dto.QuizzesCompletados);

            return Ok(new { success = true, mensaje = "Progreso actualizado correctamente." });
        }

        // ─── Helper ────────────────────────────────────────────────────────────
        private int? ObtenerUserId()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(claim, out var id) ? id : null;
        }
    }
}
