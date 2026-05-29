namespace PlanoriaCapstone.Models;

public class FlashcardReview
{
    public int Id { get; set; }
    public int FlashcardId { get; set; }
    public int SessionId { get; set; }
    public int UserId { get; set; }
    public bool KnewIt { get; set; }
    public int? ResponseTimeMs { get; set; }
    public decimal EaseFactor { get; set; } = 2.5m;
    public int IntervalDays { get; set; } = 1;
    public DateTime NextReviewDate { get; set; }
    public DateTime ReviewedAt { get; set; }

    public Flashcard? Flashcard { get; set; }
    public FlashcardStudySession? Session { get; set; }
    public User? User { get; set; }
}
