using SproutSocial.Application.DTOs.UserDtos;

namespace SproutSocial.Application.Abstractions.Services;

public interface IAuthService
{
    Task<TokenResponseDto> LoginAsync(LoginDto model, int accessTokenLifeTime);
    Task<TokenResponseDto> RefreshTokenLoginAsync(string refreshToken);
}
