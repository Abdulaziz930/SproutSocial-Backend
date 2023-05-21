namespace SproutSocial.Application.Exceptions.Authentication;

public sealed class TwoFaNotEnabledException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }

    public TwoFaNotEnabledException()
    {
        ErrorMessage = "2FA not active in this account";
    }

    public TwoFaNotEnabledException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public TwoFaNotEnabledException(string message, Exception? innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}