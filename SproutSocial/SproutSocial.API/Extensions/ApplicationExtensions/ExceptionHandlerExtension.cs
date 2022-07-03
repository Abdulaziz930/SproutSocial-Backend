using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using SproutSocial.Service.Dtos;
using SproutSocial.Service.Exceptions;

namespace SproutSocial.API.Extensions.ApplicationExtensions
{
    public static class ExceptionHandlerExtension
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    int statusCode = StatusCodes.Status500InternalServerError;
                    string message = "Internal server error. Please try again later!";

                    if (contextFeature != null)
                    {
                        message = contextFeature.Error.Message;

                        if (contextFeature.Error is ArgumentNullException || contextFeature.Error is NullReferenceException)
                            statusCode = StatusCodes.Status400BadRequest;
                        else if (contextFeature.Error is ItemNotFoundException)
                            statusCode = StatusCodes.Status404NotFound;
                        else if (contextFeature.Error is RecordAlreadyExistException)
                            statusCode = StatusCodes.Status409Conflict;
                        else if (contextFeature.Error is PageIndexFormatException)
                            statusCode = StatusCodes.Status400BadRequest;
                        else if (contextFeature.Error is FileFormatException)
                            statusCode = StatusCodes.Status415UnsupportedMediaType;
                        else if (contextFeature.Error is FileSizeException)
                            statusCode = StatusCodes.Status413RequestEntityTooLarge;
                        else if (contextFeature.Error is RegisterFailException)
                            statusCode = StatusCodes.Status422UnprocessableEntity;
                    }

                    context.Response.StatusCode = statusCode;

                    var errorJsonStr = JsonConvert.SerializeObject(
                        new ResponseDto { Status = statusCode, Message = message }
                    );
                    await context.Response.WriteAsync(errorJsonStr);
                    await context.Response.CompleteAsync();
                });
            });
        }
    }
}
