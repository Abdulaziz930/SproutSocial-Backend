namespace SproutSocial.Domain.Entities;

public class Topic : BaseAuditableEntity
{
    public string Name { get; set; } = null!;

    public ICollection<UserTopic>? UserTopics { get; set; }
}
