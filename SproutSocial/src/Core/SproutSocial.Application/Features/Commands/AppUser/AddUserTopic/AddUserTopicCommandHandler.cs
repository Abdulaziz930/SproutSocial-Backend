namespace SproutSocial.Application.Features.Commands.AppUser.AddUserTopic;

public class AddUserTopicCommandHandler : IRequestHandler<AddUserTopicCommandRequest, AddUserTopicCommandResponse>
{
    private readonly IUserService _userService;

    public AddUserTopicCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<AddUserTopicCommandResponse> Handle(AddUserTopicCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.AddUserTopicsAsync(request.TopicIds);

        return new()
        {
            StatusCode = result.IsSuccess ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
            Message = result.IsSuccess ? result.Message : "Something went wrong"
        };
    }
}