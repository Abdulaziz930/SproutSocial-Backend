using Microsoft.EntityFrameworkCore;
using SproutSocial.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Data.Repositories.EFRepository
{
    public class Repository<TEntity, IContext> : IRepository<TEntity> where TEntity : class, new()
                                                                      where IContext : DbContext 
    {
        protected readonly IContext Context;

        public Repository(IContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null, params string[] includes)
        {
            var query = Context.Set<TEntity>().AsQueryable();

            if (includes != null && includes.Length > 0)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }

            if(expression != null)
                return await query.Where(expression).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllPagenatedAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? expression = null, params string[] includes)
        {
            var query = Context.Set<TEntity>().AsQueryable();

            if (includes != null && includes.Length > 0)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }

            if(expression != null)
                return await query.Where(expression).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? expression = null, params string[] includes)
        {
            var query = Context.Set<TEntity>().AsQueryable();

            if (includes != null && includes.Length > 0)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }

            if(expression != null)
                return await query.FirstOrDefaultAsync(expression);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> GetTotalCountAsync(Expression<Func<TEntity, bool>>? expression = null, params string[] includes)
        {
            var query = Context.Set<TEntity>().AsQueryable();

            if (includes != null && includes.Length > 0)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }

            if(expression != null)
                return await query.CountAsync(expression);

            return await query.CountAsync();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public async Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> expression, params string[] includes)
        {
            var query = Context.Set<TEntity>().AsQueryable();

            if (includes != null && includes.Length > 0)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }

            return await query.AnyAsync(expression);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }
    }
}
