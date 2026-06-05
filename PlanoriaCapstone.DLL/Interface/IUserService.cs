using PlanoriaCapstone.DTOs.Users.Requests;
using PlanoriaCapstone.DTOs.Users.Responses;

namespace PlanoriaCapstone.Bll.Interface;

public interface IUserService
{
    Task<UserResponseDto> GetProfileAsync(int userId);
    Task<UserResponseDto> UpdateProfileAsync(int userId, UpdateProfileRequestDto request);
    Task UploadAvatarAsync(int userId, Stream avatarStream, string fileName);
    Task DeleteAvatarAsync(int userId);
    Task<UserPreferencesResponseDto> GetPreferencesAsync(int userId);
    Task<UserPreferencesResponseDto> UpdatePreferencesAsync(int userId, UpdatePreferencesRequestDto request);
    Task ResetDefaultsAsync(int userId);
    Task<NotificationSettingsResponseDto> GetNotificationSettingsAsync(int userId);
    Task<NotificationSettingsResponseDto> UpdateNotificationSettingsAsync(int userId, UpdateNotificationSettingsRequestDto request);
    Task TestNotificationAsync(int userId);
    Task DeleteAccountAsync(int userId, DeleteAccountRequestDto request);
    Task<ExportDataResponseDto> ExportDataAsync(int userId, ExportDataRequestDto request);
    Task DeactivateAsync(int userId);
}
