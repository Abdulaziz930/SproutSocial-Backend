namespace SproutSocial.Application.DTOs.UserDtos;

public record TwoFaLoginDto(string Code, string Email, string TwoFaSecurityToken);