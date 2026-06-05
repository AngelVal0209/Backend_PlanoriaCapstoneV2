using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.Progress.Requests;
using Backend_PlanoriaCapstone.Extensions;

namespace Backend_PlanoriaCapstone.Controllers
{
    [ApiController]
    [Route("api/performance")]
    [Authorize]
    public class PerformanceController : ControllerBase
    {
        private readonly IPerformanceService _performanceService;

        public PerformanceController(IPerformanceService performanceService)
        {
            _performanceService = performanceService;
        }

        [HttpGet("global")]
        public async Task<IActionResult> GetGlobalStats()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _performanceService.GetGlobalStatsAsync(userId.Value);
            return Ok(result);
        }

        [HttpGet("ranking")]
        public async Task<IActionResult> GetRanking()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _performanceService.GetRankingAsync(userId.Value);
            return Ok(result);
        }

        [HttpGet("achievements")]
        public async Task<IActionResult> GetAchievements()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _performanceService.GetAchievementsAsync(userId.Value);
            return Ok(result);
        }

        [HttpGet("trends/weekly")]
        public async Task<IActionResult> GetWeeklyTrend()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _performanceService.GetWeeklyTrendAsync(userId.Value);
            return Ok(result);
        }

        [HttpGet("trends/monthly")]
        public async Task<IActionResult> GetMonthlyTrend()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _performanceService.GetMonthlyTrendAsync(userId.Value);
            return Ok(result);
        }

        [HttpGet("trends/yearly")]
        public async Task<IActionResult> GetYearlyReport([FromQuery] int year)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _performanceService.GetYearlyReportAsync(userId.Value, year);
            return Ok(result);
        }

        [HttpPost("goals")]
        public async Task<IActionResult> SetGoals([FromBody] SetGoalRequestDto request)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            await _performanceService.SetGoalsAsync(userId.Value, request);
            return Ok(new { message = "Goal set" });
        }

        [HttpGet("goals")]
        public async Task<IActionResult> GetGoals()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _performanceService.GetGoalsAsync(userId.Value);
            return Ok(result);
        }

        [HttpPut("goals/progress")]
        public async Task<IActionResult> UpdateGoalProgress([FromBody] UpdateGoalProgressRequestDto request)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            await _performanceService.UpdateGoalProgressAsync(userId.Value, request);
            return Ok(new { message = "Goal progress updated" });
        }

        [HttpGet("goals/check")]
        public async Task<IActionResult> CheckAchievement()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _performanceService.CheckAchievementAsync(userId.Value);
            return Ok(result);
        }
    }
}
