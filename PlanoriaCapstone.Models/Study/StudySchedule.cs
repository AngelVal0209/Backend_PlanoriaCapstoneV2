namespace PlanoriaCapstone.Models;

public class StudySchedule
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime StartDatetime { get; set; }
    public DateTime EndDatetime { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
    public bool NotificationSent { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
    public ICollection<ScheduleInterval>? ScheduleIntervals { get; set; }
    public ICollection<ScheduleContent>? ScheduleContents { get; set; }
}
