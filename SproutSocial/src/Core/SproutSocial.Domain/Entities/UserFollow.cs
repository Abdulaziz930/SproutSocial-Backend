using SproutSocial.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SproutSocial.Domain.Entities;

public class UserFollow : BaseAuditableEntity
{
    public Guid FollowingUserId { get; set; }
    public AppUser FollowingUser { get; set; } = null!;
    public Guid FollowerUserId { get; set; }
    public AppUser FollowerUser { get; set; } = null!;
    public bool IsConfirmed { get; set; }

    [NotMapped]
    public override bool IsDeleted { get; set; }
}
