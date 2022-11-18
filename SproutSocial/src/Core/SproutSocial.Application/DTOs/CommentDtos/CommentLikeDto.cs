namespace SproutSocial.Application.DTOs.CommentDtos;

public record CommentLikeDto
{
    public Guid UserId { get; init; }
    public string UserName { get; init; } = null!;
}