namespace SproutSocial.Application.Exceptions.Authentication;

public sealed class TwoFaMethodNotFoundException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }

    public TwoFaMethodNotFoundException()
    {
        ErrorMessage = "2FA method not found";
    }

    public TwoFaMethodNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public TwoFaMethodNotFoundException(string message, Exception? innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}