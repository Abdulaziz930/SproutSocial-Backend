using SproutSocial.Application.DTOs.UserDtos;

namespace SproutSocial.Application.DTOs.CommentDtos;

public record CommentDto(
    Guid Id,
    string Message,
    UserInfoDto? UserInfo,
    List<CommentLikeDto>? Likes,
    int LikeCount
);