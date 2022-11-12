﻿using Microsoft.AspNetCore.Identity;

namespace SproutSocial.Domain.Entities.Identity;

public class AppUser : IdentityUser<Guid>
{
    public string? Fullname { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenEndDate { get; set; }

    public ICollection<UserTopic>? UserTopics { get; set; }
    public ICollection<Blog>? Blogs { get; set; }
    public ICollection<BlogLike>? BlogLikes { get; set; }
    public ICollection<Comment>? Comments { get; set; }
}
