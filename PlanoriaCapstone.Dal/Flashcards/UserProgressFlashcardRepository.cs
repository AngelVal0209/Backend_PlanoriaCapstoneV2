using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal;

public class UserProgressFlashcardRepository : IUserProgressFlashcardRepository
{
    private readonly AppDbContext _context;

    public UserProgressFlashcardRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserProgressFlashcard?> GetByUserAndDeckAsync(int userId, int deckId)
    {
        return await _context.UserProgressFlashcards
            .FirstOrDefaultAsync(p => p.UserId == userId && p.DeckId == deckId);
    }

    public async Task<IEnumerable<UserProgressFlashcard>> GetByUserAsync(int userId)
    {
        return await _context.UserProgressFlashcards
            .Where(p => p.UserId == userId)
            .Include(p => p.Deck)
            .ToListAsync();
    }

    public async Task<UserProgressFlashcard> CreateOrUpdateAsync(UserProgressFlashcard progress)
    {
        var existing = await _context.UserProgressFlashcards
            .FirstOrDefaultAsync(p => p.UserId == progress.UserId && p.DeckId == progress.DeckId);

        if (existing != null)
        {
            existing.TotalStudySessions = progress.TotalStudySessions;
            existing.TotalReviews = progress.TotalReviews;
            existing.CardsMastered = progress.CardsMastered;
            existing.CardsInLearning = progress.CardsInLearning;
            existing.AverageEaseFactor = progress.AverageEaseFactor;
            existing.LastStudiedAt = progress.LastStudiedAt;
            _context.UserProgressFlashcards.Update(existing);
        }
        else
        {
            _context.UserProgressFlashcards.Add(progress);
        }

        await _context.SaveChangesAsync();
        return existing ?? progress;
    }
}
