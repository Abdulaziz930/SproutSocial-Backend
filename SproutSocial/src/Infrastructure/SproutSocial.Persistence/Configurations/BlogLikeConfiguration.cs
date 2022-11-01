using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SproutSocial.Persistence.Configurations;

public class BlogLikeConfiguration : IEntityTypeConfiguration<BlogLike>
{
    public void Configure(EntityTypeBuilder<BlogLike> builder)
    {
        builder.Property(bl => bl.BlogId)
            .IsRequired(true);
        builder.Property(bl => bl.AppUserId)
            .IsRequired(true);

        builder
            .HasOne(bl => bl.Blog)
            .WithMany(b => b.BlogLikes)
            .HasForeignKey(bl => bl.BlogId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(bl => bl.AppUser)
            .WithMany(u => u.BlogLikes)
            .HasForeignKey(bl => bl.AppUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
