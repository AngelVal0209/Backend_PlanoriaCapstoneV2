namespace PlanoriaCapstone.Models;

public class ScheduleInterval
{
    public int Id { get; set; }
    public int ScheduleId { get; set; }
    public string IntervalType { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public int OrderPosition { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }

    public StudySchedule? Schedule { get; set; }
}
