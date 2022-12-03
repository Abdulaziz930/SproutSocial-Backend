namespace SproutSocial.Application.Exceptions.Blogs;

public sealed class BlogNotFoundException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage { get; }

    public BlogNotFoundException()
    {
        ErrorMessage = "There is no any blog items";
    }

    public BlogNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public BlogNotFoundException(string message, Exception? innerException) : base(message, innerException)
    {
        ErrorMessage = message;
    }
}