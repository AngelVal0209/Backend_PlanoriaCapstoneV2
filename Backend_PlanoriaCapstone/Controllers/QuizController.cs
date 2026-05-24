using Backend_PlanoriaCapstone.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.Quiz;

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

        // GET api/quiz?idArchivo=5
        // Returns all quizzes generated for a given archivo
        [HttpGet]
        public async Task<IActionResult> ObtenerPorArchivo([FromQuery] int idArchivo)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            var tieneAcceso = await _quizService.VerificarAccesoArchivoAsync(idArchivo, userId.Value);
            if (!tieneAcceso) return Forbid();

            var quizzes = await _quizService.ObtenerPorArchivoAsync(idArchivo);

            if (quizzes == null || !quizzes.Any())
                return NotFound("No se encontraron quizzes para este archivo.");

            return Ok(quizzes);
        }

        // GET api/quiz/{id}
        // Returns a specific quiz with its questions
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            var tieneAcceso = await _quizService.VerificarAccesoQuizAsync(id, userId.Value);
            if (!tieneAcceso) return Forbid();

            var quiz = await _quizService.ObtenerPorIdAsync(id);
            if (quiz == null) return NotFound("Quiz no encontrado.");

            return Ok(quiz);
        }

        // POST api/quiz/{id}/resolver
        // Submits the result of a completed quiz
        [HttpPost("{id:int}/resolver")]
        public async Task<IActionResult> ResolverQuiz(int id, [FromBody] ResolverQuizDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            var resultado = await _quizService.ResolverQuizAsync(
                userId.Value,
                id,
                dto.Correctas,
                dto.Incorrectas,
                dto.Puntaje,
                dto.TiempoMinutos);

            if (!resultado)
                return BadRequest("No se pudo guardar el resultado del quiz.");

            return Ok(new { success = true, mensaje = "Quiz resuelto correctamente." });
        }
    }
}
