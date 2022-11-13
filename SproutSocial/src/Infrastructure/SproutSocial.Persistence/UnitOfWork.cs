﻿namespace SproutSocial.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private ITopicReadRepository? _topicReadRepository;
    private ITopicWriteRepository? _topicWriteRepository;
    private IBlogReadRepository? _blogReadRepository;
    private IBlogWriteRepository? _blogWriteRepository;
    private ICommentReadRepository? _commentReadRepository;
    private ICommentWriteRepository? _commentWriteRepository;

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

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default) 
        => await _context.SaveChangesAsync(cancellationToken);
}
