using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal;
using PlanoriaCapstone.DTOs.Progress.Requests;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Bll.Service;

public class PerformanceService : IPerformanceService
{
    private readonly IUserProgressFlashcardRepository _flashcardProgressRepository;
    private readonly IUserProgressQuizRepository _quizProgressRepository;
    private readonly IUserCourseExamProgressRepository _examProgressRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    public PerformanceService(
        IUserProgressFlashcardRepository flashcardProgressRepository,
        IUserProgressQuizRepository quizProgressRepository,
        IUserCourseExamProgressRepository examProgressRepository,
        ICourseRepository courseRepository,
        IActivityLogRepository activityLogRepository)
    {
        _flashcardProgressRepository = flashcardProgressRepository;
        _quizProgressRepository = quizProgressRepository;
        _examProgressRepository = examProgressRepository;
        _courseRepository = courseRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<object> GetGlobalStatsAsync(int userId)
    {
        var flashcardProgress = await _flashcardProgressRepository.GetByUserAsync(userId);
        var quizProgress = await _quizProgressRepository.GetByUserAsync(userId);

        return new
        {
            TotalCardsMastered = flashcardProgress.Sum(p => p.CardsMastered),
            TotalCardsInLearning = flashcardProgress.Sum(p => p.CardsInLearning),
            TotalQuizzesPassed = quizProgress.Sum(p => p.PassedCount),
            AverageQuizScore = quizProgress.Any() ? quizProgress.Average(p => p.AverageScore) : 0,
            TotalStudySessions = flashcardProgress.Sum(p => p.TotalStudySessions),
            TotalReviews = flashcardProgress.Sum(p => p.TotalReviews)
        };
    }

    public async Task<object> GetRankingAsync(int userId)
    {
        var progress = await _flashcardProgressRepository.GetByUserAsync(userId);
        var totalMastered = progress.Sum(p => p.CardsMastered);

        return new
        {
            UserRank = 1,
            TotalUsers = 1,
            Percentile = 100,
            TotalMastered = totalMastered,
            Category = totalMastered >= 100 ? "Expert" : totalMastered >= 50 ? "Intermediate" : "Beginner"
        };
    }

    public async Task<object> GetAchievementsAsync(int userId)
    {
        var progress = await _flashcardProgressRepository.GetByUserAsync(userId);
        var quizProgress = await _quizProgressRepository.GetByUserAsync(userId);

        var achievements = new List<object>();

        if (progress.Sum(p => p.CardsMastered) >= 100)
            achievements.Add(new { Id = 1, Name = "Card Master", Description = "Master 100 flashcards", UnlockedAt = DateTime.UtcNow });

        if (quizProgress.Sum(p => p.PassedCount) >= 10)
            achievements.Add(new { Id = 2, Name = "Quiz Champion", Description = "Pass 10 quizzes", UnlockedAt = DateTime.UtcNow });

        if (progress.Sum(p => p.TotalStudySessions) >= 50)
            achievements.Add(new { Id = 3, Name = "Dedicated Student", Description = "Complete 50 study sessions", UnlockedAt = DateTime.UtcNow });

        return achievements;
    }

    public async Task<object> GetWeeklyTrendAsync(int userId)
    {
        var now = DateTime.UtcNow;
        var weekStart = now.AddDays(-7);
        var logs = await _activityLogRepository.GetByUserAsync(userId, 100);
        var weeklyLogs = logs.Where(l => l.CreatedAt >= weekStart).ToList();

        return new
        {
            WeekStart = weekStart,
            WeekEnd = now,
            TotalActivities = weeklyLogs.Count,
            DailyBreakdown = Enumerable.Range(0, 7).Select(i =>
            {
                var day = weekStart.AddDays(i);
                return new
                {
                    Date = day,
                    Activities = weeklyLogs.Count(l => l.CreatedAt.Date == day.Date)
                };
            })
        };
    }

    public async Task<object> GetMonthlyTrendAsync(int userId)
    {
        return await GetWeeklyTrendAsync(userId);
    }

    public async Task<object> GetYearlyReportAsync(int userId, int year)
    {
        var progress = await _flashcardProgressRepository.GetByUserAsync(userId);
        var quizProgress = await _quizProgressRepository.GetByUserAsync(userId);

        return new
        {
            Year = year,
            TotalCardsMastered = progress.Sum(p => p.CardsMastered),
            TotalQuizzesPassed = quizProgress.Sum(p => p.PassedCount),
            AverageEaseFactor = progress.Any() ? progress.Average(p => p.AverageEaseFactor) : 2.50m,
            MonthlyBreakdown = Enumerable.Range(1, 12).Select(m => new
            {
                Month = m,
                CardsMastered = 0,
                QuizzesPassed = 0
            })
        };
    }

    public async Task SetGoalsAsync(int userId, SetGoalRequestDto request)
    {
        await _activityLogRepository.LogAsync(new ActivityLog
        {
            UserId = userId,
            Action = "SetGoal",
            EntityType = "Goal",
            Details = $"Target: {request.TargetType} = {request.TargetValue}, Metric: {request.Metric}",
            CreatedAt = DateTime.UtcNow
        });
    }

    public async Task<object> GetGoalsAsync(int userId)
    {
        var logs = await _activityLogRepository.GetByUserAsync(userId, 50);
        var goalLogs = logs.Where(l => l.Action == "SetGoal").ToList();

        return goalLogs.Select(l => new
        {
            TargetType = l.Details,
            SetAt = l.CreatedAt,
            Progress = 0,
            Status = "InProgress"
        });
    }

    public async Task UpdateGoalProgressAsync(int userId, UpdateGoalProgressRequestDto request)
    {
        await _activityLogRepository.LogAsync(new ActivityLog
        {
            UserId = userId,
            Action = "UpdateGoalProgress",
            EntityType = "Goal",
            EntityId = request.GoalId,
            Details = $"Progress: {request.CurrentValue}, Status: {request.Status}",
            CreatedAt = DateTime.UtcNow
        });
    }

    public async Task<object> CheckAchievementAsync(int userId)
    {
        return await GetAchievementsAsync(userId);
    }
}
