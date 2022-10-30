namespace SproutSocial.Persistence.Repositories;

public class BlogWriteRepository : WriteRepository<Blog>, IBlogWriteRepository
{
    public BlogWriteRepository(AppDbContext context) : base(context)
    {
    }
}
