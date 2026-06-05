using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal;
using PlanoriaCapstone.DTOs.Progress.Responses.Flashcards;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Bll.Service;

public class FlashcardProgressService : IFlashcardProgressService
{
    private readonly IUserProgressFlashcardRepository _progressRepository;
    private readonly IFlashcardDeckRepository _deckRepository;
    private readonly IFlashcardRepository _flashcardRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    public FlashcardProgressService(
        IUserProgressFlashcardRepository progressRepository,
        IFlashcardDeckRepository deckRepository,
        IFlashcardRepository flashcardRepository,
        ICourseRepository courseRepository,
        IActivityLogRepository activityLogRepository)
    {
        _progressRepository = progressRepository;
        _deckRepository = deckRepository;
        _flashcardRepository = flashcardRepository;
        _courseRepository = courseRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<FlashcardProgressResponseDto> GetByDeckAsync(int userId, int deckId)
    {
        var progress = await _progressRepository.GetByUserAndDeckAsync(userId, deckId);
        var deck = await _deckRepository.GetByIdAsync(deckId);

        return MapToProgressDto(progress, deck?.Name ?? "Unknown");
    }

    public async Task<IEnumerable<FlashcardProgressResponseDto>> GetByCourseAsync(int userId, int courseId)
    {
        var decks = await _deckRepository.GetByCourseIdAsync(courseId);
        var result = new List<FlashcardProgressResponseDto>();

        foreach (var deck in decks)
        {
            var progress = await _progressRepository.GetByUserAndDeckAsync(userId, deck.Id);
            result.Add(MapToProgressDto(progress, deck.Name));
        }

        return result;
    }

    public async Task<IEnumerable<FlashcardProgressResponseDto>> GetOverallAsync(int userId)
    {
        var allProgress = await _progressRepository.GetByUserAsync(userId);
        var result = new List<FlashcardProgressResponseDto>();

        foreach (var progress in allProgress)
        {
            var deck = await _deckRepository.GetByIdAsync(progress.DeckId);
            result.Add(MapToProgressDto(progress, deck?.Name ?? "Unknown"));
        }

        return result;
    }

    public async Task<FlashcardMasteryResponseDto> GetMasteryLevelAsync(int userId, int deckId)
    {
        var progress = await _progressRepository.GetByUserAndDeckAsync(userId, deckId);
        var cards = await _flashcardRepository.GetByDeckIdAsync(deckId);
        var firstCard = cards.FirstOrDefault();

        return new FlashcardMasteryResponseDto
        {
            FlashcardId = firstCard?.Id ?? 0,
            Question = firstCard?.Question ?? string.Empty,
            EaseFactor = progress?.AverageEaseFactor ?? 2.50m,
            RepetitionCount = progress?.TotalReviews ?? 0,
            LastReviewDate = progress?.LastStudiedAt,
            NextReviewDate = progress?.LastStudiedAt?.AddDays(1),
            MasteryLevel = GetMasteryLevel(progress)
        };
    }

    public async Task<MasteryTrendResponseDto> GetMasteryTrendAsync(int userId, int deckId)
    {
        var progress = await _progressRepository.GetByUserAndDeckAsync(userId, deckId);
        var now = DateTime.UtcNow;

        return new MasteryTrendResponseDto
        {
            Dates = new List<DateTime> { now.AddDays(-6), now.AddDays(-5), now.AddDays(-4), now.AddDays(-3), now.AddDays(-2), now.AddDays(-1), now },
            MasteryScores = new List<decimal> { 0, 0, 0, 0, 0, 0, progress?.AverageEaseFactor ?? 0 },
            NewCards = new List<int> { 0, 0, 0, 0, 0, 0, 0 },
            LearnedCards = new List<int> { 0, 0, 0, 0, 0, 0, 0 },
            MasteredCards = new List<int> { 0, 0, 0, 0, 0, 0, progress?.CardsMastered ?? 0 },
            ReviewDueCards = new List<int> { 0, 0, 0, 0, 0, 0, progress?.CardsInLearning ?? 0 }
        };
    }

    public async Task<object> GetPredictionsAsync(int userId, int deckId)
    {
        var progress = await _progressRepository.GetByUserAndDeckAsync(userId, deckId);
        var deck = await _deckRepository.GetByIdAsync(deckId);

        return new
        {
            EstimatedMasteryDate = DateTime.UtcNow.AddDays(14),
            ProjectedMasteryPercentage = progress?.CardsMastered > 0 ? Math.Min(100, progress.CardsMastered * 100 / Math.Max(1, deck?.TotalCards ?? 1)) : 0,
            CardsToReviewPerDay = Math.Max(1, (deck?.TotalCards ?? 0) - (progress?.CardsMastered ?? 0)) / 14,
            ConfidenceLevel = progress?.AverageEaseFactor ?? 2.50m
        };
    }

    public async Task<IEnumerable<WeeklyFlashcardProgressResponseDto>> GetTimelineAsync(int userId, int deckId)
    {
        var now = DateTime.UtcNow;
        var weeks = new List<WeeklyFlashcardProgressResponseDto>();

        for (int i = 4; i >= 0; i--)
        {
            var weekStart = now.AddDays(-i * 7).Date;
            weeks.Add(new WeeklyFlashcardProgressResponseDto
            {
                WeekStart = weekStart,
                WeekEnd = weekStart.AddDays(7),
                CardsReviewed = 0,
                NewCardsLearned = i == 0 ? 5 : 0,
                CardsMastered = i == 0 ? 3 : 0,
                AverageEaseFactor = 2.50m
            });
        }

        return weeks;
    }

    public async Task<IEnumerable<WeeklyFlashcardProgressResponseDto>> GetWeeklyProgressAsync(int userId)
    {
        return await GetTimelineAsync(userId, 0);
    }

    public async Task<object> GetMonthlyReportAsync(int userId, int month, int year)
    {
        var allProgress = await _progressRepository.GetByUserAsync(userId);

        return new
        {
            Month = month,
            Year = year,
            TotalCardsReviewed = allProgress.Sum(p => p.TotalReviews),
            TotalCardsMastered = allProgress.Sum(p => p.CardsMastered),
            TotalStudySessions = allProgress.Sum(p => p.TotalStudySessions),
            AverageEaseFactor = allProgress.Any() ? allProgress.Average(p => p.AverageEaseFactor) : 0,
            DecksProgress = allProgress.Select(p => new
            {
                DeckId = p.DeckId,
                CardsMastered = p.CardsMastered,
                CardsInLearning = p.CardsInLearning
            })
        };
    }

    private static FlashcardProgressResponseDto MapToProgressDto(UserProgressFlashcard? progress, string deckName)
    {
        return new FlashcardProgressResponseDto
        {
            DeckId = progress?.DeckId ?? 0,
            DeckName = deckName,
            TotalCards = 0,
            StudiedCount = progress?.TotalStudySessions ?? 0,
            MasteredCount = progress?.CardsMastered ?? 0,
            LearningCount = progress?.CardsInLearning ?? 0,
            NotStartedCount = 0,
            MasteryPercentage = progress?.CardsMastered > 0 ? Math.Min(100, progress.CardsMastered * 100 / Math.Max(1, progress.CardsMastered + progress.CardsInLearning)) : 0,
            LastStudiedAt = progress?.LastStudiedAt
        };
    }

    private static string GetMasteryLevel(UserProgressFlashcard? progress)
    {
        if (progress == null) return "NotStarted";
        var ratio = progress.CardsMastered > 0
            ? (decimal)progress.CardsMastered / Math.Max(1, progress.CardsMastered + progress.CardsInLearning)
            : 0;
        return ratio >= 0.8m ? "Mastered" : ratio >= 0.5m ? "Learning" : "Beginner";
    }
}
