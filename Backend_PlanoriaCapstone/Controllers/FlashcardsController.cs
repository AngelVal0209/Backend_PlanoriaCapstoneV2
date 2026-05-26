using Backend_PlanoriaCapstone.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.Flashcard;

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
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            // Ownership check: verify the analysis belongs to a file owned by the user
            var tieneAcceso = await _flashcardService.VerificarAccesoAnalisisAsync(idAnalisis, userId.Value);
            if (!tieneAcceso) return Forbid();

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
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");

            var tieneAcceso = await _flashcardService.VerificarAccesoFlashcardAsync(id, userId.Value);
            if (!tieneAcceso) return Forbid();

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

<<<<<<< HEAD
            var userId = ObtenerUserId();

            if (userId == null)
                return Unauthorized("Token inválido.");
=======
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized("Token inválido.");
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864

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
    }
}
