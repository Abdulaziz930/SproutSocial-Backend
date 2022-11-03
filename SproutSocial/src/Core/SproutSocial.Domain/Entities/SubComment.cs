using SproutSocial.Domain.Entities.Identity;

namespace SproutSocial.Domain.Entities;

public class SubComment : BaseAuditableEntity
{
    public string Message { get; set; } = null!;

    public Guid CommentId { get; set; }
    public Comment Comment { get; set; } = null!;
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
}
