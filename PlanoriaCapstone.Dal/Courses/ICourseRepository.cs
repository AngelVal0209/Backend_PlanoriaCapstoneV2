using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(int id);
    Task<IEnumerable<Course>> GetByUserIdAsync(int userId);
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course> CreateAsync(Course course);
    Task<Course> UpdateAsync(Course course);
    Task<bool> DeleteAsync(int id);
}
