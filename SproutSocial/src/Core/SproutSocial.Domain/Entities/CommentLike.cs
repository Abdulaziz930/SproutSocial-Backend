using SproutSocial.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SproutSocial.Domain.Entities;

public class CommentLike : BaseAuditableEntity
{
    public Guid CommentId { get; set; }
    public Comment Comment { get; set; } = null!;
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;


    [NotMapped]
    public override DateTime LastModifiedDate { get; set; }
    [NotMapped]
    public override string? LastModifiedBy { get; set; }
    [NotMapped]
    public override bool IsDeleted { get; set; }
}
