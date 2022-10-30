using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SproutSocial.Domain.Entities.Identity;

namespace SproutSocial.Persistence.Contexts;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public AppDbContext(DbContextOptions<AppDbContext> options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Topic> Topics { get; set; } = null!;
    public DbSet<UserTopic> UserTopics { get; set; } = null!;
    public DbSet<Blog> Blogs { get; set; } = null!;
    public DbSet<BlogTopic> BlogTopics { get; set; } = null!;
    public DbSet<BlogImage> BlogImages { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}
