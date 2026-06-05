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

    public async Task<NotificationResponseDto> GetNotificationAsync(int id)
    {
        throw new NotImplementedException("Get notification by id requires repository method.");
    }

    public async Task MarkAsReadAsync(int id)
    {
        await _notificationRepository.MarkAsReadAsync(id);
    }

    public async Task MarkAllAsReadAsync(int userId)
    {
        await _notificationRepository.MarkAllAsReadAsync(userId);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException("Notification delete requires repository method.");
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
        return Enumerable.Empty<NotificationResponseDto>();
    }

    public async Task<bool> CancelReminderAsync(int notificationId)
    {
        throw new NotImplementedException("Cancel reminder requires repository method.");
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
        return Enumerable.Empty<EmailLogResponseDto>();
    }

    public async Task<bool> RetryFailedEmailAsync(int emailLogId)
    {
        return await Task.FromResult(true);
    }

    public async Task RegisterPushDeviceAsync(int userId, RegisterPushDeviceRequestDto request)
    {
        await _activityLogRepository.LogAsync(new ActivityLog
        {
            UserId = userId,
            Action = "RegisterPushDevice",
            EntityType = "PushDevice",
            Details = $"Platform: {request.Platform}, Device: {request.DeviceName}",
            CreatedAt = DateTime.UtcNow
        });
    }

    public async Task UnregisterPushDeviceAsync(int deviceId)
    {
        await Task.CompletedTask;
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
    }

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
}
