namespace SproutSocial.Application.DTOs.UserDtos;

public record AddUserTopicReponseDto
{
    public bool IsSuccess { get; init; }
    public string? Message { get; init; }
}
