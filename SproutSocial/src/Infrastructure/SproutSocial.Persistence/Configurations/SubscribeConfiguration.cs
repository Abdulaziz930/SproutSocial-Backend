using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SproutSocial.Persistence.Configurations;

public class SubscribeConfiguration : IEntityTypeConfiguration<Subscribe>
{
    public void Configure(EntityTypeBuilder<Subscribe> builder)
    {
        builder.ToTable("Subscribes");
        builder.Property(x => x.Email)
            .IsRequired(true)
            .HasMaxLength(256);
    }
}
