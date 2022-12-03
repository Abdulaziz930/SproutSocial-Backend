namespace SproutSocial.Application.Exceptions.Topics;

public sealed class TopicAlreadyExistException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage { get; }

    public TopicAlreadyExistException()
    {
        ErrorMessage = "Topic name already exist";
    }

    public TopicAlreadyExistException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public TopicAlreadyExistException(string message, Exception? innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}
