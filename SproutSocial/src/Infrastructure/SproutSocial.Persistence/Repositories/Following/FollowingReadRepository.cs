namespace SproutSocial.Persistence.Repositories;

public class FollowingReadRepository : ReadRepository<UserFollow>, IFollowingReadRepository
{
    public FollowingReadRepository(AppDbContext context) : base(context)
    {
    }
}
