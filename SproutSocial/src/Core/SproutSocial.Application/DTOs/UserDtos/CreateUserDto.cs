namespace SproutSocial.Application.DTOs.UserDtos;

public record CreateUserDto(
    string? Fullname, 
    string Username, 
    string Email, 
    string Password, 
    string PasswordConfirm
);
