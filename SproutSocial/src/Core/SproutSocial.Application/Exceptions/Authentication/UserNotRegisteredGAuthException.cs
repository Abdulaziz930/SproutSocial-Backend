namespace SproutSocial.Application.Exceptions.Authentication;

public sealed class UserNotRegisteredGAuthException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }

    public UserNotRegisteredGAuthException()
    {
        ErrorMessage = "This user is not registered in the Google authenticator app";
    }

    public UserNotRegisteredGAuthException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public UserNotRegisteredGAuthException(string message, Exception? innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}
