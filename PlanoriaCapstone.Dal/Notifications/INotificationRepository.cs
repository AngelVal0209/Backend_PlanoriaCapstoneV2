using PlanoriaCapstone.Models;
using PlanoriaCapstone.DTOs.Notifications.Responses;

namespace PlanoriaCapstone.Dal;

public interface INotificationRepository
{
    Task<IEnumerable<Notification>> GetByUserAsync(int userId, bool? unreadOnly = null);
    Task<Notification?> GetByIdAsync(int id);
    Task<Notification> CreateAsync(Notification notification);
    Task<bool> MarkAsReadAsync(int id);
    Task<bool> MarkAllAsReadAsync(int userId);
    Task<bool> DeleteAsync(int id);
    Task<int> GetUnreadCountAsync(int userId);
    Task<IEnumerable<Notification>> GetPendingRemindersAsync();
    Task<IEnumerable<EmailLogResponseDto>> GetEmailLogsAsync();
    Task<EmailLogResponseDto?> GetEmailLogByIdAsync(int id);
    Task UpdateEmailLogStatusAsync(int id, string status);
}
// ❌ BORRAR la clase EmailLogResponseDto de aquí