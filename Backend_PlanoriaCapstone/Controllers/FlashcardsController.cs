using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.Flashcard;
using System.Security.Claims;

namespace Backend_PlanoriaCapstone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FlashcardsController : ControllerBase
    {
        private readonly IFlashcardService _flashcardService;

        public FlashcardsController(IFlashcardService flashcardService)
        {
            _flashcardService = flashcardService;
        }
        // GET api/flashcards/todos
        // Returns all flashcards available in the system
        [HttpGet("todos")]
        public async Task<IActionResult> ObtenerTodos()
        {
            var flashcards =
                await _flashcardService.ObtenerTodosAsync();

            return Ok(new
            {
                success = true,
                data = flashcards
            });
        }
        // GET api/flashcards?idAnalisis=5
        // Returns all flashcards generated for a given analysis/archivo
        [HttpGet]
        public async Task<IActionResult> ObtenerPorAnalisis([FromQuery] int idAnalisis)
        {
            var flashcards = await _flashcardService.ObtenerPorArchivoAsync(idAnalisis);

            if (flashcards == null || !flashcards.Any())
                return NotFound("No se encontraron flashcards para este análisis.");

            return Ok(flashcards);
        }

        // GET api/flashcards/{id}
        // Returns a single flashcard by its ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var flashcard = await _flashcardService.ObtenerPorIdAsync(id);

            if (flashcard == null)
                return NotFound("Flashcard no encontrada.");

            return Ok(flashcard);
        }

        // POST api/flashcards
        // Creates a manual flashcard linked to an analysis
        [HttpPost]
        public async Task<IActionResult> Crear(
            [FromBody] CrearFlashcardDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var flashcard =
                    await _flashcardService.CrearManualAsync(dto);

                return Ok(new
                {
                    success = true,
                    message = "Flashcard creada correctamente.",
                    data = flashcard
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        success = false,
                        message = ex.Message
                    });
            }
        }

        // POST api/flashcards/responder
        // Records the user's answer to a flashcard
        [HttpPost("responder")]
        public async Task<IActionResult> Responder([FromBody] ResponderFlashcardDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = ObtenerUserId();

            if (userId == null)
                return Unauthorized("Token inválido.");

            var resultado = await _flashcardService.ResponderAsync(
                userId.Value,
                dto.IdFlashcard,
                dto.Correcta,
                dto.TiempoRespuestaSegundos);

            if (!resultado)
                return BadRequest("No se pudo registrar la respuesta.");

            return Ok(new
            {
                success = true,
                mensaje = "Flashcard respondida correctamente."
            });
        }

        // ─── Helper ────────────────────────────────────────────────────────────
        private int? ObtenerUserId()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(claim, out var id) ? id : null;
        }
    }
}
