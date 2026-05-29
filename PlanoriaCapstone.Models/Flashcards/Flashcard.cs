namespace PlanoriaCapstone.Models;

public class Flashcard
{
    public int Id { get; set; }
    public int DeckId { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public string Difficulty { get; set; } = "medium";
    public string? Tags { get; set; }
    public int Position { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public FlashcardDeck? Deck { get; set; }
    public ICollection<FlashcardReview>? FlashcardReviews { get; set; }
}
