namespace PlanoriaCapstone.Models;

public class UserProgressQuiz
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int QuizId { get; set; }
    public int TotalAttempts { get; set; }
    public decimal BestScore { get; set; }
    public decimal AverageScore { get; set; }
    public DateTime? LastAttemptAt { get; set; }
    public int PassedCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
    public Quiz? Quiz { get; set; }
}
