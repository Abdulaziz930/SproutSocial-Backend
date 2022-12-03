namespace SproutSocial.Application.Exceptions.Users;

public sealed class FollowerNotFoundException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }

    public FollowerNotFoundException()
    {
        ErrorMessage = "Follower not found!";
    }

    public FollowerNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public FollowerNotFoundException(string followingName, string followedName)
    {
        ErrorMessage = $"{followingName} is not following {followedName}";
    }

    public FollowerNotFoundException(string message, Exception? innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}
