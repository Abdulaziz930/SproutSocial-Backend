namespace SproutSocial.Application.Exceptions.Common;

public sealed class PageFormatException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }

    public PageFormatException()
    {
        ErrorMessage = "Page must be greater or equal than 1";
    }

    public PageFormatException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public PageFormatException(string message, Exception? innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}
