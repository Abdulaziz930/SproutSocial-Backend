namespace SproutSocial.Application.Exceptions.Users;

public sealed class UserFollowException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }

    public UserFollowException()
    {
        ErrorMessage = "User cannot follow himself/herself";
    }

    public UserFollowException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public UserFollowException(string message, Exception? innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}
