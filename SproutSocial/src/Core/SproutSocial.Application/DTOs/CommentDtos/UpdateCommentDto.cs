namespace SproutSocial.Application.DTOs.CommentDtos;

public record UpdateCommentDto
{
    public string Message { get; init; } = null!;
}
