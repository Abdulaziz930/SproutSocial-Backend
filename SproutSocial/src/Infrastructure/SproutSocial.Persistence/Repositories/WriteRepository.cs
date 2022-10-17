using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SproutSocial.Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;

    public WriteRepository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<bool> AddAsync(T entity)
    {
        EntityEntry<T> entityEntry = await Table.AddAsync(entity);
        return entityEntry.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(ICollection<T> entities)
    {
        await _context.AddRangeAsync(entities);
        return true;
    }

    public bool Remove(T entity)
    {
        EntityEntry<T> entityEntry = _context.Remove(entity);
        return entityEntry.State == EntityState.Deleted;
    }

    public async Task<bool> RemoveAsync(string id)
    {
        if (Guid.TryParse(id, out var entityId))
        {
            T model = await Table.SingleOrDefaultAsync(e => e.Id == entityId);
            return Remove(model);
        }

        return false;
    }

    public bool RemoveRange(ICollection<T> entities)
    {
        Table.RemoveRange(entities);
        return true;
    }

    public bool Update(T entity)
    {
        EntityEntry<T> entityEntry = Table.Update(entity);
        return entityEntry.State == EntityState.Modified;
    }
}
