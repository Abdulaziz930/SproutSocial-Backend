namespace SproutSocial.Application.DTOs.UserDtos;

public class CreateUserDto
{
    public string? Fullname { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PasswordConfirm { get; set; } = null!;
}
