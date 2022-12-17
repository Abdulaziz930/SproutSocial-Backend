namespace SproutSocial.Application.Exceptions.Subscribes;

public sealed class SubscribedEmailNotFound : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }

    public SubscribedEmailNotFound()
    {
        ErrorMessage = "Subscribed email address not found!!";
    }

    public SubscribedEmailNotFound(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public SubscribedEmailNotFound(string message, Exception? innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}
