namespace SproutSocial.Persistence.Repositories;

public class CommentReadRepository : ReadRepository<Comment>, ICommentReadRepository
{
    public CommentReadRepository(AppDbContext context) : base(context)
    {
    }
}
