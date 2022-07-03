using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Task InsertAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null, params string[] includes);
        Task<IEnumerable<TEntity>> GetAllPagenatedAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? expression = null, params string[] includes);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? expression = null, params string[] includes);
        Task<int> GetTotalCountAsync(Expression<Func<TEntity, bool>>? expression = null, params string[] includes);
        Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> expression, params string[] includes);
        void Remove(TEntity entity);
    }
}
