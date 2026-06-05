using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal;
using PlanoriaCapstone.DTOs.Progress.Responses.Quizzes;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Bll.Service;

public class QuizProgressService : IQuizProgressService
{
    private readonly IUserProgressQuizRepository _progressRepository;
    private readonly IQuizRepository _quizRepository;
    private readonly IQuizAttemptRepository _attemptRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    public QuizProgressService(
        IUserProgressQuizRepository progressRepository,
        IQuizRepository quizRepository,
        IQuizAttemptRepository attemptRepository,
        ICourseRepository courseRepository,
        IActivityLogRepository activityLogRepository)
    {
        _progressRepository = progressRepository;
        _quizRepository = quizRepository;
        _attemptRepository = attemptRepository;
        _courseRepository = courseRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<QuizProgressResponseDto> GetByQuizAsync(int userId, int quizId)
    {
        var progress = await _progressRepository.GetByUserAndQuizAsync(userId, quizId);
        var quiz = await _quizRepository.GetByIdAsync(quizId);
        return MapToProgressDto(progress, quiz?.Title ?? "Unknown");
    }

    public async Task<IEnumerable<QuizProgressResponseDto>> GetByCourseAsync(int userId, int courseId)
    {
        var quizzes = await _quizRepository.GetByCourseIdAsync(courseId);
        var result = new List<QuizProgressResponseDto>();

        foreach (var quiz in quizzes)
        {
            var progress = await _progressRepository.GetByUserAndQuizAsync(userId, quiz.Id);
            result.Add(MapToProgressDto(progress, quiz.Title));
        }

        return result;
    }

    public async Task<IEnumerable<QuizProgressResponseDto>> GetOverallAsync(int userId)
    {
        var allProgress = await _progressRepository.GetByUserAsync(userId);
        var result = new List<QuizProgressResponseDto>();

        foreach (var progress in allProgress)
        {
            var quiz = await _quizRepository.GetByIdAsync(progress.QuizId);
            result.Add(MapToProgressDto(progress, quiz?.Title ?? "Unknown"));
        }

        return result;
    }

    public async Task<QuizPerformanceResponseDto> GetAverageScoreAsync(int userId, int? quizId)
    {
        var attempts = quizId.HasValue
            ? (await _attemptRepository.GetByQuizIdAsync(quizId.Value))
            : (await _attemptRepository.GetByUserAsync(userId));

        var userAttempts = attempts.Where(a => a.UserId == userId).ToList();

        return new QuizPerformanceResponseDto
        {
            WeakTopics = new List<TopicAccuracy>(),
            StrongTopics = new List<TopicAccuracy>(),
            AverageResponseTime = 0,
            ScoreTrend = userAttempts.Select((a, i) => new ScorePoint
            {
                Attempt = i + 1,
                Score = a.ScorePercentage ?? 0
            }).ToList()
        };
    }

    public async Task<IEnumerable<string>> GetWeakTopicsAsync(int userId, int courseId)
    {
        var quizzes = await _quizRepository.GetByCourseIdAsync(courseId);
        var weakTopics = new List<string>();

        foreach (var quiz in quizzes)
        {
            var progress = await _progressRepository.GetByUserAndQuizAsync(userId, quiz.Id);
            if (progress == null || progress.AverageScore < 70)
                weakTopics.Add(quiz.Title);
        }

        return weakTopics;
    }

    public async Task<object> GetImprovementAsync(int userId, int quizId)
    {
        var attempts = (await _attemptRepository.GetByQuizIdAsync(quizId))
            .Where(a => a.UserId == userId)
            .OrderBy(a => a.CreatedAt)
            .ToList();

        return new
        {
            FirstScore = attempts.FirstOrDefault()?.ScorePercentage ?? 0,
            LastScore = attempts.LastOrDefault()?.ScorePercentage ?? 0,
            Improvement = attempts.Count >= 2 ? (attempts.Last().ScorePercentage ?? 0) - (attempts.First().ScorePercentage ?? 0) : 0,
            AttemptsCount = attempts.Count,
            ScoreProgression = attempts.Select((a, i) => new { Attempt = i + 1, a.ScorePercentage })
        };
    }

    public async Task<QuizComparisonResponseDto> CompareQuizzesAsync(int userId, int quizId1, int quizId2)
    {
        var q1 = await _quizRepository.GetByIdAsync(quizId1);
        var q2 = await _quizRepository.GetByIdAsync(quizId2);
        var p1 = await _progressRepository.GetByUserAndQuizAsync(userId, quizId1);
        var p2 = await _progressRepository.GetByUserAndQuizAsync(userId, quizId2);

        return new QuizComparisonResponseDto
        {
            Period1 = new PeriodInfo { Date = DateTime.UtcNow, Score = p1?.AverageScore ?? 0 },
            Period2 = new PeriodInfo { Date = DateTime.UtcNow, Score = p2?.AverageScore ?? 0 },
            Improvement = (p1?.AverageScore ?? 0) - (p2?.AverageScore ?? 0),
            BestQuiz = (p1?.AverageScore ?? 0) >= (p2?.AverageScore ?? 0) ? q1?.Title : q2?.Title,
            WorstQuiz = (p1?.AverageScore ?? 0) < (p2?.AverageScore ?? 0) ? q1?.Title : q2?.Title
        };
    }

    public async Task<object> CompareCoursesAsync(int userId, int courseId1, int courseId2)
    {
        var q1 = await _quizRepository.GetByCourseIdAsync(courseId1);
        var q2 = await _quizRepository.GetByCourseIdAsync(courseId2);
        var course1 = await _courseRepository.GetByIdAsync(courseId1);
        var course2 = await _courseRepository.GetByIdAsync(courseId2);

        return new
        {
            Course1 = new
            {
                Name = course1?.Name,
                QuizzesCount = q1.Count(),
                AverageScore = await GetCourseAverageScore(userId, q1)
            },
            Course2 = new
            {
                Name = course2?.Name,
                QuizzesCount = q2.Count(),
                AverageScore = await GetCourseAverageScore(userId, q2)
            }
        };
    }

    public async Task<object> CompareTimeframesAsync(int userId, DateTime from1, DateTime to1, DateTime from2, DateTime to2)
    {
        var allAttempts = await _attemptRepository.GetByUserAsync(userId);
        var period1 = allAttempts.Where(a => a.CreatedAt >= from1 && a.CreatedAt <= to1).ToList();
        var period2 = allAttempts.Where(a => a.CreatedAt >= from2 && a.CreatedAt <= to2).ToList();

        return new
        {
            Period1 = new { From = from1, To = to1, AverageScore = period1.Any() ? period1.Average(a => a.ScorePercentage) : 0, Count = period1.Count },
            Period2 = new { From = from2, To = to2, AverageScore = period2.Any() ? period2.Average(a => a.ScorePercentage) : 0, Count = period2.Count },
            Improvement = period1.Any() && period2.Any() ? period1.Average(a => a.ScorePercentage) - period2.Average(a => a.ScorePercentage) : 0
        };
    }

    private async Task<decimal> GetCourseAverageScore(int userId, IEnumerable<Quiz> quizzes)
    {
        var scores = new List<decimal>();
        foreach (var quiz in quizzes)
        {
            var progress = await _progressRepository.GetByUserAndQuizAsync(userId, quiz.Id);
            if (progress != null)
                scores.Add(progress.AverageScore);
        }
        return scores.Any() ? scores.Average() : 0;
    }

    private static QuizProgressResponseDto MapToProgressDto(UserProgressQuiz? progress, string title)
    {
        return new QuizProgressResponseDto
        {
            QuizId = progress?.QuizId ?? 0,
            QuizTitle = title,
            TotalAttempts = progress?.TotalAttempts ?? 0,
            BestScore = progress?.BestScore,
            AverageScore = progress?.AverageScore,
            LastAttemptDate = progress?.LastAttemptAt,
            PassedCount = progress?.PassedCount ?? 0,
            RecommendedRetry = progress != null && progress.AverageScore < 70
        };
    }
}
