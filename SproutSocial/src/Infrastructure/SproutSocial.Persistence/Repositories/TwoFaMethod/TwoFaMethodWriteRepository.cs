using SproutSocial.Domain.Entities.Identity;

namespace SproutSocial.Persistence.Repositories;

public class TwoFaMethodWriteRepository : WriteRepository<TwoFaMethod>, ITwoFaMethodWriteRepository
{
    public TwoFaMethodWriteRepository(AppDbContext context) : base(context)
    {
    }
}