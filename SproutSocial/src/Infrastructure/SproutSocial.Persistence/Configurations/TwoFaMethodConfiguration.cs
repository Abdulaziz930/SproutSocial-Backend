using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SproutSocial.Domain.Entities.Identity;

namespace SproutSocial.Persistence.Configurations;

public class TwoFaMethodConfiguration : IEntityTypeConfiguration<TwoFaMethod>
{
    public void Configure(EntityTypeBuilder<TwoFaMethod> builder)
    {
        builder.ToTable("TwoFaMethods");
        builder.Property(tfa => tfa.TwoFactorAuthMethod).IsRequired(true);
    }
}
