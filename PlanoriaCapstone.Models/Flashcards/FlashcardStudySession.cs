namespace PlanoriaCapstone.Models;

public class FlashcardStudySession
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int DeckId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public int CardsReviewed { get; set; }
    public int CardsKnown { get; set; }
    public int CardsUnknown { get; set; }
    public string SessionType { get; set; } = "normal";

    public User? User { get; set; }
    public FlashcardDeck? Deck { get; set; }
    public ICollection<FlashcardReview>? FlashcardReviews { get; set; }
}
