using SproutSocial.Application.DTOs.UserDtos;

namespace SproutSocial.Application.DTOs.BlogDtos;

public class BlogDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Image { get; set; } = null!;
    public UserInfoDto? UserInfo { get; set; }
    public List<TopicDto>? Topics { get; set; }
    public List<BlogLikeDto>? Likes { get; set; }
    public int LikeCount { get; set; }
}
