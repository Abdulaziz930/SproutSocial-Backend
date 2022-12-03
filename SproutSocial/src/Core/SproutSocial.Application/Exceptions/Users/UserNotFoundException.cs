namespace SproutSocial.Application.Exceptions.Users;

public sealed class UserNotFoundException : Exception, IServiceException
{
    public HttpStatusCode StatusCode { get; }

    public string ErrorMessage { get; }

    public UserNotFoundException(string key, string value)
    {
        StatusCode = HttpStatusCode.Unauthorized;
        ErrorMessage = $"User not found with {key}: {value}";
    }

    public UserNotFoundException(string message, HttpStatusCode? statusCode = null) : base(message)
    {
        StatusCode = statusCode ?? HttpStatusCode.Unauthorized;
        ErrorMessage = message;
    }

    public UserNotFoundException(string message, HttpStatusCode statusCode, Exception? innerException) : base(message, innerException)
    {
        StatusCode = statusCode;
        ErrorMessage = message;
    }
}
