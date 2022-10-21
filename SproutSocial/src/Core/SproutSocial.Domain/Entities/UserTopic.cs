using SproutSocial.Domain.Entities.Identity;

namespace SproutSocial.Domain.Entities;

public class UserTopic : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
    public Guid TopicId { get; set; }
    public Topic Topic { get; set; } = null!;
}
