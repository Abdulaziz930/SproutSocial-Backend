namespace SproutSocial.Application.Exceptions;

public sealed class UserFollowException : Exception
{
    public UserFollowException(string? message) : base(message)
    {
    }

    public UserFollowException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
