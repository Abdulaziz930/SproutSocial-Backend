namespace SproutSocial.Application.Exceptions.Topics;

public sealed class TopicNotFoundException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage { get; }

    public TopicNotFoundException()
    {
        ErrorMessage = "There is no any topic items";
    }

    public TopicNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public TopicNotFoundException(string message, Exception? innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}
