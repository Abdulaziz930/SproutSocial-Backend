namespace SproutSocial.Persistence.Repositories;

public class TopicReadRepository : ReadRepository<Topic>, ITopicReadRepository
{
    public TopicReadRepository(AppDbContext context) : base(context)
    {
    }
}
