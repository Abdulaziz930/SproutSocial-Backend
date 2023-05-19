using SproutSocial.Domain.Enums;

namespace SproutSocial.Application.DTOs.UserDtos;

public class LoginResponseDto
{
    public bool RequiresTwoFactor { get; set; }
    public TwoFactorAuthMethod? TwoFactorAuthMethod { get; set; }
    public string? TwoFaSecurityToken { get; set; }
    public TokenResponseDto? TokenResponse { get; set; }
}