using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.System.Requests;
using Backend_PlanoriaCapstone.Extensions;

namespace Backend_PlanoriaCapstone.Controllers
{
    [ApiController]
    [Route("api/system")]
    [Authorize]
    public class SystemController : ControllerBase
    {
        private readonly ISystemService _systemService;

        public SystemController(ISystemService systemService)
        {
            _systemService = systemService;
        }

        [HttpGet("config")]
        public async Task<IActionResult> GetConfigs()
        {
            var result = await _systemService.GetConfigsAsync();
            return Ok(result);
        }

        [HttpGet("config/{key}")]
        public async Task<IActionResult> GetConfig(string key)
        {
            var result = await _systemService.GetConfigAsync(key);
            return Ok(result);
        }

        [HttpPut("config")]
        public async Task<IActionResult> UpdateConfig([FromBody] UpdateSystemConfigRequestDto request)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _systemService.UpdateConfigAsync(userId.Value, request);
            return Ok(result);
        }

        [HttpPost("config/{key}/reset")]
        public async Task<IActionResult> ResetConfig(string key)
        {
            await _systemService.ResetConfigAsync(key);
            return Ok(new { message = "Config reset" });
        }

        [HttpGet("health")]
        public async Task<IActionResult> HealthCheck()
        {
            var result = await _systemService.HealthCheckAsync();
            return Ok(result);
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var result = await _systemService.GetStatusAsync();
            return Ok(result);
        }

        [HttpGet("metrics")]
        public async Task<IActionResult> GetMetrics()
        {
            var result = await _systemService.GetMetricsAsync();
            return Ok(result);
        }

        [HttpPost("cache/clear")]
        public async Task<IActionResult> ClearCache([FromBody] ClearCacheRequestDto request)
        {
            await _systemService.ClearCacheAsync(request?.CacheType);
            return Ok(new { message = "Cache cleared" });
        }

        [HttpGet("cache/stats")]
        public async Task<IActionResult> GetCacheStats()
        {
            var result = await _systemService.GetCacheStatsAsync();
            return Ok(result);
        }

        [HttpPost("cache/warmup")]
        public async Task<IActionResult> WarmupCache()
        {
            await _systemService.WarmupCacheAsync();
            return Ok(new { message = "Cache warmed up" });
        }

        [HttpGet("logs")]
        public async Task<IActionResult> GetLogs([FromQuery] GetLogsRequestDto request)
        {
            var result = await _systemService.GetLogsAsync(request);
            return Ok(result);
        }

        [HttpGet("logs/search")]
        public async Task<IActionResult> SearchLogs([FromQuery] string query)
        {
            var result = await _systemService.SearchLogsAsync(query);
            return Ok(result);
        }

        [HttpGet("logs/export")]
        public async Task<IActionResult> ExportLogs([FromQuery] GetLogsRequestDto request)
        {
            var result = await _systemService.ExportLogsAsync(request);
            return File(result, "application/json", "logs-export.json");
        }
    }
}
