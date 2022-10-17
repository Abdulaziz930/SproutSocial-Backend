using SproutSocial.Domain.Entities.Common;

namespace SproutSocial.Application.Repositories;

public interface IWriteRepository<T>  : IRepository<T> where T : BaseEntity
{
    Task<bool> AddAsync(T entity);
    Task<bool> AddRangeAsync(ICollection<T> entities);
    bool Remove(T entity);
    bool RemoveRange(ICollection<T> entities);
    Task<bool> RemoveAsync(string id);
    bool Update(T entity);
}
