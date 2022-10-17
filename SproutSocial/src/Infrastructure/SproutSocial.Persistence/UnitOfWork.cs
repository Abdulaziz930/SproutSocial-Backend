namespace SproutSocial.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private ITopicReadRepository? _topicReadRepository;
    private ITopicWriteRepository? _topicWriteRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public ITopicReadRepository TopicReadRepository => _topicReadRepository = _topicReadRepository ?? new TopicReadRepository(_context);
    public ITopicWriteRepository TopicWriteRepository => _topicWriteRepository = _topicWriteRepository ?? new TopicWriteRepository(_context);

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
}
