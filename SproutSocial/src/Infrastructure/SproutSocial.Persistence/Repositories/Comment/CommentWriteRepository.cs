namespace SproutSocial.Persistence.Repositories;

public class CommentWriteRepository : WriteRepository<Comment>, ICommentWriteRepository
{
    public CommentWriteRepository(AppDbContext context) : base(context)
    {
    }
}
