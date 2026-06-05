using PlanoriaCapstone.DTOs.Quiz.Requests;
using PlanoriaCapstone.DTOs.Quiz.Responses;

namespace PlanoriaCapstone.Bll.Interface;

public interface IQuizAttemptService
{
    Task<QuizAttemptResponseDto> StartAsync(int userId, StartQuizAttemptRequestDto request);
    Task<QuizResultResponseDto> SubmitAsync(int userId, SubmitQuizRequestDto request);
    Task<QuizAttemptResponseDto> GetResultAsync(int attemptId);
    Task<IEnumerable<QuizAttemptResponseDto>> GetAttemptsAsync(int userId, int? quizId);
    Task SaveAnswerAsync(int userId, SubmitAnswerRequestDto request);
    Task UpdateAnswerAsync(int userId, SubmitAnswerRequestDto request);
    Task BulkSaveAnswersAsync(int userId, List<SubmitAnswerRequestDto> request);
    Task AutoGradeAsync(int attemptId);
    Task RegradeAsync(int attemptId);
    Task<IEnumerable<QuizAttemptResponseDto>> GetHistoryAsync(int userId, int quizId);
    Task<QuizAttemptResponseDto> GetBestAttemptAsync(int userId, int quizId);
    Task<object> CompareAttemptsAsync(int attemptId1, int attemptId2);
}
