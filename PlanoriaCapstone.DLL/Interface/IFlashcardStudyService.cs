using PlanoriaCapstone.DTOs.Flashcards.Cards.Responses;
using PlanoriaCapstone.DTOs.Flashcards.Study.Requests;
using PlanoriaCapstone.DTOs.Flashcards.Study.Responses;

namespace PlanoriaCapstone.Bll.Interface;

public interface IFlashcardStudyService
{
    Task<StudySessionResponseDto> StartSessionAsync(int userId, StartStudySessionRequestDto request);
    Task<NextCardResponseDto> GetNextCardAsync(int sessionId);
    Task SubmitAnswerAsync(int userId, SubmitFlashcardAnswerRequestDto request);
    Task<StudySessionResponseDto> EndSessionAsync(int userId, EndStudySessionRequestDto request);
    Task<DueCardsResponseDto> GetDueCardsAsync(int userId, int deckId);
    Task<IEnumerable<FlashcardResponseDto>> GetOverdueCardsAsync(int userId, int deckId);
    Task ScheduleReviewAsync(int userId, ScheduleReviewRequestDto request);
    Task<IEnumerable<StudySessionResponseDto>> GetSessionHistoryAsync(int userId, int? deckId);
    Task<StudySessionResponseDto> GetSessionAsync(int sessionId);
    Task<object> GetSessionSummaryAsync(int sessionId);
    Task<object> GetPerformanceAsync(int userId, int deckId);
}
