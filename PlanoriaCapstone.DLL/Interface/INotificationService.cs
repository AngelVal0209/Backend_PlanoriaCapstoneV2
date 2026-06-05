using PlanoriaCapstone.DTOs.Notifications.Responses;
using PlanoriaCapstone.DTOs.Notifications.Requests;

namespace PlanoriaCapstone.Bll.Interface;

public interface INotificationService
{
    Task<IEnumerable<NotificationListResponseDto>> GetNotificationsAsync(int userId, bool? unreadOnly);
    Task<NotificationResponseDto> GetNotificationAsync(int id);
    Task MarkAsReadAsync(int id);
    Task MarkAllAsReadAsync(int userId);
    Task<bool> DeleteAsync(int id);
    Task<NotificationResponseDto> CreateReminderAsync(ScheduleReminderRequestDto request);
    Task<IEnumerable<NotificationResponseDto>> GetPendingRemindersAsync();
    Task<bool> CancelReminderAsync(int notificationId);
    Task SendTestEmailAsync(int userId);
    Task<IEnumerable<EmailLogResponseDto>> GetEmailLogsAsync();
    Task<bool> RetryFailedEmailAsync(int emailLogId);
    Task RegisterPushDeviceAsync(int userId, RegisterPushDeviceRequestDto request);
    Task UnregisterPushDeviceAsync(int deviceId);
    Task SendPushAsync(int userId, string title, string message);
}
