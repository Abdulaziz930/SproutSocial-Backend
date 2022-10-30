namespace SproutSocial.Application.Features.Queries.Blog.GetBlogById;

public class GetBlogByIdQueryRequest : IRequest<GetBlogByIdQueryResponse>
{
    public string? Id { get; set; }
}