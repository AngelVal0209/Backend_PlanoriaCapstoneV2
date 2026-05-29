using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal;

public interface INotificationRepository
{
    Task<IEnumerable<Notification>> GetByUserAsync(int userId, bool? unreadOnly = null);
    Task<Notification> CreateAsync(Notification notification);
    Task<bool> MarkAsReadAsync(int id);
    Task<bool> MarkAllAsReadAsync(int userId);
    Task<int> GetUnreadCountAsync(int userId);
}
