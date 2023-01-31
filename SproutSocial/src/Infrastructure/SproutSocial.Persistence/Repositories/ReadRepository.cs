using System.Linq.Expressions;

namespace SproutSocial.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;

    public ReadRepository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll(bool tracking = true, params string[] includes)
    {
        var query = Table.AsQueryable();
        if (includes != null && includes.Length > 0)
        {
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
        }

        return !tracking ? query.AsNoTracking() : query;
    }

    public IQueryable<T> GetFiltered(Expression<Func<T, bool>> filter, int pageIndex, int pageSize, bool tracking = true, params string[] includes)
    {
        var query = Table.Where(filter);
        if (includes != null && includes.Length > 0)
        {
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
        }

        query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

        return !tracking ? query.AsNoTracking() : query;
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, bool tracking = true, params string[] includes)
    {
        var query = Table.AsQueryable();
        if (includes != null && includes.Length > 0)
        {
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
        }

        if (!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(filter);
    }

    public async Task<T>? GetByIdAsync(string id, bool tracking = true, params string[] includes)
    {
        var query = Table.AsQueryable();
        if (includes != null && includes.Length > 0)
        {
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
        }

        if (!tracking)
            query = query.AsNoTracking();

        if (Guid.TryParse(id, out var entityId))
            return await query.FirstOrDefaultAsync(e => e.Id == entityId);

        return null;
    }

    public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        var query = Table.AsQueryable();

        if (includes != null && includes.Length > 0)
        {
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
        }

        return await query.AnyAsync(expression);
    }

    public async Task<int> GetTotalCountAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        var query = Table.AsQueryable();

        if (includes != null && includes.Length > 0)
        {
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
        }

        return await query.AsNoTracking().CountAsync(expression);
    }
}
