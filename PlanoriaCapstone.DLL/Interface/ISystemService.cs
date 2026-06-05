using PlanoriaCapstone.DTOs.System.Responses;
using PlanoriaCapstone.DTOs.System.Requests;

namespace PlanoriaCapstone.Bll.Interface;

public interface ISystemService
{
    Task<IEnumerable<SystemConfigResponseDto>> GetConfigsAsync();
    Task<SystemConfigResponseDto> GetConfigAsync(string key);
    Task<SystemConfigResponseDto> UpdateConfigAsync(int userId, UpdateSystemConfigRequestDto request);
    Task ResetConfigAsync(string key);
    Task<HealthCheckResponseDto> HealthCheckAsync();
    Task<object> GetStatusAsync();
    Task<object> GetMetricsAsync();
    Task ClearCacheAsync(string? cacheKey);
    Task<CacheStatsResponseDto> GetCacheStatsAsync();
    Task WarmupCacheAsync();
    Task<IEnumerable<LogEntryResponseDto>> GetLogsAsync(GetLogsRequestDto request);
    Task<IEnumerable<LogEntryResponseDto>> SearchLogsAsync(string query);
    Task<byte[]> ExportLogsAsync(GetLogsRequestDto request);
}
