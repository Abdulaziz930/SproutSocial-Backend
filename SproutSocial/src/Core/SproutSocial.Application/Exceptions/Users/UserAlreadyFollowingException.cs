namespace SproutSocial.Application.Exceptions.Users;

public sealed class UserAlreadyFollowingException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage { get; }

    public UserAlreadyFollowingException()
    {
        ErrorMessage = "User already following";
    }

    public UserAlreadyFollowingException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public UserAlreadyFollowingException(string followingName, string followedName)
    {
        ErrorMessage = $"{followingName} already following {followedName}";
    }

    public UserAlreadyFollowingException(string message, Exception? innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}
