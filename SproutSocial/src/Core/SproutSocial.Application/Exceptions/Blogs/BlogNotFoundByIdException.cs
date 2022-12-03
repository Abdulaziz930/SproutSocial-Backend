namespace SproutSocial.Application.Exceptions.Blogs;

public sealed class BlogNotFoundByIdException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }

    public BlogNotFoundByIdException(Guid id)
    {
        ErrorMessage = $"Blog not found with id: {id}";
    }

    public BlogNotFoundByIdException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public BlogNotFoundByIdException(string message, Exception? innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}