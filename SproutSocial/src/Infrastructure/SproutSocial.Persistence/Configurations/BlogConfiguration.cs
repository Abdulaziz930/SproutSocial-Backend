using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SproutSocial.Persistence.Configurations;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.ToTable("Blogs");
        builder.Property(b => b.Title).IsRequired(true).HasMaxLength(100);
        builder.Property(b => b.Content).IsRequired(true).HasMaxLength(1500);
        builder.Property(b => b.IsDeleted).HasDefaultValue(false);
        builder.Property(b => b.AppUserId).IsRequired(true);
        builder.HasOne(b => b.AppUser).WithMany(u => u.Blogs);
        builder.HasOne(b => b.BlogImage).WithOne(bi => bi.Blog).HasForeignKey<BlogImage>(bi => bi.BlogId);
    }
}
