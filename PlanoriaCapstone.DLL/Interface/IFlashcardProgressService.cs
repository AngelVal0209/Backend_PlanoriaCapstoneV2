using PlanoriaCapstone.DTOs.Progress.Responses.Flashcards;

namespace PlanoriaCapstone.Bll.Interface;

public interface IFlashcardProgressService
{
    Task<FlashcardProgressResponseDto> GetByDeckAsync(int userId, int deckId);
    Task<IEnumerable<FlashcardProgressResponseDto>> GetByCourseAsync(int userId, int courseId);
    Task<IEnumerable<FlashcardProgressResponseDto>> GetOverallAsync(int userId);
    Task<FlashcardMasteryResponseDto> GetMasteryLevelAsync(int userId, int deckId);
    Task<MasteryTrendResponseDto> GetMasteryTrendAsync(int userId, int deckId);
    Task<object> GetPredictionsAsync(int userId, int deckId);
    Task<IEnumerable<WeeklyFlashcardProgressResponseDto>> GetTimelineAsync(int userId, int deckId);
    Task<IEnumerable<WeeklyFlashcardProgressResponseDto>> GetWeeklyProgressAsync(int userId);
    Task<object> GetMonthlyReportAsync(int userId, int month, int year);
}
