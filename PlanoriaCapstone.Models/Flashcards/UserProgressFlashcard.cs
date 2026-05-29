namespace PlanoriaCapstone.Models;

public class UserProgressFlashcard
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int DeckId { get; set; }
    public int TotalStudySessions { get; set; }
    public int TotalReviews { get; set; }
    public int CardsMastered { get; set; }
    public int CardsInLearning { get; set; }
    public decimal AverageEaseFactor { get; set; } = 2.50m;
    public DateTime? LastStudiedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
    public FlashcardDeck? Deck { get; set; }
}
