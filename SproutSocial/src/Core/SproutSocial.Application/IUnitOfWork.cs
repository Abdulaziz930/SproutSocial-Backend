using SproutSocial.Application.Repositories;

namespace SproutSocial.Application;

public interface IUnitOfWork
{
    ITopicReadRepository TopicReadRepository { get; }
    ITopicWriteRepository TopicWriteRepository { get; }

    Task<int> SaveAsync();
}
