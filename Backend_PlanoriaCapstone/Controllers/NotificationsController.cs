using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.Notifications.Requests;
using Backend_PlanoriaCapstone.Extensions;

namespace Backend_PlanoriaCapstone.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications([FromQuery] bool? unreadOnly)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _notificationService.GetNotificationsAsync(userId.Value, unreadOnly);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotification(int id)
        {
            var result = await _notificationService.GetNotificationAsync(id);
            return Ok(result);
        }

        [HttpPatch("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _notificationService.MarkAsReadAsync(id);
            return Ok(new { message = "Notification marked as read" });
        }

        [HttpPatch("read-all")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            await _notificationService.MarkAllAsReadAsync(userId.Value);
            return Ok(new { message = "All notifications marked as read" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _notificationService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPost("reminders")]
        public async Task<IActionResult> CreateReminder([FromBody] ScheduleReminderRequestDto request)
        {
            var result = await _notificationService.CreateReminderAsync(request);
            return Ok(result);
        }

        [HttpGet("reminders/pending")]
        public async Task<IActionResult> GetPendingReminders()
        {
            var result = await _notificationService.GetPendingRemindersAsync();
            return Ok(result);
        }

        [HttpDelete("reminders/{id}")]
        public async Task<IActionResult> CancelReminder(int id)
        {
            var result = await _notificationService.CancelReminderAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPost("email/test")]
        public async Task<IActionResult> SendTestEmail()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            await _notificationService.SendTestEmailAsync(userId.Value);
            return Ok(new { message = "Test email sent" });
        }

        [HttpGet("email/logs")]
        public async Task<IActionResult> GetEmailLogs()
        {
            var result = await _notificationService.GetEmailLogsAsync();
            return Ok(result);
        }

        [HttpPost("email/retry/{id}")]
        public async Task<IActionResult> RetryFailedEmail(int id)
        {
            var result = await _notificationService.RetryFailedEmailAsync(id);
            if (!result) return NotFound();
            return Ok(new { message = "Email retry initiated" });
        }

        [HttpPost("push/register")]
        public async Task<IActionResult> RegisterPushDevice([FromBody] RegisterPushDeviceRequestDto request)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            await _notificationService.RegisterPushDeviceAsync(userId.Value, request);
            return Ok(new { message = "Push device registered" });
        }

        [HttpPost("push/unregister")]
        public async Task<IActionResult> UnregisterPushDevice([FromQuery] int deviceId)
        {
            await _notificationService.UnregisterPushDeviceAsync(deviceId);
            return Ok(new { message = "Push device unregistered" });
        }

        [HttpPost("push/send")]
        public async Task<IActionResult> SendPush([FromQuery] int userId, [FromQuery] string title, [FromQuery] string message)
        {
            await _notificationService.SendPushAsync(userId, title, message);
            return Ok(new { message = "Push notification sent" });
        }
    }
}
