using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.Cronograma.Requests;
using Backend_PlanoriaCapstone.Extensions;

namespace Backend_PlanoriaCapstone.Controllers
{
    [ApiController]
    [Route("api/schedules")]
    [Authorize]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public SchedulesController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByUser()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _scheduleService.GetByUserAsync(userId.Value);
            return Ok(result);
        }

        [HttpGet("range")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _scheduleService.GetByDateRangeAsync(userId.Value, from, to);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _scheduleService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateScheduleRequestDto request)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _scheduleService.CreateAsync(userId.Value, request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateScheduleRequestDto request)
        {
            var result = await _scheduleService.UpdateAsync(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _scheduleService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("calendar/month")]
        public async Task<IActionResult> GetMonthView([FromQuery] int year, [FromQuery] int month)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _scheduleService.GetMonthViewAsync(userId.Value, year, month);
            return Ok(result);
        }

        [HttpGet("calendar/week")]
        public async Task<IActionResult> GetWeekView([FromQuery] int year, [FromQuery] int week)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _scheduleService.GetWeekViewAsync(userId.Value, year, week);
            return Ok(result);
        }

        [HttpGet("calendar/day")]
        public async Task<IActionResult> GetDayView([FromQuery] DateTime date)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _scheduleService.GetDayViewAsync(userId.Value, date);
            return Ok(result);
        }

        [HttpGet("calendar/agenda")]
        public async Task<IActionResult> GetAgenda([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _scheduleService.GetAgendaAsync(userId.Value, from, to);
            return Ok(result);
        }

        [HttpPost("recurring")]
        public async Task<IActionResult> CreateRecurring([FromBody] CreateScheduleRequestDto request, [FromQuery] string recurrence)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            await _scheduleService.CreateRecurringAsync(userId.Value, request, recurrence);
            return Ok(new { message = "Recurring schedules created" });
        }

        [HttpPut("recurring/{id}")]
        public async Task<IActionResult> UpdateRecurring(int id, [FromBody] UpdateScheduleRequestDto request)
        {
            await _scheduleService.UpdateRecurringAsync(id, request);
            return Ok(new { message = "Recurring schedule updated" });
        }

        [HttpDelete("recurring/{id}")]
        public async Task<IActionResult> DeleteRecurring(int id)
        {
            await _scheduleService.DeleteRecurringAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> MarkComplete(int id)
        {
            await _scheduleService.MarkCompleteAsync(id);
            return Ok(new { message = "Schedule completed" });
        }

        [HttpPatch("{id}/incomplete")]
        public async Task<IActionResult> MarkIncomplete(int id)
        {
            await _scheduleService.MarkIncompleteAsync(id);
            return Ok(new { message = "Schedule marked incomplete" });
        }

        [HttpPost("bulk-complete")]
        public async Task<IActionResult> BulkComplete([FromBody] List<int> scheduleIds)
        {
            await _scheduleService.BulkCompleteAsync(scheduleIds);
            return Ok(new { message = "Schedules completed" });
        }
    }
}
