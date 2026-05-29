using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal;

public interface IQuizRepository
{
    Task<Quiz?> GetByIdAsync(int id);
    Task<IEnumerable<Quiz>> GetByCourseIdAsync(int courseId);
    Task<Quiz> CreateAsync(Quiz quiz);
    Task<Quiz> UpdateAsync(Quiz quiz);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Quiz>> GetAllAsync();
}
