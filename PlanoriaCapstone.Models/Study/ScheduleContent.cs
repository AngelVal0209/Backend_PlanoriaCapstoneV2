namespace PlanoriaCapstone.Models;

public class ScheduleContent
{
    public int Id { get; set; }
    public int ScheduleId { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public int ContentId { get; set; }
    public int? EstimatedMinutes { get; set; }
    public bool Completed { get; set; }
    public DateTime? CompletedAt { get; set; }

    public StudySchedule? Schedule { get; set; }
}
