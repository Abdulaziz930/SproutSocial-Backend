namespace SproutSocial.Application.Exceptions.Authentication;

public sealed class AuthenticationFailException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

    public string ErrorMessage { get; }

    public AuthenticationFailException()
    {
        ErrorMessage = "Username/Email or password incorrect";
    }

    public AuthenticationFailException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public AuthenticationFailException(string message, Exception? innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}
