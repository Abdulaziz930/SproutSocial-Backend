namespace SproutSocial.Application.Abstractions.Services;

public interface IBlogService
{
    Task<List<BlogDto>> GetAllBlogsAsync();
    Task<BlogDto> GetBlogByIdAsync(string id);
    Task<bool> CreateBlogAsync(CreateBlogDto blog);
    Task<bool> UpdateBlogAsync(string id, UpdateBlogDto blog);
    Task<bool> DeleteBlogAsync(string id);
}
