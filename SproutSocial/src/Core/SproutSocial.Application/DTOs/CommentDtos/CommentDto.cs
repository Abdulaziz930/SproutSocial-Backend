﻿using SproutSocial.Application.DTOs.UserDtos;

namespace SproutSocial.Application.DTOs.CommentDtos;

public class CommentDto
{
    public Guid Id { get; set; }
    public string Message { get; set; } = null!;
    public UserInfoDto? UserInfo { get; set; }
    public List<CommentLikeDto>? Likes { get; set; }
    public int LikeCount { get; set; }
}
