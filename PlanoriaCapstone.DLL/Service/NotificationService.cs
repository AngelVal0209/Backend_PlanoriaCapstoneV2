using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal;
using PlanoriaCapstone.DTOs.Notifications.Responses;
using PlanoriaCapstone.DTOs.Notifications.Requests;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Bll.Service;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IStudyScheduleRepository _scheduleRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    public NotificationService(
        INotificationRepository notificationRepository,
        IStudyScheduleRepository scheduleRepository,
        IActivityLogRepository activityLogRepository)
    {
        _notificationRepository = notificationRepository;
        _scheduleRepository = scheduleRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<IEnumerable<NotificationListResponseDto>> GetNotificationsAsync(int userId, bool? unreadOnly)
    {
        var notifications = await _notificationRepository.GetByUserAsync(userId, unreadOnly);
        return notifications.Select(n => new NotificationListResponseDto
        {
            Id = n.Id,
            Type = n.Type,
            Title = n.Title,
            IsRead = n.IsRead,
            CreatedAt = n.CreatedAt,
            Priority = "Normal"
        });
    }

    // ✅ CORREGIDO
    public async Task<NotificationResponseDto> GetNotificationAsync(int id)
    {
        var notification = await _notificationRepository.GetByIdAsync(id);
        if (notification == null)
            throw new KeyNotFoundException($"Notificación con ID {id} no encontrada");

        return MapToResponseDto(notification);
    }

    public async Task MarkAsReadAsync(int id)
    {
        await _notificationRepository.MarkAsReadAsync(id);
    }

    public async Task MarkAllAsReadAsync(int userId)
    {
        await _notificationRepository.MarkAllAsReadAsync(userId);
    }

    // ✅ CORREGIDO
    public async Task<bool> DeleteAsync(int id)
    {
        var notification = await _notificationRepository.GetByIdAsync(id);
        if (notification == null)
            return false;

        await _notificationRepository.DeleteAsync(id);
        return true;
    }

    public async Task<NotificationResponseDto> CreateReminderAsync(ScheduleReminderRequestDto request)
    {
        var schedule = await _scheduleRepository.GetByIdAsync(request.ScheduleId);
        if (schedule == null)
            throw new KeyNotFoundException($"Schedule with id {request.ScheduleId} not found");

        var scheduledFor = schedule.StartDatetime.AddMinutes(-request.RemindMinutesBefore);

        var notification = new Notification
        {
            UserId = schedule.UserId,
            Type = "Reminder",
            Title = $"Upcoming: {schedule.Title}",
            Message = $"Your study session '{schedule.Title}' starts in {request.RemindMinutesBefore} minutes.",
            RelatedEntityType = "StudySchedule",
            RelatedEntityId = schedule.Id,
            IsRead = false,
            ScheduledFor = scheduledFor,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _notificationRepository.CreateAsync(notification);

        return MapToResponseDto(created);
    }

    public async Task<IEnumerable<NotificationResponseDto>> GetPendingRemindersAsync()
    {
        var pending = await _notificationRepository.GetPendingRemindersAsync();
        return pending.Select(MapToResponseDto);
    }

    // ✅ CORREGIDO
    public async Task<bool> CancelReminderAsync(int notificationId)
    {
        var notification = await _notificationRepository.GetByIdAsync(notificationId);
        if (notification == null || notification.Type != "Reminder")
            return false;

        await _notificationRepository.DeleteAsync(notificationId);
        return true;
    }

    public async Task SendTestEmailAsync(int userId)
    {
        var notification = new Notification
        {
            UserId = userId,
            Type = "Test",
            Title = "Test Email",
            Message = "This is a test email notification.",
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        };

        await _notificationRepository.CreateAsync(notification);
    }

    public async Task<IEnumerable<EmailLogResponseDto>> GetEmailLogsAsync()
    {
        var logs = await _notificationRepository.GetEmailLogsAsync();
        return logs ?? Enumerable.Empty<EmailLogResponseDto>();
    }

    public async Task<bool> RetryFailedEmailAsync(int emailLogId)
    {
        var log = await _notificationRepository.GetEmailLogByIdAsync(emailLogId);
        if (log == null)
            return false;

        // Reenviar email
        await _notificationRepository.UpdateEmailLogStatusAsync(emailLogId, "retrying");
        return true;
    }

    public async Task RegisterPushDeviceAsync(int userId, RegisterPushDeviceRequestDto request)
    {
        await LogActivitySafeAsync(userId, "RegisterPushDevice", "PushDevice", null,
            $"Platform: {request.Platform}, Device: {request.DeviceName}");
    }

    public async Task UnregisterPushDeviceAsync(int deviceId)
    {
        await LogActivitySafeAsync(1, "UnregisterPushDevice", "PushDevice", deviceId,
            "Dispositivo push desregistrado");
    }

    public async Task SendPushAsync(int userId, string title, string message)
    {
        var notification = new Notification
        {
            UserId = userId,
            Type = "Push",
            Title = title,
            Message = message,
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        };

        await _notificationRepository.CreateAsync(notification);

        await LogActivitySafeAsync(userId, "SendPush", "Notification", null,
            $"Push enviado: {title}");
    }

    // ============================================
    // HELPERS
    // ============================================

    private static NotificationResponseDto MapToResponseDto(Notification notification)
    {
        return new NotificationResponseDto
        {
            Id = notification.Id,
            Type = notification.Type,
            Title = notification.Title,
            Message = notification.Message,
            RelatedEntityType = notification.RelatedEntityType,
            RelatedEntityId = notification.RelatedEntityId,
            IsRead = notification.IsRead,
            ScheduledFor = notification.ScheduledFor,
            SentAt = notification.SentAt,
            CreatedAt = notification.CreatedAt
        };
    }

    private async Task LogActivitySafeAsync(int userId, string action, string entityType,
        int? entityId, string details)
    {
        try
        {
            await _activityLogRepository.LogAsync(new ActivityLog
            {
                UserId = userId,
                Action = action,
                EntityType = entityType,
                EntityId = entityId,
                Details = details,
                CreatedAt = DateTime.UtcNow
            });
        }
        catch { }
    }
}