using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.Curso;
using System.Security.Claims;

namespace Backend_PlanoriaCapstone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        public CursoController(
            ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        // =====================================
        // GET ALL CURSOS
        // =====================================
        // GET api/curso
        // Returns all courses
        // =====================================

        [HttpGet]
        public async Task<IActionResult>
            ObtenerTodos()
        {
            var cursos =
                await _cursoService
                    .ObtenerTodosAsync();

            return Ok(new
            {
                success = true,
                data = cursos
            });
        }

        // =====================================
        // GET CURSO BY ID
        // =====================================
        // GET api/curso/5
        // Returns a course by ID
        // =====================================

        [HttpGet("{id:int}")]
        public async Task<IActionResult>
            ObtenerPorId(int id)
        {
            var curso =
                await _cursoService
                    .ObtenerPorIdAsync(id);

            if (curso == null)
                return NotFound(
                    "Curso no encontrado.");

            return Ok(new
            {
                success = true,
                data = curso
            });
        }

        // =====================================
        // CREATE CURSO
        // =====================================
        // POST api/curso
        // Creates a new course
        // =====================================

        [HttpPost]
        public async Task<IActionResult>
        Crear(
        [FromBody]
        CrearCursoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = ObtenerUserId();

            if (userId == null)
                return Unauthorized("Token inválido.");

            var curso =
                await _cursoService
                    .CrearAsync(dto, userId.Value);

            return Ok(new
            {
                success = true,
                data = curso
            });
        }

        // =====================================
        // DELETE CURSO
        // =====================================
        // DELETE api/curso/5
        // Deletes a course
        // =====================================

        [HttpDelete("{id:int}")]
        public async Task<IActionResult>
            Eliminar(int id)
        {
            var eliminado =
                await _cursoService
                    .EliminarAsync(id);

            if (!eliminado)
                return NotFound(
                    "Curso no encontrado.");

            return Ok(new
            {
                success = true,
                mensaje =
                    "Curso eliminado correctamente."
            });
        }
        // ─── Helper ────────────────────────────────────────────────────────────
        private int? ObtenerUserId()
        {
            var claim =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

            return int.TryParse(claim, out var id)
                ? id
                : null;
        }
    }
}