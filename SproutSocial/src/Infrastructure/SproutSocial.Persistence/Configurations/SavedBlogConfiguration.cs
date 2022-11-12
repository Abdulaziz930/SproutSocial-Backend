using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SproutSocial.Persistence.Configurations;

public class SavedBlogConfiguration : IEntityTypeConfiguration<SavedBlog>
{
    public void Configure(EntityTypeBuilder<SavedBlog> builder)
    {
        builder.Property(sb => sb.BlogId)
            .IsRequired(true);
        builder.Property(sb => sb.AppUserId)
            .IsRequired(true);

        builder
            .HasOne(sb => sb.Blog)
            .WithMany(b => b.SavedBlogs)
            .HasForeignKey(sb => sb.BlogId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(sb => sb.AppUser)
            .WithMany(u => u.SavedBlogs)
            .HasForeignKey(sb => sb.AppUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
