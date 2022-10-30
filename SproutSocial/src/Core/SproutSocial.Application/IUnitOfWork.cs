using SproutSocial.Application.Repositories;

namespace SproutSocial.Application;

public interface IUnitOfWork
{
    ITopicReadRepository TopicReadRepository { get; }
    ITopicWriteRepository TopicWriteRepository { get; }
    IBlogReadRepository BlogReadRepository { get; }
    IBlogWriteRepository BlogWriteRepository { get; }

    Task<int> SaveAsync();
}
