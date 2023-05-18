using SproutSocial.Application.DTOs.UserDtos;

namespace SproutSocial.Application.Abstractions.Services;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginDto model, int accessTokenLifeTime);
    Task<TokenResponseDto> RefreshTokenLoginAsync(string refreshToken);
    Task<ConfirmEmailResponseDto> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto);
    Task<TokenResponseDto> TwoFaLoginAsync(TwoFaLoginDto twoFaLoginDto, int accessTokenLifeTime);
    Task<string> GetGAuthSetup(string email);
    Task SetGAuth(SetGAuthDto setGAuthDto);
    Task<TokenResponseDto> GAuthLogin(TwoFaLoginDto twoFaLoginDto, int accessTokenLifeTime);
}
