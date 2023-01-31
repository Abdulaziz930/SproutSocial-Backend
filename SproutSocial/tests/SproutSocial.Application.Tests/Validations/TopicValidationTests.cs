using FluentValidation.TestHelper;
using SproutSocial.Application.Features.Commands.Topic.CreateTopic;
using SproutSocial.Application.Features.Commands.Topic.DeleteTopic;
using SproutSocial.Application.Features.Commands.Topic.UpdateTopic;
using SproutSocial.Application.Tests.Validations.Base;
using Xunit;

namespace SproutSocial.Application.Tests.Validations;

public class TopicValidationTests : CoreData
{
    [Theory, MemberData(nameof(String101))]
    public void TopucCreateValidation_NameNullEmptyMin255_ReturnError(string topicName)
    {
        //Arrange
        var validation = new CreateTopicCommandValidator();
        var model = new CreateTopicCommandRequest
        {
            Name = topicName
        };

        //Act
        var result = validation.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Theory, MemberData(nameof(Id))]
    public void TopicDeleteValidation_IdNullEmptyNotGuid_ReturnError(string id)
    {
        //Arrange
        var validation = new DeleteTopicCommandValidator();
        var model = new DeleteTopicCommandRequest
        {
            Id = id
        };

        //Act
        var result = validation.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Theory, MemberData(nameof(String101))]
    public void TopicUpdateValidation_NameNullEmptyMin100_ReturnError(string topicName)
    {
        //Arrange
        var validation = new UpdateTopicCommandValidator();
        var model = new UpdateTopicCommandRequest
        {
            Name = topicName
        };

        //Act
        var result = validation.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Theory, MemberData(nameof(Id))]
    public void TopicUpdateValidation_IdNullEmptyNotGuid_ReturnError(string id)
    {
        //Arrange
        var validation = new UpdateTopicCommandValidator();
        var model = new UpdateTopicCommandRequest
        {
            Id = id
        };

        //Act
        var result = validation.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}