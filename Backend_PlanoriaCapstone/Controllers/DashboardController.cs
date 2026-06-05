using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.Dashboard.Requests;
using Backend_PlanoriaCapstone.Extensions;

namespace Backend_PlanoriaCapstone.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("overview")]
        public async Task<IActionResult> GetSummary()
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _dashboardService.GetSummaryAsync(userId.Value);
            return Ok(result);
        }

        [HttpGet("activity")]
        public async Task<IActionResult> GetRecentActivity([FromQuery] int limit = 20)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _dashboardService.GetRecentActivityAsync(userId.Value, limit);
            return Ok(result);
        }

        [HttpGet("deadlines")]
        public async Task<IActionResult> GetUpcomingDeadlines([FromQuery] int days = 30)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _dashboardService.GetUpcomingDeadlinesAsync(userId.Value, days);
            return Ok(result);
        }

        [HttpGet("metrics/study-time")]
        public async Task<IActionResult> GetStudyTime([FromQuery] string period = "today")
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _dashboardService.GetStudyTimeAsync(userId.Value, period);
            return Ok(result);
        }

        [HttpGet("metrics/cards-reviewed")]
        public async Task<IActionResult> GetCardsReviewed([FromQuery] string period = "today")
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _dashboardService.GetCardsReviewedAsync(userId.Value, period);
            return Ok(result);
        }

        [HttpGet("metrics/quizzes-completed")]
        public async Task<IActionResult> GetQuizzesCompleted([FromQuery] string period = "today")
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _dashboardService.GetQuizzesCompletedAsync(userId.Value, period);
            return Ok(result);
        }

        [HttpGet("charts/progress")]
        public async Task<IActionResult> GetProgressChart([FromQuery] int? courseId, [FromQuery] string period = "week")
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _dashboardService.GetProgressChartAsync(userId.Value, courseId, period);
            return Ok(result);
        }

        [HttpGet("charts/heatmap")]
        public async Task<IActionResult> GetHeatmapData([FromQuery] int? year)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _dashboardService.GetHeatmapDataAsync(userId.Value, year);
            return Ok(result);
        }

        [HttpGet("charts/distribution")]
        public async Task<IActionResult> GetDistributionData([FromQuery] int? courseId)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _dashboardService.GetDistributionDataAsync(userId.Value, courseId);
            return Ok(result);
        }

        [HttpGet("export/pdf")]
        public async Task<IActionResult> ExportToPdf([FromQuery] ExportDashboardRequestDto request)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _dashboardService.ExportToPdfAsync(userId.Value, request);
            return File(result, "application/pdf", "dashboard-report.pdf");
        }

        [HttpGet("export/csv")]
        public async Task<IActionResult> ExportToCsv([FromQuery] ExportDashboardRequestDto request)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _dashboardService.ExportToCsvAsync(userId.Value, request);
            return File(System.Text.Encoding.UTF8.GetBytes(result), "text/csv", "dashboard-report.csv");
        }

        [HttpPost("export/report")]
        public async Task<IActionResult> GenerateReport([FromBody] ExportDashboardRequestDto request)
        {
            var userId = User.ObtenerUserId();
            if (userId == null) return Unauthorized();
            var result = await _dashboardService.GenerateReportAsync(userId.Value, request);
            return Ok(result);
        }
    }
}
