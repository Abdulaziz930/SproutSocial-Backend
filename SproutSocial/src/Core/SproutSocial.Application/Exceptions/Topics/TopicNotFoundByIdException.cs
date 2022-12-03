namespace SproutSocial.Application.Exceptions.Topics;

public sealed class TopicNotFoundByIdException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }

    public TopicNotFoundByIdException(Guid id)
    {
        ErrorMessage = $"Topic not found with id: {id}";
    }

    public TopicNotFoundByIdException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public TopicNotFoundByIdException(string message, Exception? innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}
