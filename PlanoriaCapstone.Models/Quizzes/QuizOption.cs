namespace PlanoriaCapstone.Models;

public class QuizOption
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string OptionText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public int OrderPosition { get; set; }
    public DateTime CreatedAt { get; set; }

    public QuizQuestion? Question { get; set; }
    public ICollection<QuizAnswer>? QuizAnswers { get; set; }
}
