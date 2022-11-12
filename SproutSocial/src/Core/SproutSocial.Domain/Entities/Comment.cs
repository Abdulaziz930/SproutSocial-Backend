using SproutSocial.Domain.Entities.Identity;

namespace SproutSocial.Domain.Entities;

public class Comment : BaseAuditableEntity
{
    public string Message { get; set; } = null!;

    public Guid BlogId { get; set; }
    public Blog Blog { get; set; } = null!;
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
    public ICollection<CommentLike>? CommentLikes { get; set; }
}
