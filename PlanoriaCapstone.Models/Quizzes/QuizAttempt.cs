namespace PlanoriaCapstone.Models;

public class QuizAttempt
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int QuizId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public decimal? ScorePercentage { get; set; }
    public bool? Passed { get; set; }
    public int? TimeSpentSeconds { get; set; }
    public DateTime CreatedAt { get; set; }

    public User? User { get; set; }
    public Quiz? Quiz { get; set; }
    public ICollection<QuizAnswer>? QuizAnswers { get; set; }
}
