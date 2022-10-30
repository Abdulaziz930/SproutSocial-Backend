namespace SproutSocial.Domain.Entities;

public class BlogTopic : BaseEntity
{
    public Guid BlogId { get; set; }
    public Blog Blog { get; set; } = null!;
    public Guid TopicId { get; set; }
    public Topic Topic { get; set; } = null!;
}
