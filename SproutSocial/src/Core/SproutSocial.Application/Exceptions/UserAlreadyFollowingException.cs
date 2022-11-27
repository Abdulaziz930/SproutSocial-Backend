namespace SproutSocial.Application.Exceptions;

public sealed class UserAlreadyFollowingException : Exception
{
    public UserAlreadyFollowingException() : base("User already following")
    {
    }

    public UserAlreadyFollowingException(string? message) : base(message)
    {
    }

    public UserAlreadyFollowingException(string followingName, string followedName) 
        : base($"{followingName} already following {followedName}")
    {
    }

    public UserAlreadyFollowingException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
