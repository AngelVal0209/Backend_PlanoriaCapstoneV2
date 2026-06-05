using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal;
using PlanoriaCapstone.DTOs.Reports.Responses;
using PlanoriaCapstone.DTOs.System.Requests;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Bll.Service;

public class ReportService : IReportService
{
    private readonly IUserProgressFlashcardRepository _flashcardProgressRepository;
    private readonly IUserProgressQuizRepository _quizProgressRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    public ReportService(
        IUserProgressFlashcardRepository flashcardProgressRepository,
        IUserProgressQuizRepository quizProgressRepository,
        IActivityLogRepository activityLogRepository)
    {
        _flashcardProgressRepository = flashcardProgressRepository;
        _quizProgressRepository = quizProgressRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<StudyReportResponseDto> GenerateStudyReportAsync(int userId, DateTime from, DateTime to)
    {
        var logs = await _activityLogRepository.GetByUserAsync(userId, 500);
        var periodLogs = logs.Where(l => l.CreatedAt >= from && l.CreatedAt <= to).ToList();

        var studyMinutes = periodLogs.Count * 30;

        return new StudyReportResponseDto
        {
            PeriodStart = from,
            PeriodEnd = to,
            TotalStudyHours = studyMinutes / 60m,
            AverageDailyMinutes = (int)(studyMinutes / Math.Max(1, (to - from).Days)),
            MostStudiedCourses = new List<string>(),
            StudyTimeByCourse = new List<StudyTimeByCourse>(),
            StudyTimeByHour = Enumerable.Range(0, 24).Select(h => new StudyTimeByHour { Hour = h, Minutes = 0 }).ToList(),
            ConsistencyScore = 75
        };
    }

    public async Task<object> GetStudyInsightsAsync(int userId)
    {
        var flashcardProgress = await _flashcardProgressRepository.GetByUserAsync(userId);
        var quizProgress = await _quizProgressRepository.GetByUserAsync(userId);

        return new
        {
            TotalStudyHours = flashcardProgress.Sum(p => p.TotalStudySessions * 0.5m),
            BestPerformanceDay = DateTime.UtcNow.DayOfWeek.ToString(),
            AverageSessionLength = 30,
            MostProductiveTime = "Morning",
            ConsistencyTrend = "Improving",
            Recommendations = new List<string>
            {
                "Try studying in the morning for better retention",
                "Take short breaks every 25 minutes",
                "Review weak topics more frequently"
            }
        };
    }

    public async Task<object> GeneratePerformanceReportAsync(int userId, DateTime from, DateTime to)
    {
        var flashcardProgress = await _flashcardProgressRepository.GetByUserAsync(userId);
        var quizProgress = await _quizProgressRepository.GetByUserAsync(userId);

        return new PerformanceReportResponseDto
        {
            PeriodStart = from,
            PeriodEnd = to,
            FlashcardsMastered = flashcardProgress.Sum(p => p.CardsMastered),
            QuizzesPassed = quizProgress.Sum(p => p.PassedCount),
            AverageQuizScore = quizProgress.Any() ? quizProgress.Average(p => p.AverageScore) : 0,
            WeakTopics = new List<string>(),
            StrongTopics = new List<string>(),
            ImprovementAreas = new List<string>()
        };
    }

    public async Task<object> GetPerformanceSummaryAsync(int userId)
    {
        var flashcardProgress = await _flashcardProgressRepository.GetByUserAsync(userId);
        var quizProgress = await _quizProgressRepository.GetByUserAsync(userId);

        return new
        {
            TotalCardsMastered = flashcardProgress.Sum(p => p.CardsMastered),
            TotalCardsInLearning = flashcardProgress.Sum(p => p.CardsInLearning),
            TotalQuizzesPassed = quizProgress.Sum(p => p.PassedCount),
            TotalQuizzesAttempted = quizProgress.Sum(p => p.TotalAttempts),
            OverallAverageScore = quizProgress.Any() ? quizProgress.Average(p => p.AverageScore) : 0,
            OverallEaseFactor = flashcardProgress.Any() ? flashcardProgress.Average(p => p.AverageEaseFactor) : 2.50m
        };
    }

    public async Task<CustomReportResponseDto> CreateCustomReportAsync(int userId, CreateCustomReportRequestDto request)
    {
        await _activityLogRepository.LogAsync(new ActivityLog
        {
            UserId = userId,
            Action = "CreateCustomReport",
            EntityType = "Report",
            Details = $"Report: {request.Name}",
            CreatedAt = DateTime.UtcNow
        });

        return new CustomReportResponseDto
        {
            Id = new Random().Next(),
            Name = request.Name,
            Config = string.Join(",", request.Metrics ?? new List<string>()),
            GeneratedAt = DateTime.UtcNow,
            DownloadUrl = string.Empty,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };
    }

    public async Task<IEnumerable<ReportTemplateResponseDto>> SaveTemplateAsync(int userId, ReportTemplateResponseDto request)
    {
        return new List<ReportTemplateResponseDto>
        {
            new()
            {
                Id = 1,
                Name = request.Name,
                Description = request.Description,
                Config = request.Config,
                IsDefault = false,
                CreatedAt = DateTime.UtcNow
            }
        };
    }

    public async Task<IEnumerable<ReportTemplateResponseDto>> GetTemplatesAsync(int userId)
    {
        return new List<ReportTemplateResponseDto>
        {
            new() { Id = 1, Name = "Weekly Study Report", Description = "Summary of weekly study activities", Config = "{}", IsDefault = true, CreatedAt = DateTime.UtcNow },
            new() { Id = 2, Name = "Monthly Performance", Description = "Monthly performance overview", Config = "{}", IsDefault = true, CreatedAt = DateTime.UtcNow }
        };
    }

    public async Task<object> ScheduleReportAsync(int userId, CreateCustomReportRequestDto request)
    {
        return new
        {
            Scheduled = true,
            ReportName = request.Name,
            Schedule = string.Join(",", request.Schedule ?? new List<string>()),
            NextRun = DateTime.UtcNow.AddDays(1),
            Format = request.Format
        };
    }
}
