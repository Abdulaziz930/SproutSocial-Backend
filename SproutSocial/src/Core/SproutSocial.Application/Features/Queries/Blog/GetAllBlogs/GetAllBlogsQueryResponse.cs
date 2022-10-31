namespace SproutSocial.Application.Features.Queries.Blog.GetAllBlogs;

public class GetAllBlogsQueryResponse
{
    public List<BlogDto> Blogs { get; init; } = null!;
    public int PageIndex { get; init; }
    public int TotalPages { get; init; }
    public bool HasNext { get; init; }
    public bool HasPrev { get; init; }
}
