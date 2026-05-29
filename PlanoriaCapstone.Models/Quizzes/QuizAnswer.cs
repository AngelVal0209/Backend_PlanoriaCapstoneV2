namespace PlanoriaCapstone.Models;

public class QuizAnswer
{
    public int Id { get; set; }
    public int AttemptId { get; set; }
    public int QuestionId { get; set; }
    public int? SelectedOptionId { get; set; }
    public string? ShortAnswerText { get; set; }
    public bool IsCorrect { get; set; }
    public decimal PointsEarned { get; set; }
    public DateTime AnsweredAt { get; set; }

    public QuizAttempt? Attempt { get; set; }
    public QuizQuestion? Question { get; set; }
    public QuizOption? SelectedOption { get; set; }
}
