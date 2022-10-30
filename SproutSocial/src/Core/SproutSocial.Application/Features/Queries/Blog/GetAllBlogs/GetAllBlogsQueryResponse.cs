using SproutSocial.Application.DTOs.UserDtos;

namespace SproutSocial.Application.Features.Queries.Blog.GetAllBlogs;

public class GetAllBlogsQueryResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Image { get; set; } = null!;
    public UserInfoDto UserInfo { get; set; } = null!;
    public List<TopicDto> Topics { get; set; } = null!;
}
