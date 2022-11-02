using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SproutSocial.Application.Abstractions.Services;
using SproutSocial.Application.Abstractions.Storage;
using SproutSocial.Application.DTOs.BlogDtos;
using SproutSocial.Application.DTOs.Common;
using SproutSocial.Application.Exceptions;
using SproutSocial.Domain.Entities.Identity;

namespace SproutSocial.Persistence.Services;

public class BlogService : IBlogService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStorageService _storageService;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public BlogService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IStorageService storageService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _storageService = storageService;
        _mapper = mapper;
    }

    public async Task<bool> CreateBlogAsync(CreateBlogDto blog)
    {
        var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
            throw new UserNotFoundException($"User not found by name: {userName}");

        Blog newBlog = new()
        {
            Title = blog.Title,
            Content = blog.Content,
            AppUser = user,
        };

        (string fileName, string pathOrContainerName) data = await _storageService.UploadAsync("blog-images", blog.FormFile);

        BlogImage blogImage = new()
        {
            FileName = data.fileName,
            Path = data.pathOrContainerName,
            Storage = _storageService.StorageName
        };

        List<BlogTopic> blogTopics = new();
        foreach (var topicId in blog.TopicIds)
        {
            blogTopics.Add(new()
            {
                TopicId = Guid.Parse(topicId),
                BlogId = newBlog.Id
            });
        }
        newBlog.BlogTopics = blogTopics;
        newBlog.BlogImage = blogImage;

        var result = await _unitOfWork.BlogWriteRepository.AddAsync(newBlog);
        await _unitOfWork.SaveAsync();

        return result;
    }

    public async Task<PagenatedListDto<BlogDto>> GetAllBlogsAsync(int page, string? search)
    {
        if (page < 1) throw new PageFormatException();

        var blogs = await _unitOfWork.BlogReadRepository.GetFiltered(b => !string.IsNullOrWhiteSpace(search) ? b.Title.ToLower().Contains(search.ToLower()) : true && !b.IsDeleted, page, 5, tracking: false, "AppUser", "BlogImage", "BlogTopics.Topic").ToListAsync();
        if (blogs == null || blogs.Count == 0)
            throw new NotFoundException("There is no any blog items");

        var blogsCount = await _unitOfWork.BlogReadRepository.GetTotalCountAsync(b => !string.IsNullOrWhiteSpace(search) ? b.Title.ToLower().Contains(search.ToLower()) : true && !b.IsDeleted);

        var blogsDto = _mapper.Map<IEnumerable<BlogDto>>(blogs);

        PagenatedListDto<BlogDto> pagenatedListDto = new(blogsDto, blogsCount, page, 5);

        return pagenatedListDto;
    }

    public async Task<BlogDto> GetBlogByIdAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var blog = await _unitOfWork.BlogReadRepository.GetSingleAsync(b => b.Id == Guid.Parse(id) && !b.IsDeleted, tracking: false, "AppUser", "BlogImage", "BlogTopics.Topic");
        if(blog == null)
            throw new NotFoundException($"Blog not found with id: {id}");

        var blogDto = _mapper.Map<BlogDto>(blog);
        return blogDto;
    }

    public async Task<bool> UpdateBlogAsync(string id, UpdateBlogDto blog)
    {
        ArgumentNullException.ThrowIfNull(id);

        var dbBlog = await _unitOfWork.BlogReadRepository.GetSingleAsync(b => b.Id == Guid.Parse(id) && !b.IsDeleted, tracking: true, "AppUser", "BlogImage", "BlogTopics.Topic");
        if (dbBlog == null)
            throw new NotFoundException($"Blog not found with id: {id}");

        if(blog.FormFile != null)
        {
            _storageService.Delete("blog-images", dbBlog.BlogImage.FileName);

            (string fileName, string pathOrContainerName) data = await _storageService.UploadAsync("blog-images", blog.FormFile);

            dbBlog.BlogImage.Path = data.pathOrContainerName;
            dbBlog.BlogImage.FileName = data.fileName;
            dbBlog.BlogImage.Storage = _storageService.StorageName;
        }

        if(blog.TopicIds != null && blog.TopicIds.Count != 0)
        {
            List<BlogTopic> blogTopics = new();
            foreach (var topicId in blog.TopicIds)
            {
                blogTopics.Add(new()
                {
                    TopicId = Guid.Parse(topicId),
                    BlogId = dbBlog.Id
                });
            }
            dbBlog.BlogTopics = blogTopics;
        }

        dbBlog.Title = blog.Title;
        dbBlog.Content = blog.Content;

        bool result = _unitOfWork.BlogWriteRepository.Update(dbBlog);
        await _unitOfWork.SaveAsync();

        return result;
    }

    public async Task<bool> DeleteBlogAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var blog = await _unitOfWork.BlogReadRepository.GetSingleAsync(b => b.Id == Guid.Parse(id) && !b.IsDeleted, tracking: true);
        if (blog == null)
            throw new NotFoundException($"Blog not found with id: {id}");

        blog.IsDeleted = true;

        await _unitOfWork.SaveAsync();

        return true;
    }

    public async Task<bool> LikeBlogAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var user = _httpContextAccessor.HttpContext.User.Identity;
        if (!user.IsAuthenticated)
            throw new AuthenticationFailException("Please login to like any post");

        var dbUser = await _userManager.FindByNameAsync(user.Name);
        if (dbUser is null)
            throw new UserNotFoundException($"User not found by name: {user.Name}");

        var blog = await _unitOfWork.BlogReadRepository.GetSingleAsync(b => b.Id == Guid.Parse(id) && !b.IsDeleted, tracking: true);
        if (blog is null)
            throw new NotFoundException($"Blog not found with id: {id}");

        BlogLike blogLike = new()
        {
            BlogId = blog.Id,
            AppUserId = dbUser.Id
        };

        blog.BlogLikes = blog.BlogLikes ?? new List<BlogLike>();
        blog.BlogLikes.Add(blogLike);

        var result = _unitOfWork.BlogWriteRepository.Update(blog);
        await _unitOfWork.SaveAsync();

        return result;
    }

    public async Task<bool> UnLikeBlogAsync(string id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var user = _httpContextAccessor.HttpContext.User.Identity;
        if (!user.IsAuthenticated)
            throw new AuthenticationFailException("Please login to like any post");

        var dbUser = await _userManager.FindByNameAsync(user.Name);
        if (dbUser is null)
            throw new UserNotFoundException($"User not found by name: {user.Name}");

        var blog = await _unitOfWork.BlogReadRepository.GetSingleAsync(b => b.Id == Guid.Parse(id) && !b.IsDeleted, tracking: true, "BlogLikes");
        if (blog is null)
            throw new NotFoundException($"Blog not found with id: {id}");

        var likedBlog = blog.BlogLikes.FirstOrDefault(b => b.BlogId == Guid.Parse(id) && b.AppUserId == dbUser.Id);

        blog.BlogLikes.Remove(likedBlog);

        var result = _unitOfWork.BlogWriteRepository.Update(blog);
        await _unitOfWork.SaveAsync();

        return result;
    }
}
