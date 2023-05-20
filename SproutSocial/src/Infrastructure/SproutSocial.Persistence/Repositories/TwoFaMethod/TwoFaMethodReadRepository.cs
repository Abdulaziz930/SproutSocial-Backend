using SproutSocial.Domain.Entities.Identity;

namespace SproutSocial.Persistence.Repositories;

public class TwoFaMethodReadRepository : ReadRepository<TwoFaMethod>, ITwoFaMethodReadRepository
{
    public TwoFaMethodReadRepository(AppDbContext context) : base(context)
    {
    }
}