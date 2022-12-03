namespace SproutSocial.Application.Exceptions.Comments;

public sealed class CommentNotFoundByIdException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }

    public CommentNotFoundByIdException(Guid id)
    {
        ErrorMessage = $"Comment not found with id: {id}";
    }

    public CommentNotFoundByIdException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public CommentNotFoundByIdException(string message, Exception? innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}
