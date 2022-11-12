using SproutSocial.Domain.Entities.Identity;

namespace SproutSocial.Domain.Entities;

public class Blog : BaseAuditableEntity
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;

    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
    public BlogImage BlogImage { get; set; } = null!;
    public ICollection<BlogTopic>? BlogTopics { get; set; }
    public ICollection<BlogLike>? BlogLikes { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<SavedBlog>? SavedBlogs { get; set; }
}
