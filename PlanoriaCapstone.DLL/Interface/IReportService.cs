using PlanoriaCapstone.DTOs.Reports.Responses;
using PlanoriaCapstone.DTOs.System.Requests;

namespace PlanoriaCapstone.Bll.Interface;

public interface IReportService
{
    Task<StudyReportResponseDto> GenerateStudyReportAsync(int userId, DateTime from, DateTime to);
    Task<object> GetStudyInsightsAsync(int userId);
    Task<object> GeneratePerformanceReportAsync(int userId, DateTime from, DateTime to);
    Task<object> GetPerformanceSummaryAsync(int userId);
    Task<CustomReportResponseDto> CreateCustomReportAsync(int userId, CreateCustomReportRequestDto request);
    Task<IEnumerable<ReportTemplateResponseDto>> SaveTemplateAsync(int userId, ReportTemplateResponseDto request);
    Task<IEnumerable<ReportTemplateResponseDto>> GetTemplatesAsync(int userId);
    Task<object> ScheduleReportAsync(int userId, CreateCustomReportRequestDto request);
}
