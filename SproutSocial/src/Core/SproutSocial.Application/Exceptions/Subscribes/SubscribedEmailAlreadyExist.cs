namespace SproutSocial.Application.Exceptions.Subscribes;

public sealed class SubscribedEmailAlreadyExist : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage { get; }

    public SubscribedEmailAlreadyExist()
    {
        ErrorMessage = "This email address already subscribed";
    }

    public SubscribedEmailAlreadyExist(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public SubscribedEmailAlreadyExist(string message, Exception? innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}
