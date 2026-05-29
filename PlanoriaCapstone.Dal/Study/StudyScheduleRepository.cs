using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal;

public class StudyScheduleRepository : IStudyScheduleRepository
{
    private readonly AppDbContext _context;

    public StudyScheduleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<StudySchedule?> GetByIdAsync(int id)
    {
        return await _context.StudySchedules
            .Include(s => s.ScheduleIntervals)
            .Include(s => s.ScheduleContents)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<StudySchedule>> GetByUserAsync(int userId)
    {
        return await _context.StudySchedules
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.StartDatetime)
            .ToListAsync();
    }

    public async Task<IEnumerable<StudySchedule>> GetByDateRangeAsync(int userId, DateTime from, DateTime to)
    {
        return await _context.StudySchedules
            .Where(s => s.UserId == userId && s.StartDatetime >= from && s.EndDatetime <= to)
            .OrderBy(s => s.StartDatetime)
            .ToListAsync();
    }

    public async Task<StudySchedule> CreateAsync(StudySchedule schedule)
    {
        _context.StudySchedules.Add(schedule);
        await _context.SaveChangesAsync();
        return schedule;
    }

    public async Task<StudySchedule> UpdateAsync(StudySchedule schedule)
    {
        _context.StudySchedules.Update(schedule);
        await _context.SaveChangesAsync();
        return schedule;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var schedule = await _context.StudySchedules.FindAsync(id);
        if (schedule == null) return false;
        _context.StudySchedules.Remove(schedule);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ScheduleInterval> AddIntervalAsync(ScheduleInterval interval)
    {
        _context.ScheduleIntervals.Add(interval);
        await _context.SaveChangesAsync();
        return interval;
    }

    public async Task<ScheduleContent> AddContentAsync(ScheduleContent content)
    {
        _context.ScheduleContents.Add(content);
        await _context.SaveChangesAsync();
        return content;
    }
}
