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
            { typeof(ArgumentException), HandleArgumentException},
            { typeof(ArgumentNullException), HandleArgumentNullException},
            { typeof(IServiceException), HandleServiceException},
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
        var interfaces = type.GetInterfaces();
        if (interfaces != null && interfaces.Length > 0 && interfaces.Contains(typeof(IServiceException)))
        {
            _exceptionHandlers[typeof(IServiceException)].Invoke(context);
            return;
        }

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

    private void HandleArgumentException(ExceptionContext context)
    {
        var exception = (ArgumentException)context.Exception;

        var details = new ProblemDetails()
        {
            Title = "Wrong argument problem",
            Detail = exception.Message
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleArgumentNullException(ExceptionContext context)
    {
        var exception = (ArgumentNullException)context.Exception;

        var details = new ProblemDetails()
        {
            Title = "Argument cannot be null",
            Detail = exception.Message
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleServiceException(ExceptionContext context)
    {
        var exception = (IServiceException)context.Exception;

        var details = new ProblemDetails()
        {
            Title = exception.ErrorMessage,
            Detail = exception.ErrorDetail,
            Status = (int)exception.StatusCode
        };

        context.HttpContext.Response.StatusCode = (int)exception.StatusCode;
        context.Result = new ObjectResult(details);

        context.ExceptionHandled = true;
    }
}