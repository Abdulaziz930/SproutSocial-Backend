namespace SproutSocial.Application.Exceptions.Authentication;

public sealed class EmailNotConfirmedException : Exception, IServiceException
{
    public HttpStatusCode StatusCode { get; }

    public string ErrorMessage { get; }
    public string? ErrorDetail { get; }

    public EmailNotConfirmedException()
    {
        StatusCode = HttpStatusCode.Forbidden;
        ErrorMessage = "Your account not activated.";
        ErrorDetail = "Please activate your account from email sent link.";
    }

    public EmailNotConfirmedException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
        ErrorMessage = message;
    }

    public EmailNotConfirmedException(string message, HttpStatusCode statusCode, Exception? innerException) : base(message, innerException)
    {
        StatusCode = statusCode;
        ErrorMessage = message;
    }
}
