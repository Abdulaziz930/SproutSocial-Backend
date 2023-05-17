namespace SproutSocial.Application.DTOs.UserDtos;

public class LoginResponseDto
{
    public bool RequiresTwoFactor { get; set; }
    public TokenResponseDto? TokenResponse { get; set; }
}