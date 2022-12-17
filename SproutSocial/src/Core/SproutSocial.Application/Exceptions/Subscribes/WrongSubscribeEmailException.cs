namespace SproutSocial.Application.Exceptions.Subscribes;

public sealed class WrongSubscribeEmailException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }

    public WrongSubscribeEmailException()
    {
        ErrorMessage = "Please enter your email address!!";
    }

    public WrongSubscribeEmailException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public WrongSubscribeEmailException(string message, Exception? innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}
