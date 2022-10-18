using Microsoft.AspNetCore.Mvc.Filters;
using SproutSocial.Application.Exceptions;

namespace SproutSocial.API.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(RecordAlreadyExistException), HandleRecordAlreadyExistException},
            { typeof(AuthenticationFailException), HandleAuthenticationFailException},
            { typeof(UserCreateFailedException), HandleUserCreateFailedException},
            { typeof(UserNotFoundException), HandleUserNotFoundException}
        };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        var details = new ProblemDetails()
        {
            Title = "Internal Server Error",
            Detail = "Unexpected error occurred",
            Status = StatusCodes.Status500InternalServerError
        };

        context.Result = new JsonResult(details);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = (NotFoundException)context.Exception;

        var details = new ProblemDetails()
        {
            Title = "The specified resource was not found.",
            Detail = exception.Message
        };

        context.Result = new NotFoundObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleRecordAlreadyExistException(ExceptionContext context)
    {
        var exception = (RecordAlreadyExistException)context.Exception;

        var details = new ProblemDetails()
        {
            Title = "The specified resource already exist.",
            Detail = exception.Message
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }
    
    private void HandleAuthenticationFailException(ExceptionContext context)
    {
        var exception = (AuthenticationFailException)context.Exception;

        var details = new ProblemDetails()
        {
            Title = "Authentication fail",
            Detail = exception.Message
        };

        context.Result = new UnauthorizedObjectResult(details);

        context.ExceptionHandled = true;
    }
    
    private void HandleUserCreateFailedException(ExceptionContext context)
    {
        var exception = (UserCreateFailedException)context.Exception;

        var details = new ProblemDetails()
        {
            Title = "User creation is failed",
            Detail = exception.Message
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleUserNotFoundException(ExceptionContext context)
    {
        var exception = (UserNotFoundException)context.Exception;

        var details = new ProblemDetails()
        {
            Title = "User not found",
            Detail = exception.Message
        };

        context.Result = new UnauthorizedObjectResult(details);

        context.ExceptionHandled = true;
    }
}
