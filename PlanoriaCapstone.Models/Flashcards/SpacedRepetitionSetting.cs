namespace PlanoriaCapstone.Models;

public class SpacedRepetitionSetting
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? DeckId { get; set; }
    public int InitialIntervalDays { get; set; } = 1;
    public int MaxIntervalDays { get; set; } = 365;
    public decimal EasyBonus { get; set; } = 1.30m;
    public decimal HardPenalty { get; set; } = 1.20m;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
    public FlashcardDeck? Deck { get; set; }
}
