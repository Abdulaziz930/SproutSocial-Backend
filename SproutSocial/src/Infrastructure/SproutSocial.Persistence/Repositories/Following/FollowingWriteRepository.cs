namespace SproutSocial.Persistence.Repositories;

public class FollowingWriteRepository : WriteRepository<UserFollow>, IFollowingWriteRepository
{
    public FollowingWriteRepository(AppDbContext context) : base(context)
    {
    }
}
