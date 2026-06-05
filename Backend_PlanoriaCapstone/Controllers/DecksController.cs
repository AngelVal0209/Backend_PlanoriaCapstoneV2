using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.Flashcards.Cards.Requests;
using PlanoriaCapstone.DTOs.Flashcards.Decks.Requests;
using Backend_PlanoriaCapstone.Extensions;

namespace Backend_PlanoriaCapstone.Controllers
{
    [ApiController]
    [Route("api/decks")]
    [Authorize]
    public class DecksController : ControllerBase
    {
        private readonly IFlashcardDeckService _deckService;

        public DecksController(IFlashcardDeckService deckService)
        {
            _deckService = deckService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByCourse([FromQuery] int? courseId)
        {
            if (!courseId.HasValue)
                return BadRequest(new { message = "courseId es requerido" });

            var result = await _deckService.GetByCourseIdAsync(courseId.Value);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _deckService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDeckRequestDto request)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();

            var result = await _deckService.CreateAsync(userId.Value, request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDeckRequestDto request)
        {
            var result = await _deckService.UpdateAsync(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _deckService.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = $"Deck {id} no encontrado" });

            return Ok(new { message = "Deck eliminado" });
        }

        [HttpPost("{id}/duplicate")]
        public async Task<IActionResult> Duplicate(int id, [FromBody] DuplicateDeckRequestDto request)
        {
            var result = await _deckService.DuplicateAsync(id, request);
            return Ok(result);
        }

        [HttpGet("{id}/cards")]
        public async Task<IActionResult> GetCards(int id)
        {
            var result = await _deckService.GetCardsAsync(id);
            return Ok(result);
        }

        [HttpPost("{id}/cards")]
        public async Task<IActionResult> AddCards(int id, [FromBody] BulkCreateFlashcardsRequestDto request)
        {
            await _deckService.AddCardsAsync(id, request);
            return Ok(new { message = "Tarjetas agregadas" });
        }

        [HttpDelete("{id}/cards")]
        public async Task<IActionResult> RemoveCards(int id, [FromBody] RemoveCardsRequest request)
        {
            await _deckService.RemoveCardsAsync(id, request.CardIds);
            return Ok(new { message = "Tarjetas eliminadas" });
        }

        [HttpPut("{id}/cards/reorder")]
        public async Task<IActionResult> ReorderCards(int id, [FromBody] ReorderFlashcardsRequestDto request)
        {
            await _deckService.ReorderCardsAsync(id, request);
            return Ok(new { message = "Tarjetas reordenadas" });
        }
    }

    public class RemoveCardsRequest
    {
        public List<int> CardIds { get; set; } = new();
    }
}
