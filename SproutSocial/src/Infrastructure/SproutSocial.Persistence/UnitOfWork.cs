namespace SproutSocial.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private ITopicReadRepository? _topicReadRepository;
    private ITopicWriteRepository? _topicWriteRepository;
    private IBlogReadRepository? _blogReadRepository;
    private IBlogWriteRepository? _blogWriteRepository;
    private ICommentReadRepository? _commentReadRepository;
    private ICommentWriteRepository? _commentWriteRepository;
    private IFollowingReadRepository? _followingReadRepository;
    private IFollowingWriteRepository? _followingWriteRepository;
    private ISubscribeReadRepository? _subscribeReadRepository;
    private ISubscribeWriteRepository? _subscribeWriteRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public ITopicReadRepository TopicReadRepository => _topicReadRepository = _topicReadRepository ?? new TopicReadRepository(_context);
    public ITopicWriteRepository TopicWriteRepository => _topicWriteRepository = _topicWriteRepository ?? new TopicWriteRepository(_context);
    public IBlogReadRepository BlogReadRepository => _blogReadRepository = _blogReadRepository ?? new BlogReadRepository(_context);
    public IBlogWriteRepository BlogWriteRepository => _blogWriteRepository = _blogWriteRepository ?? new BlogWriteRepository(_context);
    public ICommentReadRepository CommentReadRepository => _commentReadRepository = _commentReadRepository ?? new CommentReadRepository(_context);
    public ICommentWriteRepository CommentWriteRepository => _commentWriteRepository = _commentWriteRepository ?? new CommentWriteRepository(_context);
    public IFollowingReadRepository FollowingReadRepository => _followingReadRepository = _followingReadRepository ?? new FollowingReadRepository(_context);
    public IFollowingWriteRepository FollowingWriteRepository => _followingWriteRepository = _followingWriteRepository ?? new FollowingWriteRepository(_context);
    public ISubscribeReadRepository SubscribeReadRepository => _subscribeReadRepository = _subscribeReadRepository ?? new SubscribeReadRepository(_context);
    public ISubscribeWriteRepository SubscribeWriteRepository => _subscribeWriteRepository = _subscribeWriteRepository ?? new SubscribeWriteRepository(_context);

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default) 
        => await _context.SaveChangesAsync(cancellationToken);
}
