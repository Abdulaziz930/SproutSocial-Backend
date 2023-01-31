using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SproutSocial.API.Controllers;
using SproutSocial.Application.Features.Commands.Topic.CreateTopic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SproutSocial.API.Tests.Controllers;

public class TopicControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly TopicsController _topicsController;

    public TopicControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _topicsController = new TopicsController(_mediatorMock.Object);
    }

    [Fact]
    public async Task CreateTopic_ReturnCreated()
    {
        CreateTopicCommandResponse createTopicResponse = new()
        {
            StatusCode = HttpStatusCode.Created,
            Message = "Topic successfully created"
        };

        _mediatorMock.Setup(x => x.Send(It.IsAny<CreateTopicCommandRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createTopicResponse);

        CreateTopicCommandRequest topic = new()
        {
            Name = "Topic-1"
        };

        var response = await _topicsController.Create(topic);
        var result = response as ObjectResult;

        Assert.IsType<ObjectResult>(result);
        Assert.Equal(result?.StatusCode, (int)HttpStatusCode.Created);
    }
}