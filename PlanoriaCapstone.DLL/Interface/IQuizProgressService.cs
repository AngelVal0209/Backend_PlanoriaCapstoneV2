using PlanoriaCapstone.DTOs.Progress.Responses.Quizzes;

namespace PlanoriaCapstone.Bll.Interface;

public interface IQuizProgressService
{
    Task<QuizProgressResponseDto> GetByQuizAsync(int userId, int quizId);
    Task<IEnumerable<QuizProgressResponseDto>> GetByCourseAsync(int userId, int courseId);
    Task<IEnumerable<QuizProgressResponseDto>> GetOverallAsync(int userId);
    Task<QuizPerformanceResponseDto> GetAverageScoreAsync(int userId, int? quizId);
    Task<IEnumerable<string>> GetWeakTopicsAsync(int userId, int courseId);
    Task<object> GetImprovementAsync(int userId, int quizId);
    Task<QuizComparisonResponseDto> CompareQuizzesAsync(int userId, int quizId1, int quizId2);
    Task<object> CompareCoursesAsync(int userId, int courseId1, int courseId2);
    Task<object> CompareTimeframesAsync(int userId, DateTime from1, DateTime to1, DateTime from2, DateTime to2);
}
