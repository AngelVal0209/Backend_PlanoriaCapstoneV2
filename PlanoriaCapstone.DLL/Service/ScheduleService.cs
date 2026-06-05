using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal;
using PlanoriaCapstone.DTOs.Cronograma.Responses;
using PlanoriaCapstone.DTOs.Cronograma.Requests;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Bll.Service;

public class ScheduleService : IScheduleService
{
    private readonly IStudyScheduleRepository _scheduleRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    public ScheduleService(
        IStudyScheduleRepository scheduleRepository,
        ICourseRepository courseRepository,
        IActivityLogRepository activityLogRepository)
    {
        _scheduleRepository = scheduleRepository;
        _courseRepository = courseRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<ScheduleResponseDto> GetByIdAsync(int id)
    {
        var schedule = await _scheduleRepository.GetByIdAsync(id);
        if (schedule == null)
            throw new KeyNotFoundException($"Schedule with id {id} not found");

        return MapToResponseDto(schedule);
    }

    public async Task<IEnumerable<ScheduleListResponseDto>> GetByUserAsync(int userId)
    {
        var schedules = await _scheduleRepository.GetByUserAsync(userId);
        return schedules.Select(MapToListDto);
    }

    public async Task<IEnumerable<ScheduleListResponseDto>> GetByDateRangeAsync(int userId, DateTime from, DateTime to)
    {
        var schedules = await _scheduleRepository.GetByDateRangeAsync(userId, from, to);
        return schedules.Select(MapToListDto);
    }

    public async Task<ScheduleResponseDto> CreateAsync(int userId, CreateScheduleRequestDto request)
    {
        var schedule = new StudySchedule
        {
            UserId = userId,
            Title = request.Title,
            StartDatetime = request.StartDateTime,
            EndDatetime = request.EndDateTime,
            IsCompleted = false,
            NotificationSent = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _scheduleRepository.CreateAsync(schedule);

        if (request.Intervals != null)
        {
            foreach (var interval in request.Intervals)
            {
                await _scheduleRepository.AddIntervalAsync(new ScheduleInterval
                {
                    ScheduleId = created.Id,
                    IntervalType = interval.IntervalType,
                    DurationMinutes = interval.DurationMinutes,
                    OrderPosition = interval.OrderPosition
                });
            }
        }

        if (request.Content != null)
        {
            foreach (var content in request.Content)
            {
                await _scheduleRepository.AddContentAsync(new ScheduleContent
                {
                    ScheduleId = created.Id,
                    ContentType = content.ContentType,
                    ContentId = content.ContentId,
                    EstimatedMinutes = content.EstimatedMinutes > 0 ? content.EstimatedMinutes : null,
                    Completed = false
                });
            }
        }

        await _activityLogRepository.LogAsync(new ActivityLog
        {
            UserId = userId,
            Action = "CreateSchedule",
            EntityType = "StudySchedule",
            EntityId = created.Id,
            CreatedAt = DateTime.UtcNow
        });

        return await GetByIdAsync(created.Id);
    }

    public async Task<ScheduleResponseDto> UpdateAsync(int id, UpdateScheduleRequestDto request)
    {
        var schedule = await _scheduleRepository.GetByIdAsync(id);
        if (schedule == null)
            throw new KeyNotFoundException($"Schedule with id {id} not found");

        if (request.Title != null) schedule.Title = request.Title;
        if (request.StartDateTime.HasValue) schedule.StartDatetime = request.StartDateTime.Value;
        if (request.EndDateTime.HasValue) schedule.EndDatetime = request.EndDateTime.Value;
        if (request.IsCompleted.HasValue) schedule.IsCompleted = request.IsCompleted.Value;
        schedule.UpdatedAt = DateTime.UtcNow;

        var updated = await _scheduleRepository.UpdateAsync(schedule);
        return MapToResponseDto(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _scheduleRepository.DeleteAsync(id);
    }

    public async Task<object> GetMonthViewAsync(int userId, int year, int month)
    {
        var monthStart = new DateTime(year, month, 1);
        var monthEnd = monthStart.AddMonths(1).AddDays(-1);
        var schedules = await _scheduleRepository.GetByDateRangeAsync(userId, monthStart, monthEnd);

        return new
        {
            Year = year,
            Month = month,
            Days = Enumerable.Range(1, monthEnd.Day).Select(day => new
            {
                Date = new DateTime(year, month, day),
                Schedules = schedules.Where(s => s.StartDatetime.Date <= new DateTime(year, month, day) && s.EndDatetime.Date >= new DateTime(year, month, day)).Select(MapToListDto)
            })
        };
    }

    public async Task<object> GetWeekViewAsync(int userId, int year, int week)
    {
        var weekStart = new DateTime(year, 1, 1).AddDays((week - 1) * 7);
        var weekEnd = weekStart.AddDays(7);
        var schedules = await _scheduleRepository.GetByDateRangeAsync(userId, weekStart, weekEnd);

        return new CalendarWeekResponseDto
        {
            WeekStart = weekStart,
            WeekEnd = weekEnd,
            Days = Enumerable.Range(0, 7).Select(i => new CalendarDayResponseDto
            {
                Date = weekStart.AddDays(i),
                Schedules = schedules.Where(s => s.StartDatetime.Date <= weekStart.AddDays(i) && s.EndDatetime.Date >= weekStart.AddDays(i)).Select(MapToListDto).ToList(),
                TotalStudyMinutes = 0,
                CompletedSessionsCount = schedules.Count(s => s.IsCompleted)
            }).ToList()
        };
    }

    public async Task<object> GetDayViewAsync(int userId, DateTime date)
    {
        var schedules = await _scheduleRepository.GetByDateRangeAsync(userId, date.Date, date.Date.AddDays(1));

        return new CalendarDayResponseDto
        {
            Date = date,
            Schedules = schedules.Select(MapToListDto).ToList(),
            TotalStudyMinutes = (int)schedules.Sum(s => (s.EndDatetime - s.StartDatetime).TotalMinutes),
            CompletedSessionsCount = schedules.Count(s => s.IsCompleted)
        };
    }

    public async Task<object> GetAgendaAsync(int userId, DateTime from, DateTime to)
    {
        var schedules = await _scheduleRepository.GetByDateRangeAsync(userId, from, to);
        return schedules.OrderBy(s => s.StartDatetime).Select(s => new
        {
            Id = s.Id,
            Title = s.Title,
            StartDateTime = s.StartDatetime,
            EndDateTime = s.EndDatetime,
            IsCompleted = s.IsCompleted,
            DurationMinutes = (int)(s.EndDatetime - s.StartDatetime).TotalMinutes
        });
    }

    public async Task CreateRecurringAsync(int userId, CreateScheduleRequestDto request, string recurrence)
    {
        var currentStart = request.StartDateTime;
        var count = recurrence.ToLower() switch
        {
            "daily" => 7,
            "weekly" => 4,
            "biweekly" => 2,
            "monthly" => 3,
            _ => 1
        };

        for (int i = 0; i < count; i++)
        {
            var copy = new CreateScheduleRequestDto
            {
                Title = request.Title,
                StartDateTime = currentStart,
                EndDateTime = currentStart.Add(request.EndDateTime - request.StartDateTime),
                CourseIds = request.CourseIds,
                Intervals = request.Intervals,
                Content = request.Content
            };

            await CreateAsync(userId, copy);

            currentStart = recurrence.ToLower() switch
            {
                "daily" => currentStart.AddDays(1),
                "weekly" => currentStart.AddDays(7),
                "biweekly" => currentStart.AddDays(14),
                "monthly" => currentStart.AddMonths(1),
                _ => currentStart
            };
        }
    }

    public async Task UpdateRecurringAsync(int scheduleId, UpdateScheduleRequestDto request)
    {
        await UpdateAsync(scheduleId, request);
    }

    public async Task DeleteRecurringAsync(int scheduleId)
    {
        await DeleteAsync(scheduleId);
    }

    public async Task MarkCompleteAsync(int scheduleId)
    {
        var schedule = await _scheduleRepository.GetByIdAsync(scheduleId);
        if (schedule == null) throw new KeyNotFoundException();

        schedule.IsCompleted = true;
        schedule.CompletedAt = DateTime.UtcNow;
        schedule.UpdatedAt = DateTime.UtcNow;
        await _scheduleRepository.UpdateAsync(schedule);
    }

    public async Task MarkIncompleteAsync(int scheduleId)
    {
        var schedule = await _scheduleRepository.GetByIdAsync(scheduleId);
        if (schedule == null) throw new KeyNotFoundException();

        schedule.IsCompleted = false;
        schedule.CompletedAt = null;
        schedule.UpdatedAt = DateTime.UtcNow;
        await _scheduleRepository.UpdateAsync(schedule);
    }

    public async Task BulkCompleteAsync(List<int> scheduleIds)
    {
        foreach (var id in scheduleIds)
        {
            await MarkCompleteAsync(id);
        }
    }

    private ScheduleResponseDto MapToResponseDto(StudySchedule schedule)
    {
        return new ScheduleResponseDto
        {
            Id = schedule.Id,
            Title = schedule.Title,
            StartDateTime = schedule.StartDatetime,
            EndDateTime = schedule.EndDatetime,
            IsCompleted = schedule.IsCompleted,
            CompletedAt = schedule.CompletedAt,
            TotalDurationMinutes = (int)(schedule.EndDatetime - schedule.StartDatetime).TotalMinutes,
            CourseIds = new List<int>(),
            Intervals = schedule.ScheduleIntervals?.Select(i => new IntervalResponseDto
            {
                Id = i.Id,
                IntervalType = i.IntervalType,
                DurationMinutes = i.DurationMinutes,
                OrderPosition = i.OrderPosition,
                StartedAt = i.StartedAt,
                EndedAt = i.EndedAt,
                IsCompleted = i.EndedAt.HasValue
            }).ToList() ?? new List<IntervalResponseDto>(),
            Content = schedule.ScheduleContents?.Select(c => new ScheduleContentResponseDto
            {
                Id = c.Id,
                ContentType = c.ContentType,
                ContentId = c.ContentId,
                ContentName = string.Empty,
                EstimatedMinutes = c.EstimatedMinutes ?? 0,
                Completed = c.Completed,
                CompletedAt = c.CompletedAt
            }).ToList() ?? new List<ScheduleContentResponseDto>()
        };
    }

    private ScheduleListResponseDto MapToListDto(StudySchedule schedule)
    {
        return new ScheduleListResponseDto
        {
            Id = schedule.Id,
            Title = schedule.Title,
            StartDateTime = schedule.StartDatetime,
            EndDateTime = schedule.EndDatetime,
            IsCompleted = schedule.IsCompleted,
            ProgressPercentage = schedule.IsCompleted ? 100 : 0,
            CourseName = string.Empty
        };
    }
}
