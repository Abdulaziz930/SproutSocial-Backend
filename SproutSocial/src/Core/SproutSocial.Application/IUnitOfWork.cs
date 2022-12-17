using SproutSocial.Application.Repositories;

namespace SproutSocial.Application;

public interface IUnitOfWork
{
    ITopicReadRepository TopicReadRepository { get; }
    ITopicWriteRepository TopicWriteRepository { get; }
    IBlogReadRepository BlogReadRepository { get; }
    IBlogWriteRepository BlogWriteRepository { get; }
    ICommentReadRepository CommentReadRepository { get; }
    ICommentWriteRepository CommentWriteRepository { get; }
    IFollowingReadRepository FollowingReadRepository { get; }
    IFollowingWriteRepository FollowingWriteRepository { get; }
    ISubscribeReadRepository SubscribeReadRepository { get; }
    ISubscribeWriteRepository SubscribeWriteRepository { get; }

    Task<int> SaveAsync(CancellationToken cancellationToken = default);
}
