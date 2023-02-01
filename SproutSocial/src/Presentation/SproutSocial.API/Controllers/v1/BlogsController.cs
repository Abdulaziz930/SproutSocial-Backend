using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using SproutSocial.Application.Features.Commands.Blog.CreateBlog;
using SproutSocial.Application.Features.Commands.Blog.DeleteBlog;
using SproutSocial.Application.Features.Commands.Blog.LikeBlog;
using SproutSocial.Application.Features.Commands.Blog.UnLikeBlog;
using SproutSocial.Application.Features.Commands.Blog.UpdateBlog;
using SproutSocial.Application.Features.Queries.Blog.GetAllBlogs;
using SproutSocial.Application.Features.Queries.Blog.GetBlogById;

namespace SproutSocial.API.Controllers.v1;

[ApiVersion("1")]
[Authorize]
public class BlogsController : BaseController
{
    private readonly IMediator _mediator;

    public BlogsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllBlogs([FromQuery] GetAllBlogsQueryRequest getAllBlogsQueryRequest)
    {
        return Ok(await _mediator.Send(getAllBlogsQueryRequest));
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetBlogById([FromRoute] GetBlogByIdQueryRequest getBlogByIdQueryRequest)
    {
        return Ok(await _mediator.Send(getBlogByIdQueryRequest));
    }

    [HttpPost("")]
    public async Task<IActionResult> Create([FromForm] CreateBlogCommandRequest createBlogCommandRequest)
    {
        var response = await _mediator.Send(createBlogCommandRequest);

        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("")]
    public async Task<IActionResult> Update([FromForm] UpdateBlogCommandRequest updateBlogCommandRequest)
    {
        var response = await _mediator.Send(updateBlogCommandRequest);

        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteBlogCommandRequest deleteBlogCommandRequest)
    {
        var response = await _mediator.Send(deleteBlogCommandRequest);

        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost("like-blog/{id}")]
    public async Task<IActionResult> LikeBlog([FromRoute] LikeBlogCommandRequest likeBlogCommandRequest)
    {
        var response = await _mediator.Send(likeBlogCommandRequest);

        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("unlike-blog/{id}")]
    public async Task<IActionResult> UnLikeBlog([FromRoute] UnLikeBlogCommandRequest unLikeBlogCommandRequest)
    {
        var response = await _mediator.Send(unLikeBlogCommandRequest);

        return StatusCode((int)response.StatusCode, response);
    }
}
