using SproutSocial.Application.DTOs.UserDtos;

namespace SproutSocial.Application.DTOs.BlogDtos;

public record BlogDto(
    Guid Id,
    string Title,
    string Content,
    string Image,
    UserInfoDto? UserInfo,
    List<TopicDto>? Topics,
    List<BlogLikeDto>? Likes,
    int LikeCount
);