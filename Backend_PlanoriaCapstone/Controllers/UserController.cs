using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.Users.Requests;
using Backend_PlanoriaCapstone.Extensions;

namespace Backend_PlanoriaCapstone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _userService.GetProfileAsync(userId.Value);
            return Ok(result);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequestDto request)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _userService.UpdateProfileAsync(userId.Value, request);
            return Ok(result);
        }

        [HttpPost("avatar")]
        public async Task<IActionResult> UploadAvatar(IFormFile file)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "Archivo no proporcionado" });

            using var stream = file.OpenReadStream();
            await _userService.UploadAvatarAsync(userId.Value, stream, file.FileName);
            return Ok(new { message = "Avatar subido" });
        }

        [HttpDelete("avatar")]
        public async Task<IActionResult> DeleteAvatar()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            await _userService.DeleteAvatarAsync(userId.Value);
            return Ok(new { message = "Avatar eliminado" });
        }

        [HttpGet("preferences")]
        public async Task<IActionResult> GetPreferences()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _userService.GetPreferencesAsync(userId.Value);
            return Ok(result);
        }

        [HttpPut("preferences")]
        public async Task<IActionResult> UpdatePreferences([FromBody] UpdatePreferencesRequestDto request)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _userService.UpdatePreferencesAsync(userId.Value, request);
            return Ok(result);
        }

        [HttpPost("preferences/reset")]
        public async Task<IActionResult> ResetDefaults()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            await _userService.ResetDefaultsAsync(userId.Value);
            return Ok(new { message = "Preferencias restablecidas" });
        }

        [HttpGet("notification-settings")]
        public async Task<IActionResult> GetNotificationSettings()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _userService.GetNotificationSettingsAsync(userId.Value);
            return Ok(result);
        }

        [HttpPut("notification-settings")]
        public async Task<IActionResult> UpdateNotificationSettings([FromBody] UpdateNotificationSettingsRequestDto request)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _userService.UpdateNotificationSettingsAsync(userId.Value, request);
            return Ok(result);
        }

        [HttpPost("notification-settings/test")]
        public async Task<IActionResult> TestNotification()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            await _userService.TestNotificationAsync(userId.Value);
            return Ok(new { message = "Notificación de prueba enviada" });
        }

        [HttpDelete("account")]
        public async Task<IActionResult> DeleteAccount([FromBody] DeleteAccountRequestDto request)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            await _userService.DeleteAccountAsync(userId.Value, request);
            return Ok(new { message = "Cuenta eliminada" });
        }

        [HttpPost("export")]
        public async Task<IActionResult> ExportData([FromBody] ExportDataRequestDto request)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _userService.ExportDataAsync(userId.Value, request);
            return Ok(result);
        }

        [HttpPost("deactivate")]
        public async Task<IActionResult> Deactivate()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            await _userService.DeactivateAsync(userId.Value);
            return Ok(new { message = "Cuenta desactivada" });
        }
    }
}
