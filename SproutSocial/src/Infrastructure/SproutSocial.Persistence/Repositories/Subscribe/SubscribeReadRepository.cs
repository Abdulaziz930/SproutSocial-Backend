namespace SproutSocial.Persistence.Repositories;

public class SubscribeReadRepository : ReadRepository<Subscribe>, ISubscribeReadRepository
{
    public SubscribeReadRepository(AppDbContext context) : base(context)
    {
    }
}
