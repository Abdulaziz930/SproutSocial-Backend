namespace SproutSocial.Persistence.Repositories;

public class TopicWriteRepository : WriteRepository<Topic>, ITopicWriteRepository
{
    public TopicWriteRepository(AppDbContext context) : base(context)
    {
    }
}
