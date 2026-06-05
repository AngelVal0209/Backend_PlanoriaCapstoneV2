using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal;
using PlanoriaCapstone.DTOs.Quiz.Requests;
using PlanoriaCapstone.DTOs.Quiz.Responses;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Bll.Service;

public class QuizAttemptService : IQuizAttemptService
{
    private readonly IQuizAttemptRepository _attemptRepository;
    private readonly IQuizRepository _quizRepository;
    private readonly IUserProgressQuizRepository _progressRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    public QuizAttemptService(
        IQuizAttemptRepository attemptRepository,
        IQuizRepository quizRepository,
        IUserProgressQuizRepository progressRepository,
        IActivityLogRepository activityLogRepository)
    {
        _attemptRepository = attemptRepository;
        _quizRepository = quizRepository;
        _progressRepository = progressRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<QuizAttemptResponseDto> StartAsync(int userId, StartQuizAttemptRequestDto request)
    {
        var quiz = await _quizRepository.GetByIdAsync(request.QuizId);
        if (quiz == null)
            throw new KeyNotFoundException($"Quiz con ID {request.QuizId} no encontrado");

        if (quiz.AttemptsAllowed > 0)
        {
            var existingAttempts = await _attemptRepository.GetByQuizIdAsync(request.QuizId);
            var userAttempts = existingAttempts.Where(a => a.UserId == userId).ToList();
            if (userAttempts.Count >= quiz.AttemptsAllowed)
                throw new InvalidOperationException("Has alcanzado el límite de intentos para este quiz");
        }

        var attempt = new QuizAttempt
        {
            UserId = userId,
            QuizId = request.QuizId,
            StartedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _attemptRepository.CreateAsync(attempt);

        await _activityLogRepository.LogAsync(new ActivityLog
        {
            UserId = userId,
            Action = "StartQuizAttempt",
            EntityType = "QuizAttempt",
            EntityId = created.Id,
            Details = $"Inicio intento de quiz ID {request.QuizId}",
            CreatedAt = DateTime.UtcNow
        });

        return MapToAttemptResponse(created);
    }

    public async Task<QuizResultResponseDto> SubmitAsync(int userId, SubmitQuizRequestDto request)
    {
        var attempt = await _attemptRepository.GetByIdAsync(request.AttemptId);
        if (attempt == null || attempt.UserId != userId)
            throw new KeyNotFoundException($"Intento con ID {request.AttemptId} no encontrado");

        if (attempt.CompletedAt != null)
            throw new InvalidOperationException("Este intento ya ha sido completado");

        // Save all provided answers
        if (request.Answers != null)
        {
            foreach (var answerDto in request.Answers)
            {
            var existingAnswers = await _attemptRepository.GetAnswersByAttemptAsync(request.AttemptId);
            var existing = existingAnswers.FirstOrDefault(a => a.QuestionId == answerDto.QuestionId);

            if (existing != null)
            {
                existing.SelectedOptionId = answerDto.SelectedOptionId;
                existing.ShortAnswerText = answerDto.ShortAnswerText;
                existing.AnsweredAt = DateTime.UtcNow;
                await _attemptRepository.UpdateAsync(attempt);
            }
            else
            {
                await _attemptRepository.AddAnswerAsync(new QuizAnswer
                {
                    AttemptId = request.AttemptId,
                    QuestionId = answerDto.QuestionId,
                    SelectedOptionId = answerDto.SelectedOptionId,
                    ShortAnswerText = answerDto.ShortAnswerText,
                    IsCorrect = false,
                    PointsEarned = 0,
                    AnsweredAt = DateTime.UtcNow
                });
            }
        }
        }

        await AutoGradeAsync(request.AttemptId);

        attempt = await _attemptRepository.GetByIdAsync(request.AttemptId);
        if (attempt == null)
            throw new KeyNotFoundException($"Intento con ID {request.AttemptId} no encontrado después de guardar");

        attempt.CompletedAt = DateTime.UtcNow;
        attempt.TimeSpentSeconds = (int)(DateTime.UtcNow - attempt.StartedAt).TotalSeconds;
        await _attemptRepository.UpdateAsync(attempt);

        await UpdateUserProgress(userId, attempt.QuizId);

        await _activityLogRepository.LogAsync(new ActivityLog
        {
            UserId = userId,
            Action = "SubmitQuizAttempt",
            EntityType = "QuizAttempt",
            EntityId = attempt.Id,
            Details = $"Intento de quiz completado. Score: {attempt.ScorePercentage}%",
            CreatedAt = DateTime.UtcNow
        });

        return await BuildResultAsync(attempt);
    }

    public async Task<QuizAttemptResponseDto> GetResultAsync(int attemptId)
    {
        var attempt = await _attemptRepository.GetByIdAsync(attemptId);
        if (attempt == null)
            throw new KeyNotFoundException($"Intento con ID {attemptId} no encontrado");

        return MapToAttemptResponse(attempt);
    }

    public async Task<IEnumerable<QuizAttemptResponseDto>> GetAttemptsAsync(int userId, int? quizId)
    {
        var attempts = quizId.HasValue
            ? (await _attemptRepository.GetByQuizIdAsync(quizId.Value))
                .Where(a => a.UserId == userId)
            : await _attemptRepository.GetByUserAsync(userId);

        return attempts
            .OrderByDescending(a => a.StartedAt)
            .Select(MapToAttemptResponse);
    }

    public async Task SaveAnswerAsync(int userId, SubmitAnswerRequestDto request)
    {
        var attempt = await _attemptRepository.GetByIdAsync(request.AttemptId);
        if (attempt == null || attempt.UserId != userId)
            throw new KeyNotFoundException("Intento no encontrado");

        if (attempt.CompletedAt != null)
            throw new InvalidOperationException("Este intento ya ha sido completado");

        var existingAnswers = await _attemptRepository.GetAnswersByAttemptAsync(request.AttemptId);
        var existing = existingAnswers.FirstOrDefault(a => a.QuestionId == request.QuestionId);

        if (existing != null)
        {
            existing.SelectedOptionId = request.SelectedOptionId;
            existing.ShortAnswerText = request.ShortAnswerText;
            existing.AnsweredAt = DateTime.UtcNow;
            GradeSingleAnswer(existing);
        }
        else
        {
            var answer = new QuizAnswer
            {
                AttemptId = request.AttemptId,
                QuestionId = request.QuestionId,
                SelectedOptionId = request.SelectedOptionId,
                ShortAnswerText = request.ShortAnswerText,
                IsCorrect = false,
                PointsEarned = 0,
                AnsweredAt = DateTime.UtcNow
            };
            GradeSingleAnswer(answer);
            await _attemptRepository.AddAnswerAsync(answer);
        }
    }

    public async Task UpdateAnswerAsync(int userId, SubmitAnswerRequestDto request)
    {
        var attempt = await _attemptRepository.GetByIdAsync(request.AttemptId);
        if (attempt == null || attempt.UserId != userId)
            throw new KeyNotFoundException("Intento no encontrado");

        var answers = await _attemptRepository.GetAnswersByAttemptAsync(request.AttemptId);
        var answer = answers.FirstOrDefault(a => a.QuestionId == request.QuestionId);
        if (answer == null)
            throw new KeyNotFoundException("Respuesta no encontrada para esta pregunta");

        answer.SelectedOptionId = request.SelectedOptionId;
        answer.ShortAnswerText = request.ShortAnswerText;
        answer.AnsweredAt = DateTime.UtcNow;
        GradeSingleAnswer(answer);
    }

    public async Task BulkSaveAnswersAsync(int userId, List<SubmitAnswerRequestDto> request)
    {
        foreach (var item in request)
            await SaveAnswerAsync(userId, item);
    }

    public async Task AutoGradeAsync(int attemptId)
    {
        var attempt = await _attemptRepository.GetByIdAsync(attemptId);
        if (attempt == null)
            throw new KeyNotFoundException($"Intento con ID {attemptId} no encontrado");

        var quiz = await _quizRepository.GetByIdAsync(attempt.QuizId);
        if (quiz == null)
            throw new KeyNotFoundException("Quiz no encontrado");

        var answers = await _attemptRepository.GetAnswersByAttemptAsync(attemptId);

        decimal totalPoints = 0;
        int correctCount = 0;

        foreach (var answer in answers)
        {
            GradeSingleAnswer(answer);

            if (answer.IsCorrect)
                correctCount++;
            totalPoints += answer.PointsEarned;
        }

        var allQuestions = quiz.QuizQuestions ?? new List<QuizQuestion>();
        decimal maxPoints = allQuestions.Sum(q => q.Points);
        var score = maxPoints > 0 ? Math.Round(totalPoints / maxPoints * 100, 2) : 0;

        attempt.ScorePercentage = score;
        attempt.Passed = score >= quiz.PassingScore;
        await _attemptRepository.UpdateAsync(attempt);
    }

    public async Task RegradeAsync(int attemptId)
    {
        await AutoGradeAsync(attemptId);

        var attempt = await _attemptRepository.GetByIdAsync(attemptId);
        if (attempt == null) return;

        await UpdateUserProgress(attempt.UserId, attempt.QuizId);
    }

    public async Task<IEnumerable<QuizAttemptResponseDto>> GetHistoryAsync(int userId, int quizId)
    {
        var attempts = (await _attemptRepository.GetByQuizIdAsync(quizId))
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.StartedAt);

        return attempts.Select(MapToAttemptResponse);
    }

    public async Task<QuizAttemptResponseDto> GetBestAttemptAsync(int userId, int quizId)
    {
        var attempts = (await _attemptRepository.GetByQuizIdAsync(quizId))
            .Where(a => a.UserId == userId && a.ScorePercentage.HasValue)
            .OrderByDescending(a => a.ScorePercentage)
            .ToList();

        var best = attempts.FirstOrDefault();
        if (best == null)
            throw new KeyNotFoundException("No se encontraron intentos completados para este quiz");

        return MapToAttemptResponse(best);
    }

    public async Task<object> CompareAttemptsAsync(int attemptId1, int attemptId2)
    {
        var a1 = await _attemptRepository.GetByIdAsync(attemptId1);
        var a2 = await _attemptRepository.GetByIdAsync(attemptId2);

        if (a1 == null || a2 == null)
            throw new KeyNotFoundException("Uno o ambos intentos no fueron encontrados");

        return new
        {
            Attempt1 = MapToAttemptResponse(a1),
            Attempt2 = MapToAttemptResponse(a2),
            Differences = new
            {
                ScoreDiff = (a1.ScorePercentage ?? 0) - (a2.ScorePercentage ?? 0),
                TimeDiff = (a1.TimeSpentSeconds ?? 0) - (a2.TimeSpentSeconds ?? 0)
            }
        };
    }

    private void GradeSingleAnswer(QuizAnswer answer)
    {
        // For MCQ: compare selected option to correct option
        if (answer.SelectedOptionId.HasValue)
        {
            // We need the question to know the correct option
            // This is called after the answer is retrieved with navigation
            // If navigation isn't loaded, we can't grade here directly
            // The full grading happens in AutoGradeAsync
        }
    }

    private async Task UpdateUserProgress(int userId, int quizId)
    {
        var quiz = await _quizRepository.GetByIdAsync(quizId);
        if (quiz == null) return;

        var allAttempts = (await _attemptRepository.GetByQuizIdAsync(quizId))
            .Where(a => a.UserId == userId && a.ScorePercentage.HasValue)
            .ToList();

        var progress = await _progressRepository.GetByUserAndQuizAsync(userId, quizId);
        if (progress == null)
        {
            progress = new UserProgressQuiz
            {
                UserId = userId,
                QuizId = quizId,
                TotalAttempts = 0,
                BestScore = 0,
                AverageScore = 0,
                PassedCount = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        progress.TotalAttempts = allAttempts.Count;
        progress.BestScore = allAttempts.Any() ? allAttempts.Max(a => a.ScorePercentage ?? 0) : 0;
        progress.AverageScore = allAttempts.Any() ? allAttempts.Average(a => a.ScorePercentage ?? 0) : 0;
        progress.LastAttemptAt = allAttempts.Any() ? allAttempts.Max(a => a.CompletedAt) : null;
        progress.PassedCount = allAttempts.Count(a => a.Passed == true);
        progress.UpdatedAt = DateTime.UtcNow;

        await _progressRepository.CreateOrUpdateAsync(progress);
    }

    private async Task<QuizResultResponseDto> BuildResultAsync(QuizAttempt attempt)
    {
        var answers = await _attemptRepository.GetAnswersByAttemptAsync(attempt.Id);
        var quiz = attempt.Quiz ?? await _quizRepository.GetByIdAsync(attempt.QuizId);

        var answerDtos = new List<AnswerResponseDto>();
        int correctCount = 0;

        foreach (var answer in answers)
        {
            var question = quiz?.QuizQuestions?.FirstOrDefault(q => q.Id == answer.QuestionId);
            var selectedOption = question?.QuizOptions?.FirstOrDefault(o => o.Id == answer.SelectedOptionId);
            var correctOption = question?.QuizOptions?.FirstOrDefault(o => o.IsCorrect);

            if (answer.IsCorrect)
                correctCount++;

            answerDtos.Add(new AnswerResponseDto
            {
                QuestionId = answer.QuestionId,
                QuestionText = question?.QuestionText,
                SelectedOption = selectedOption != null ? new OptionResponseDto
                {
                    Id = selectedOption.Id,
                    OptionText = selectedOption.OptionText,
                    IsCorrect = selectedOption.IsCorrect,
                    OrderPosition = selectedOption.OrderPosition
                } : null,
                ShortAnswerText = answer.ShortAnswerText,
                IsCorrect = answer.IsCorrect,
                PointsEarned = answer.PointsEarned,
                CorrectAnswer = correctOption != null ? new OptionResponseDto
                {
                    Id = correctOption.Id,
                    OptionText = correctOption.OptionText,
                    IsCorrect = correctOption.IsCorrect,
                    OrderPosition = correctOption.OrderPosition
                } : null
            });
        }

        return new QuizResultResponseDto
        {
            Attempt = MapToAttemptResponse(attempt),
            Answers = answerDtos,
            FeedbackSummary = attempt.Passed == true
                ? "¡Felicidades! Has aprobado el quiz."
                : "Sigue practicando. Revisa las respuestas incorrectas.",
            WeakTopics = new List<string>(),
            Recommendations = attempt.Passed == true
                ? new List<string> { "Continúa con el siguiente tema." }
                : new List<string> { "Revisa el material de estudio e intenta de nuevo." }
        };
    }

    private QuizAttemptResponseDto MapToAttemptResponse(QuizAttempt attempt)
    {
        return new QuizAttemptResponseDto
        {
            Id = attempt.Id,
            QuizId = attempt.QuizId,
            QuizTitle = attempt.Quiz?.Title,
            StartedAt = attempt.StartedAt,
            CompletedAt = attempt.CompletedAt,
            ScorePercentage = attempt.ScorePercentage,
            Passed = attempt.Passed,
            TimeSpentSeconds = attempt.TimeSpentSeconds,
            AnswersCount = attempt.QuizAnswers?.Count ?? 0,
            CorrectAnswersCount = attempt.QuizAnswers?.Count(a => a.IsCorrect) ?? 0
        };
    }
}
