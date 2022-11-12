namespace SproutSocial.Application.DTOs.CommentDtos;

public class CommentLikeDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;
}
