namespace SproutSocial.Application.Features.Queries.Blog.GetAllBlogs;

public class GetAllBlogsQueryRequest : IRequest<GetAllBlogsQueryResponse>
{
    public int Page { get; set; } = 1;
    public string? Search { get; set; }
}