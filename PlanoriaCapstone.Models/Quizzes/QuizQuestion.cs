namespace PlanoriaCapstone.Models;

public class QuizQuestion
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string QuestionType { get; set; } = string.Empty;
    public string? Explanation { get; set; }
    public decimal Points { get; set; } = 1.00m;
    public int OrderPosition { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Quiz? Quiz { get; set; }
    public ICollection<QuizOption>? QuizOptions { get; set; }
    public ICollection<QuizAnswer>? QuizAnswers { get; set; }
}
