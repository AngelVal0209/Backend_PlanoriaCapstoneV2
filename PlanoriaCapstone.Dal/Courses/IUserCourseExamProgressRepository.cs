using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal;

public interface IUserCourseExamProgressRepository
{
    Task<UserCourseExamProgress?> GetByUserAndCourseAsync(int userId, int courseId);
    Task<UserCourseExamProgress> CreateOrUpdateAsync(UserCourseExamProgress progress);
    Task<IEnumerable<ExamReadinessScore>> GetReadinessHistoryAsync(int userId, int courseId);
    Task AddReadinessScoreAsync(ExamReadinessScore score);
}
