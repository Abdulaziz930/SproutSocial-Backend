namespace SproutSocial.Application.DTOs.BlogDtos;

public class PostCommentDto
{
    public string Message { get; set; } = null!;
    public string BlogId { get; set; } = null!;
    public string? CommentId { get; set; }
}
