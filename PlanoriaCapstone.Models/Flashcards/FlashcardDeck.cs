namespace PlanoriaCapstone.Models;

public class FlashcardDeck
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int TotalCards { get; set; }
    public bool SpacedRepetitionEnabled { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Course? Course { get; set; }
    public ICollection<Flashcard>? Flashcards { get; set; }
    public ICollection<FlashcardStudySession>? FlashcardStudySessions { get; set; }
    public ICollection<UserProgressFlashcard>? UserProgressFlashcards { get; set; }
    public ICollection<SpacedRepetitionSetting>? SpacedRepetitionSettings { get; set; }
}
