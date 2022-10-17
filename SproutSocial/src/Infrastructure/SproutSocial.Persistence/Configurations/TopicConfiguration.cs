using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SproutSocial.Persistence.Configurations;

public class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.ToTable("Topics");
        builder.Property(t => t.Name).IsRequired(true).HasMaxLength(100);
        builder.Property(t => t.IsDeleted).HasDefaultValue(false);
    }
}
