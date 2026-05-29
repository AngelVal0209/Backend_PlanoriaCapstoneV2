namespace PlanoriaCapstone.Models;

public class ExamReadinessScore
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public decimal Score { get; set; }
    public int? DaysUntilExam { get; set; }
    public DateTime CalculatedAt { get; set; }

    public User? User { get; set; }
    public Course? Course { get; set; }
}
