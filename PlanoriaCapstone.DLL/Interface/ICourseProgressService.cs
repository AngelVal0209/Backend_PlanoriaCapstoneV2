using PlanoriaCapstone.DTOs.Progress.Responses.Exam;

namespace PlanoriaCapstone.Bll.Interface;

public interface ICourseProgressService
{
    Task<CourseExamProgressResponseDto> GetExamProgressAsync(int userId, int courseId);
    Task<ReadinessScoreResponseDto> GetReadinessScoreAsync(int userId, int courseId);
    Task<IEnumerable<object>> GetRecommendationsAsync(int userId, int courseId);
    Task<IEnumerable<ReadinessHistoryResponseDto>> GetReadinessHistoryAsync(int userId, int courseId);
    Task<IEnumerable<ReadinessHistoryResponseDto>> GetReadinessTrendAsync(int userId, int courseId);
    Task<object> GetPredictionsAsync(int userId, int courseId);
    Task<IEnumerable<WeaknessesResponseDto>> IdentifyWeaknessesAsync(int userId, int courseId);
    Task<IEnumerable<WeaknessesResponseDto>> GetPriorityTopicsAsync(int userId, int courseId);
    Task<IEnumerable<WeaknessesResponseDto>> SuggestFocusAsync(int userId, int courseId);
}
