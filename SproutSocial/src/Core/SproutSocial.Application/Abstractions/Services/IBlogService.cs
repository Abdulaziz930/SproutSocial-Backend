using SproutSocial.Application.DTOs.Common;

namespace SproutSocial.Application.Abstractions.Services;

public interface IBlogService
{
    Task<PagenatedListDto<BlogDto>> GetAllBlogsAsync(int page);
    Task<BlogDto> GetBlogByIdAsync(string id);
    Task<bool> CreateBlogAsync(CreateBlogDto blog);
    Task<bool> UpdateBlogAsync(string id, UpdateBlogDto blog);
    Task<bool> DeleteBlogAsync(string id);
}
