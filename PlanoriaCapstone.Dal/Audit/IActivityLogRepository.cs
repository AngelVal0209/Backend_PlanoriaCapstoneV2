using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal;

public interface IActivityLogRepository
{
    Task LogAsync(ActivityLog log);
    Task<IEnumerable<ActivityLog>> GetByUserAsync(int userId, int limit = 50);
    Task<IEnumerable<ActivityLog>> GetByEntityAsync(string entityType, int entityId);
}
