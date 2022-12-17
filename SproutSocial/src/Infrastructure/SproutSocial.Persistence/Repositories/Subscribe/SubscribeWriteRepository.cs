namespace SproutSocial.Persistence.Repositories;

public class SubscribeWriteRepository : WriteRepository<Subscribe>, ISubscribeWriteRepository
{
    public SubscribeWriteRepository(AppDbContext context) : base(context)
    {
    }
}
