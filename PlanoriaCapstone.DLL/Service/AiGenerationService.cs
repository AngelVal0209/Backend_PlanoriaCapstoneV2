using System.Collections.Concurrent;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal;
using PlanoriaCapstone.DTOs.IA.Requests;
using PlanoriaCapstone.DTOs.IA.Responses;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Bll.Service;

public class AiGenerationService : IAiGenerationService
{
    private readonly IFileUploadRepository _fileUploadRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    // In-memory cache for generated content lookups (stub until real DB queries are added)
    private static readonly ConcurrentDictionary<int, (int FileUploadId, int CourseId)>
        _generatedIndex = new();

    private static AIConfigRequestDto? _currentConfig;
    private static bool _configLoaded;
    private static readonly object _configLock = new();

    public AiGenerationService(
        IFileUploadRepository fileUploadRepository,
        IActivityLogRepository activityLogRepository)
    {
        _fileUploadRepository = fileUploadRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<GenerationResponseDto> GenerateFlashcardsAsync(int userId, GenerateContentRequestDto request)
    {
        var file = await _fileUploadRepository.GetByIdAsync(request.FileId);
        if (file == null)
            throw new KeyNotFoundException($"Archivo con ID {request.FileId} no encontrado");

        var content = await _fileUploadRepository.CreateGeneratedContentAsync(new GeneratedContent
        {
            FileUploadId = request.FileId,
            CourseId = request.TargetCourseId,
            ContentType = request.ContentType ?? "flashcard",
            GeneratedEntityId = 0,
            TopicSpecified = request.Topic,
            GenerationConfig = System.Text.Json.JsonSerializer.Serialize(new
            {
                request.NumberOfItems,
                request.Difficulty,
                request.Language
            }),
            CreatedAt = DateTime.UtcNow
        });

        _generatedIndex[content.Id] = (content.FileUploadId, content.CourseId);

        await _activityLogRepository.LogAsync(new ActivityLog
        {
            UserId = userId,
            Action = "GenerateFlashcards",
            EntityType = "GeneratedContent",
            EntityId = content.Id,
            Details = $"Generación de flashcards desde archivo ID {request.FileId}",
            CreatedAt = DateTime.UtcNow
        });

        return new GenerationResponseDto
        {
            GenerationId = content.Id,
            FileId = request.FileId,
            ContentType = "flashcard",
            Status = "pending",
            Progress = 0,
            EstimatedTime = 30,
            CreatedAt = content.CreatedAt
        };
    }

    public async Task<GenerationResponseDto> GenerateQuizAsync(int userId, GenerateContentRequestDto request)
    {
        var file = await _fileUploadRepository.GetByIdAsync(request.FileId);
        if (file == null)
            throw new KeyNotFoundException($"Archivo con ID {request.FileId} no encontrado");

        var content = await _fileUploadRepository.CreateGeneratedContentAsync(new GeneratedContent
        {
            FileUploadId = request.FileId,
            CourseId = request.TargetCourseId,
            ContentType = request.ContentType ?? "quiz",
            GeneratedEntityId = 0,
            TopicSpecified = request.Topic,
            GenerationConfig = System.Text.Json.JsonSerializer.Serialize(new
            {
                request.NumberOfItems,
                request.Difficulty,
                request.Language
            }),
            CreatedAt = DateTime.UtcNow
        });

        _generatedIndex[content.Id] = (content.FileUploadId, content.CourseId);

        await _activityLogRepository.LogAsync(new ActivityLog
        {
            UserId = userId,
            Action = "GenerateQuiz",
            EntityType = "GeneratedContent",
            EntityId = content.Id,
            Details = $"Generación de quiz desde archivo ID {request.FileId}",
            CreatedAt = DateTime.UtcNow
        });

        return new GenerationResponseDto
        {
            GenerationId = content.Id,
            FileId = request.FileId,
            ContentType = "quiz",
            Status = "pending",
            Progress = 0,
            EstimatedTime = 45,
            CreatedAt = content.CreatedAt
        };
    }

    public async Task<GenerationResponseDto> GetGenerationStatusAsync(int generationId)
    {
        var generated = await FindGeneratedContentByIdAsync(generationId);
        if (generated == null)
            throw new KeyNotFoundException($"Generación con ID {generationId} no encontrada");

        return new GenerationResponseDto
        {
            GenerationId = generated.Id,
            FileId = generated.FileUploadId,
            ContentType = generated.ContentType,
            Status = generated.GeneratedEntityId > 0 ? "completed" : "processing",
            Progress = generated.GeneratedEntityId > 0 ? 100 : 50,
            EstimatedTime = 0,
            CreatedAt = generated.CreatedAt
        };
    }

    public Task SetConfigAsync(AIConfigRequestDto request)
    {
        lock (_configLock)
        {
            _currentConfig = request;
            _configLoaded = true;
        }
        return Task.CompletedTask;
    }

    public Task<AIConfigResponseDto> GetConfigAsync()
    {
        lock (_configLock)
        {
            if (!_configLoaded || _currentConfig == null)
            {
                return Task.FromResult(new AIConfigResponseDto
                {
                    Provider = "gemini",
                    Model = "gemini-1.5-flash",
                    MaxTokens = 2000,
                    Temperature = 0.7m,
                    IsActive = false,
                    LastUsedAt = null
                });
            }

            return Task.FromResult(new AIConfigResponseDto
            {
                Provider = _currentConfig.Provider,
                Model = _currentConfig.Model,
                MaxTokens = _currentConfig.MaxTokens,
                Temperature = _currentConfig.Temperature,
                IsActive = true,
                LastUsedAt = DateTime.UtcNow
            });
        }
    }

    public async Task TestConnectionAsync()
    {
        await _activityLogRepository.LogAsync(new ActivityLog
        {
            UserId = 0,
            Action = "TestAiConnection",
            EntityType = "System",
            Details = "Prueba de conexión AI",
            CreatedAt = DateTime.UtcNow
        });

        await Task.CompletedTask;
    }

    public async Task<GenerationResponseDto> RegenerateAsync(ImproveContentRequestDto request)
    {
        var generated = await FindGeneratedContentByIdAsync(request.GeneratedContentId);
        if (generated == null)
            throw new KeyNotFoundException($"Contenido generado con ID {request.GeneratedContentId} no encontrado");

        generated.GeneratedEntityId = 0;
        generated.GenerationConfig = System.Text.Json.JsonSerializer.Serialize(new
        {
            feedback = request.Feedback,
            adjustComplexity = request.AdjustComplexity,
            focusTopics = request.FocusTopics
        });

        await _activityLogRepository.LogAsync(new ActivityLog
        {
            Action = "RegenerateContent",
            EntityType = "GeneratedContent",
            EntityId = generated.Id,
            Details = "Regeneración de contenido solicitada",
            CreatedAt = DateTime.UtcNow
        });

        return new GenerationResponseDto
        {
            GenerationId = generated.Id,
            FileId = generated.FileUploadId,
            ContentType = generated.ContentType,
            Status = "pending",
            Progress = 0,
            EstimatedTime = 30,
            CreatedAt = generated.CreatedAt
        };
    }

    public async Task<GenerationResponseDto> ImproveQuestionsAsync(ImproveContentRequestDto request)
    {
        return await RegenerateAsync(request);
    }

    public async Task<GenerationResponseDto> AdjustDifficultyAsync(int generatedContentId, string newDifficulty)
    {
        var generated = await FindGeneratedContentByIdAsync(generatedContentId);
        if (generated == null)
            throw new KeyNotFoundException($"Contenido generado con ID {generatedContentId} no encontrado");

        generated.GenerationConfig = System.Text.Json.JsonSerializer.Serialize(new
        {
            difficulty = newDifficulty
        });

        return new GenerationResponseDto
        {
            GenerationId = generated.Id,
            FileId = generated.FileUploadId,
            ContentType = generated.ContentType,
            Status = "pending",
            Progress = 0,
            EstimatedTime = 20,
            CreatedAt = generated.CreatedAt
        };
    }

    public async Task<IEnumerable<GeneratedContentResponseDto>> GetHistoryAsync(int userId, int? fileId)
    {
        var results = new List<GeneratedContentResponseDto>();

        if (fileId.HasValue)
        {
            var file = await _fileUploadRepository.GetByIdAsync(fileId.Value);
            if (file?.GeneratedContents != null)
            {
                foreach (var gc in file.GeneratedContents)
                {
                    _generatedIndex[gc.Id] = (gc.FileUploadId, gc.CourseId);
                    results.Add(MapToGeneratedResponse(gc));
                }
            }
        }
        else
        {
            var files = await _fileUploadRepository.GetByUserIdAsync(userId);
            foreach (var file in files)
            {
                var fileWithContents = await _fileUploadRepository.GetByIdAsync(file.Id);
                if (fileWithContents?.GeneratedContents != null)
                {
                    foreach (var gc in fileWithContents.GeneratedContents)
                    {
                        _generatedIndex[gc.Id] = (gc.FileUploadId, gc.CourseId);
                        results.Add(MapToGeneratedResponse(gc));
                    }
                }
            }
        }

        return results.OrderByDescending(r => r.CreatedAt);
    }

    public async Task<GeneratedContentResponseDto> GetGeneratedContentAsync(int id)
    {
        var generated = await FindGeneratedContentByIdAsync(id);
        if (generated == null)
            throw new KeyNotFoundException($"Contenido generado con ID {id} no encontrado");

        return MapToGeneratedResponse(generated);
    }

    public async Task<bool> DeleteHistoryAsync(int id)
    {
        var generated = await FindGeneratedContentByIdAsync(id);
        if (generated == null)
            return false;

        _generatedIndex.TryRemove(id, out _);

        // Remove from the file upload's collection if loaded
        if (_generatedIndex.TryGetValue(id, out var entry))
        {
            var file = await _fileUploadRepository.GetByIdAsync(entry.FileUploadId);
            if (file?.GeneratedContents != null)
            {
                var toRemove = file.GeneratedContents.FirstOrDefault(g => g.Id == id);
                if (toRemove != null)
                {
                    file.GeneratedContents.Remove(toRemove);
                    await _fileUploadRepository.UpdateAsync(file);
                    return true;
                }
            }
        }

        return await Task.FromResult(false);
    }

    private async Task<GeneratedContent?> FindGeneratedContentByIdAsync(int id)
    {
        // Check cache first
        if (_generatedIndex.TryGetValue(id, out var entry))
        {
            var file = await _fileUploadRepository.GetByIdAsync(entry.FileUploadId);
            var gc = file?.GeneratedContents?.FirstOrDefault(g => g.Id == id);
            if (gc != null)
                return gc;
        }

        // Fallback: search through all generated content available via file uploads
        // This is limited; in production, add a proper repository method
        return await Task.FromResult<GeneratedContent?>(null);
    }

    private static GeneratedContentResponseDto MapToGeneratedResponse(GeneratedContent gc)
    {
        return new GeneratedContentResponseDto
        {
            Id = gc.Id,
            FileId = gc.FileUploadId,
            FileOriginalName = gc.FileUpload?.OriginalFilename,
            ContentType = gc.ContentType,
            GeneratedEntityId = gc.GeneratedEntityId,
            EntityName = $"Entidad #{gc.GeneratedEntityId}",
            TopicSpecified = gc.TopicSpecified,
            GenerationConfig = gc.GenerationConfig,
            CreatedAt = gc.CreatedAt
        };
    }
}
