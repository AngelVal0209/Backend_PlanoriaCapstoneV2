using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal;

public interface IQuizRepository
{
    Task<Quiz?> GetByIdAsync(int id);
    Task<IEnumerable<Quiz>> GetByCourseIdAsync(int courseId);
    Task<IEnumerable<Quiz>> GetAllAsync();
    Task<Quiz> CreateAsync(Quiz quiz);
    Task<Quiz> UpdateAsync(Quiz quiz);
    Task<QuizQuestion> AddQuestionAsync(QuizQuestion question);  // ✅ NUEVO
    Task<bool> DeleteAsync(int id);
}