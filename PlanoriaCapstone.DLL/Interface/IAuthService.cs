using PlanoriaCapstone.DTOs.Auth.Requests;
using PlanoriaCapstone.DTOs.Auth.Responses;

namespace PlanoriaCapstone.Bll.Interface;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    Task LogoutAsync(int userId);
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
    Task VerifyEmailAsync(VerifyEmailRequestDto request);
    Task<VerificationSentResponseDto> ResendVerificationAsync(ResendVerificationRequestDto request);
    Task ForgotPasswordAsync(ForgotPasswordRequestDto request);
    Task ResetPasswordAsync(ResetPasswordRequestDto request);
    Task ChangePasswordAsync(int userId, ChangePasswordRequestDto request);
}
