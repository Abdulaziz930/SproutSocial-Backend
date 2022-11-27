using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SproutSocial.Persistence.Configurations;

public class UserFollowConfiguration : IEntityTypeConfiguration<UserFollow>
{
    public void Configure(EntityTypeBuilder<UserFollow> builder)
    {
        builder.Property(uf => uf.FollowingUserId)
            .IsRequired(true);
        builder.Property(uf => uf.FollowedUserId)
            .IsRequired(true);
        builder.Property(uf => uf.IsConfirmed)
            .HasDefaultValue(false);

        builder
            .HasOne(uf => uf.FollowingUser)
            .WithMany(u => u.FollowRequestsMade)
            .HasForeignKey(uf => uf.FollowingUserId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(uf => uf.FollowedUser)
            .WithMany(u => u.FollowRequestsAccepted)
            .HasForeignKey(uf => uf.FollowedUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
