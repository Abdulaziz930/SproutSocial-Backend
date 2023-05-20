using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SproutSocial.Domain.Entities.Identity;

namespace SproutSocial.Persistence.Configurations;

public class UserTwoFaMethodConfiguration : IEntityTypeConfiguration<UserTwoFaMethod>
{
    public void Configure(EntityTypeBuilder<UserTwoFaMethod> builder)
    {
        builder.Property(ut => ut.IsSelected)
            .HasDefaultValue(false);
        builder.Property(ut => ut.UserId)
            .IsRequired(true);
        builder.Property(ut => ut.TwoFaMethodId)
            .IsRequired(true);
        builder
            .HasOne(ut => ut.TwoFaMethod)
            .WithMany(t => t.UserTwoFaMethods)
            .HasForeignKey(ut => ut.TwoFaMethodId);
        builder
            .HasOne(ut => ut.AppUser)
            .WithMany(u => u.UserTwoFaMethods)
            .HasForeignKey(ut => ut.UserId);
    }
}