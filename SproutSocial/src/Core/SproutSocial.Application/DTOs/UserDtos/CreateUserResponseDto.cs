namespace SproutSocial.Application.DTOs.UserDtos;

public record CreateUserResponseDto
{
    public bool IsSuccess { get; init; }
    public string? Message { get; init; }
}