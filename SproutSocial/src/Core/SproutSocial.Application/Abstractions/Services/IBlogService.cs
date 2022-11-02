using SproutSocial.Application.DTOs.Common;

namespace SproutSocial.Application.Abstractions.Services;

public interface IBlogService
{
    Task<PagenatedListDto<BlogDto>> GetAllBlogsAsync(int page, string? search);
    Task<BlogDto> GetBlogByIdAsync(string id);
    Task<bool> CreateBlogAsync(CreateBlogDto blog);
    Task<bool> UpdateBlogAsync(string id, UpdateBlogDto blog);
    Task<bool> DeleteBlogAsync(string id);
    Task<bool> LikeBlogAsync(string id);
    Task<bool> UnLikeBlogAsync(string id);
}
