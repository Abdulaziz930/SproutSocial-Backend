using SproutSocial.Domain.Entities.Common;
using System.Linq.Expressions;

namespace SproutSocial.Application.Repositories;

public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAll(bool tracking = true, params string[] includes);
    IQueryable<T> GetFiltered(Expression<Func<T, bool>> filter, int pageIndex, int pageSize, bool tracking = true, params string[] includes);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, bool tracking = true, params string[] includes);
    Task<T> GetByIdAsync(string id, bool tracking = true, params string[] includes);
    Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression, params string[] includes);
    Task<int> GetTotalCountAsync(Expression<Func<T, bool>> expression, params string[] includes);
}
