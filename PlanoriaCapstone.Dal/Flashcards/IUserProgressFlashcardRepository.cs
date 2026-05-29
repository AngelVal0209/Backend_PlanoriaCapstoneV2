using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal;

public interface IUserProgressFlashcardRepository
{
    Task<UserProgressFlashcard?> GetByUserAndDeckAsync(int userId, int deckId);
    Task<IEnumerable<UserProgressFlashcard>> GetByUserAsync(int userId);
    Task<UserProgressFlashcard> CreateOrUpdateAsync(UserProgressFlashcard progress);
}
