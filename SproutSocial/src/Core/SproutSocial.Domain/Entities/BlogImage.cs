namespace SproutSocial.Domain.Entities;

public class BlogImage : FileEntity
{
    public Guid BlogId { get; set; }
    public Blog Blog { get; set; } = null!;
}
