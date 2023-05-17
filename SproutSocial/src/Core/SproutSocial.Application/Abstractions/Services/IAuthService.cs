using SproutSocial.Application.DTOs.UserDtos;

namespace SproutSocial.Application.Abstractions.Services;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginDto model, int accessTokenLifeTime);
    Task<TokenResponseDto> RefreshTokenLoginAsync(string refreshToken);
    Task<ConfirmEmailResponseDto> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto);
    Task<TokenResponseDto> TwoFaLoginAsync(TwoFaLoginDto twoFaLoginDto, int accessTokenLifeTime);
}
