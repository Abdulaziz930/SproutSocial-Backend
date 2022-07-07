using SproutSocial.Service.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task RegisterAsync(RegisterDto registerDto);
        Task<AuthenticatedResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthenticatedResponseDto> RefreshTokenAsync(TokenPostDto tokenDto);
        Task RevokeAsync(string? userName);
        Task ChangePasswordAsync(ChangePasswordDto changePasswordDto);
        Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task ResetPasswordAsync(string id, ResetPasswordDto resetPasswordDto);
        Task SelectTopicAsync(string userId, int? topicId);
    }
}
