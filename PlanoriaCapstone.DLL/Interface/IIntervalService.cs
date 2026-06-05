using PlanoriaCapstone.DTOs.Cronograma.Responses;

namespace PlanoriaCapstone.Bll.Interface;

public interface IIntervalService
{
    Task<IntervalResponseDto> CreateAsync(int scheduleId, IntervalResponseDto request);
    Task<IntervalResponseDto> UpdateAsync(int intervalId, IntervalResponseDto request);
    Task<bool> DeleteAsync(int intervalId);
    Task ReorderAsync(int scheduleId, List<int> intervalIds);
    Task<IntervalResponseDto> GetActiveIntervalAsync(int scheduleId);
    Task StartTimerAsync(int intervalId);
    Task PauseTimerAsync(int intervalId);
    Task ResumeTimerAsync(int intervalId);
    Task StopTimerAsync(int intervalId);
    Task<IEnumerable<IntervalResponseDto>> GetTemplatesAsync();
    Task<IntervalResponseDto> CreateTemplateAsync(IntervalResponseDto request);
    Task<bool> DeleteTemplateAsync(int templateId);
    Task ApplyTemplateAsync(int scheduleId, int templateId);
}
