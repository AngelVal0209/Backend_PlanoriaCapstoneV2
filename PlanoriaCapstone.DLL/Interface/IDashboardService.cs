using PlanoriaCapstone.DTOs.Dashboard.Responses;
using PlanoriaCapstone.DTOs.Dashboard.Requests;

namespace PlanoriaCapstone.Bll.Interface;

public interface IDashboardService
{
    Task<DashboardOverviewResponseDto> GetSummaryAsync(int userId);
    Task<IEnumerable<ActivityItemResponseDto>> GetRecentActivityAsync(int userId, int limit);
    Task<IEnumerable<UpcomingDeadlineResponseDto>> GetUpcomingDeadlinesAsync(int userId, int days);
    Task<MetricCardResponseDto> GetStudyTimeAsync(int userId, string period);
    Task<MetricCardResponseDto> GetCardsReviewedAsync(int userId, string period);
    Task<MetricCardResponseDto> GetQuizzesCompletedAsync(int userId, string period);
    Task<ChartDataResponseDto> GetProgressChartAsync(int userId, int? courseId, string period);
    Task<HeatmapDataResponseDto> GetHeatmapDataAsync(int userId, int? year);
    Task<DistributionDataResponseDto> GetDistributionDataAsync(int userId, int? courseId);
    Task<byte[]> ExportToPdfAsync(int userId, ExportDashboardRequestDto request);
    Task<string> ExportToCsvAsync(int userId, ExportDashboardRequestDto request);
    Task<object> GenerateReportAsync(int userId, ExportDashboardRequestDto request);
}
