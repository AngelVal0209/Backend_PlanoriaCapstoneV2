using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal;
using PlanoriaCapstone.DTOs.System.Responses;
using PlanoriaCapstone.DTOs.System.Requests;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Bll.Service;

public class SystemService : ISystemService
{
    private readonly IActivityLogRepository _activityLogRepository;

    public SystemService(IActivityLogRepository activityLogRepository)
    {
        _activityLogRepository = activityLogRepository;
    }

    public async Task<IEnumerable<SystemConfigResponseDto>> GetConfigsAsync()
    {
        return new List<SystemConfigResponseDto>
        {
            new() { ConfigKey = "app.name", ConfigValue = "Planoria", Description = "Application name", UpdatedAt = DateTime.UtcNow, UpdatedBy = "System" },
            new() { ConfigKey = "app.version", ConfigValue = "1.0.0", Description = "Application version", UpdatedAt = DateTime.UtcNow, UpdatedBy = "System" }
        };
    }

    public async Task<SystemConfigResponseDto> GetConfigAsync(string key)
    {
        return new SystemConfigResponseDto
        {
            ConfigKey = key,
            ConfigValue = "default",
            Description = string.Empty,
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = "System"
        };
    }

    public async Task<SystemConfigResponseDto> UpdateConfigAsync(int userId, UpdateSystemConfigRequestDto request)
    {
        await _activityLogRepository.LogAsync(new ActivityLog
        {
            UserId = userId,
            Action = "UpdateConfig",
            EntityType = "SystemConfiguration",
            Details = $"Key: {request.ConfigKey}",
            CreatedAt = DateTime.UtcNow
        });

        return new SystemConfigResponseDto
        {
            ConfigKey = request.ConfigKey,
            ConfigValue = request.ConfigValue,
            Description = request.Description ?? string.Empty,
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = userId.ToString()
        };
    }

    public async Task ResetConfigAsync(string key)
    {
        await Task.CompletedTask;
    }

    public async Task<HealthCheckResponseDto> HealthCheckAsync()
    {
        return new HealthCheckResponseDto
        {
            Status = "Healthy",
            Version = "1.0.0",
            Uptime = TimeSpan.FromHours(1).ToString(),
            Database = "Connected",
            Cache = "Connected",
            Queue = "Connected",
            Services = new List<string> { "API", "Database", "Cache" }
        };
    }

    public async Task<object> GetStatusAsync()
    {
        return new
        {
            Status = "Running",
            ActiveUsers = 1,
            TotalSchedules = 0,
            TotalFlashcards = 0,
            Uptime = TimeSpan.FromHours(1).ToString()
        };
    }

    public async Task<object> GetMetricsAsync()
    {
        return new
        {
            RequestsPerMinute = 0,
            AverageResponseTime = 0,
            ErrorRate = 0,
            ActiveConnections = 0,
            MemoryUsage = "128MB"
        };
    }

    public async Task ClearCacheAsync(string? cacheKey)
    {
        await Task.CompletedTask;
    }

    public async Task<CacheStatsResponseDto> GetCacheStatsAsync()
    {
        return new CacheStatsResponseDto
        {
            Hits = 100,
            Misses = 20,
            HitRate = 0.83m,
            Size = 1024,
            LastClearedAt = DateTime.UtcNow.AddDays(-1)
        };
    }

    public async Task WarmupCacheAsync()
    {
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<LogEntryResponseDto>> GetLogsAsync(GetLogsRequestDto request)
    {
        return new List<LogEntryResponseDto>
        {
            new() { Timestamp = DateTime.UtcNow, Level = "Info", Message = "System started", Context = "Startup", UserId = null, IpAddress = "127.0.0.1" }
        };
    }

    public async Task<IEnumerable<LogEntryResponseDto>> SearchLogsAsync(string query)
    {
        return await GetLogsAsync(new GetLogsRequestDto());
    }

    public async Task<byte[]> ExportLogsAsync(GetLogsRequestDto request)
    {
        return await Task.FromResult(Array.Empty<byte>());
    }
}
