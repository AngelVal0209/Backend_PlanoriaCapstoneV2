using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Models;
using PlanoriaCapstone.DTOs.Notifications.Responses;  // ✅ Usar el DTO original

namespace PlanoriaCapstone.Dal;

public class NotificationRepository : INotificationRepository
{
    private readonly AppDbContext _context;

    public NotificationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Notification>> GetByUserAsync(int userId, bool? unreadOnly = null)
    {
        var query = _context.Notifications.Where(n => n.UserId == userId);
        if (unreadOnly == true)
            query = query.Where(n => !n.IsRead);
        return await query.OrderByDescending(n => n.CreatedAt).ToListAsync();
    }

    public async Task<Notification?> GetByIdAsync(int id)
    {
        return await _context.Notifications.FindAsync(id);
    }

    public async Task<Notification> CreateAsync(Notification notification)
    {
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
        return notification;
    }

    public async Task<bool> MarkAsReadAsync(int id)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification == null) return false;
        notification.IsRead = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MarkAllAsReadAsync(int userId)
    {
        await _context.Notifications
            .Where(n => n.UserId == userId && !n.IsRead)
            .ExecuteUpdateAsync(s => s.SetProperty(n => n.IsRead, true));
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification == null) return false;
        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetUnreadCountAsync(int userId)
    {
        return await _context.Notifications
            .CountAsync(n => n.UserId == userId && !n.IsRead);
    }

    public async Task<IEnumerable<Notification>> GetPendingRemindersAsync()
    {
        return await _context.Notifications
            .Where(n => n.Type == "Reminder" && !n.IsRead && n.ScheduledFor <= DateTime.UtcNow)
            .OrderBy(n => n.ScheduledFor)
            .ToListAsync();
    }

    public async Task<IEnumerable<EmailLogResponseDto>> GetEmailLogsAsync()
    {
        return await _context.Notifications
            .Where(n => n.Type == "Test" || n.Type == "Email")
            .OrderByDescending(n => n.CreatedAt)
            .Select(n => new EmailLogResponseDto
            {
                Id = n.Id,
                To = "",
                Subject = n.Title,
                Status = n.IsRead ? "sent" : "pending",
                SentAt = n.CreatedAt,
                ErrorMessage = null
            })
            .ToListAsync();
    }

    public async Task<EmailLogResponseDto?> GetEmailLogByIdAsync(int id)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification == null) return null;

        return new EmailLogResponseDto
        {
            Id = notification.Id,
            To = "",
            Subject = notification.Title,
            Status = notification.IsRead ? "sent" : "pending",
            SentAt = notification.CreatedAt,
            ErrorMessage = null
        };
    }

    public async Task UpdateEmailLogStatusAsync(int id, string status)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification != null)
        {
            notification.IsRead = status == "sent";
            await _context.SaveChangesAsync();
        }
    }
}
// ❌ BORRAR la clase EmailLogResponseDto de aquí