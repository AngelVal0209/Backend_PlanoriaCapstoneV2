using PlanoriaCapstone.DTOs.IA.Requests;
using PlanoriaCapstone.DTOs.IA.Responses;

namespace PlanoriaCapstone.Bll.Interface;

public interface IAiGenerationService
{
    Task<GenerationResponseDto> GenerateFlashcardsAsync(int userId, GenerateContentRequestDto request);
    Task<GenerationResponseDto> GenerateQuizAsync(int userId, GenerateContentRequestDto request);
    Task<GenerationResponseDto> GetGenerationStatusAsync(int generationId);
    Task SetConfigAsync(AIConfigRequestDto request);
    Task<AIConfigResponseDto> GetConfigAsync();
    Task TestConnectionAsync();
    Task<GenerationResponseDto> RegenerateAsync(ImproveContentRequestDto request);
    Task<GenerationResponseDto> ImproveQuestionsAsync(ImproveContentRequestDto request);
    Task<GenerationResponseDto> AdjustDifficultyAsync(int generatedContentId, string newDifficulty);
    Task<IEnumerable<GeneratedContentResponseDto>> GetHistoryAsync(int userId, int? fileId);
    Task<GeneratedContentResponseDto> GetGeneratedContentAsync(int id);
    Task<bool> DeleteHistoryAsync(int id);
}
