using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using SproutSocial.Application.Features.Commands.Comment.DeleteComment;
using SproutSocial.Application.Features.Commands.Comment.LikeComment;
using SproutSocial.Application.Features.Commands.Comment.PostComment;
using SproutSocial.Application.Features.Commands.Comment.UnLikeComment;
using SproutSocial.Application.Features.Commands.Comment.UpdateComment;
using SproutSocial.Application.Features.Queries.Comment.GetComments;

namespace SproutSocial.API.Controllers.v1;

[ApiVersion("1")]
[Authorize]
public class BlogCommentsController : BaseController
{
    private readonly IMediator _mediator;

    public BlogCommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("")]
    public async Task<IActionResult> PostComment(PostCommentCommandRequest postCommentCommandRequest)
    {
        var response = await _mediator.Send(postCommentCommandRequest);

        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("{blogId}")]
    public async Task<IActionResult> GetAllComments([FromRoute] string blogId, [FromQuery] int page)
    {
        return Ok(await _mediator.Send(new GetCommentsQueryRequest
        {
            BlogId = blogId,
            Page = page
        }));
    }

    [HttpPut("")]
    public async Task<IActionResult> UpdateComment(UpdateCommentCommandRequest updateCommentCommandRequest)
    {
        var response = await _mediator.Send(updateCommentCommandRequest);

        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteComment([FromRoute] DeleteCommentCommandRequest deleteCommentCommandRequest)
    {
        var response = await _mediator.Send(deleteCommentCommandRequest);

        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost("like-comment/{commentId}")]
    public async Task<IActionResult> LikeComment([FromRoute] LikeCommentCommandRequest likeCommentCommandRequest)
    {
        var response = await _mediator.Send(likeCommentCommandRequest);

        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("unlike-comment/{commentId}")]
    public async Task<IActionResult> UnLikeComment([FromRoute] UnLikeCommentCommandRequest unLikeCommentCommandRequest)
    {
        var response = await _mediator.Send(unLikeCommentCommandRequest);

        return StatusCode((int)response.StatusCode, response);
    }
}
