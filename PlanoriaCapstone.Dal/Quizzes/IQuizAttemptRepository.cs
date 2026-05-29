using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal;

public interface IQuizAttemptRepository
{
    Task<QuizAttempt?> GetByIdAsync(int id);
    Task<IEnumerable<QuizAttempt>> GetByUserAsync(int userId);
    Task<IEnumerable<QuizAttempt>> GetByQuizIdAsync(int quizId);
    Task<QuizAttempt> CreateAsync(QuizAttempt attempt);
    Task<QuizAttempt> UpdateAsync(QuizAttempt attempt);
    Task<QuizAnswer> AddAnswerAsync(QuizAnswer answer);
    Task<IEnumerable<QuizAnswer>> GetAnswersByAttemptAsync(int attemptId);
}
