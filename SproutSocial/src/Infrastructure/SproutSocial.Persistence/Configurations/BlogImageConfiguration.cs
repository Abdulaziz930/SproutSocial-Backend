using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SproutSocial.Persistence.Configurations;

public class BlogImageConfiguration : IEntityTypeConfiguration<BlogImage>
{
    public void Configure(EntityTypeBuilder<BlogImage> builder)
    {
        builder.ToTable("BlogImages");
        builder.Property(bi => bi.FileName).IsRequired(true).HasMaxLength(250);
        builder.Property(bi => bi.Path).IsRequired(true).HasMaxLength(450);
        builder.Property(bi => bi.Storage).IsRequired(true).HasMaxLength(100);
    }
}
