using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SproutSocial.Application.Behaviours;

public class ValidationBehaviour : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                .SelectMany(v => v.Errors)
                .Select(v => v.ErrorMessage);

            var responseObj = new
            {
                Title = "One or more validation errors occurred.",
                Detail = errors,
                Status = (int)HttpStatusCode.BadRequest
            };

            context.Result = new BadRequestObjectResult(responseObj);
        }
    }
}
