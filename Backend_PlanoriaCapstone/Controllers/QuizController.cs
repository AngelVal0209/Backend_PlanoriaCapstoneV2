using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.Quiz;
using System.Security.Claims;

namespace Backend_PlanoriaCapstone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }
        // GET api/quiz/todos
        // Returns all quizzes summaries
        [HttpGet("todos")]
        public async Task<IActionResult> ObtenerTodos()
        {
            var quizzes =
                await _quizService.ObtenerTodosAsync();

            return Ok(new
            {
                success = true,
                data = quizzes
            });
        }
        // GET api/quiz?idArchivo=5
        // Returns all quizzes generated for a given archivo
        [HttpGet]
        public async Task<IActionResult> ObtenerPorArchivo(
            [FromQuery] int idArchivo)
        {
            var quizzes =
                await _quizService.ObtenerPorArchivoAsync(idArchivo);

            if (quizzes == null || !quizzes.Any())
            {
                return NotFound(
                    "No se encontraron quizzes.");
            }

            return Ok(new
            {
                success = true,
                data = quizzes
            });
        }
        // GET api/quiz/{id}
        // Returns a specific quiz with its questions
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var quiz =
                await _quizService.ObtenerPorIdAsync(id);

            if (quiz == null)
                return NotFound("Quiz no encontrado.");

            return Ok(new
            {
                success = true,
                data = quiz
            });
        }

        // POST api/quiz/{id}/resolver
        // Submits the result of a completed quiz
        [HttpPost("{id:int}/resolver")]
        public async Task<IActionResult> ResolverQuiz(int id, [FromBody] ResolverQuizDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            // Map from DTO to primitive params expected by the service
            int correctas   = dto.Correctas;
            int incorrectas = dto.Incorrectas;
            decimal puntaje = dto.Puntaje;
            int tiempoMinutos = dto.TiempoMinutos;

            var resultado = await _quizService.ResolverQuizAsync(
                userId.Value,
                id,
                correctas,
                incorrectas,
                puntaje,
                tiempoMinutos);

            if (!resultado)
                return BadRequest("No se pudo guardar el resultado del quiz.");

            return Ok(new { success = true, mensaje = "Quiz resuelto correctamente." });
        }

        // ─── Helper ────────────────────────────────────────────────────────────
        private int? ObtenerUserId()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(claim, out var id) ? id : null;
        }
    }
}
