namespace SproutSocial.Persistence.Repositories;

public class BlogReadRepository : ReadRepository<Blog>, IBlogReadRepository
{
    public BlogReadRepository(AppDbContext context) : base(context)
    {
    }
}
