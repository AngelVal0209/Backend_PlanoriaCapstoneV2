using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal;

public interface IUserProgressQuizRepository
{
    Task<UserProgressQuiz?> GetByUserAndQuizAsync(int userId, int quizId);
    Task<IEnumerable<UserProgressQuiz>> GetByUserAsync(int userId);
    Task<UserProgressQuiz> CreateOrUpdateAsync(UserProgressQuiz progress);
}
