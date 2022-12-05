using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

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

            var details = Map(errors, context.ModelState.Keys);

            var responseObj = new
            {
                Title = "One or more validation errors occurred.",
                Detail = details,
                Status = (int)HttpStatusCode.BadRequest
            };

            context.Result = new BadRequestObjectResult(responseObj);
        }
    }

    private Dictionary<string, List<string>> Map(IEnumerable<string> errors, KeyEnumerable keys)
    {
        Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
        List<string> errorsList = new List<string>();
        for (int i = 0; i < keys.Count(); i++)
        {
            if (result.ContainsKey(keys.ElementAt(i)))
            {
                errorsList.Add(errors.ElementAt(i));
                result[keys.ElementAt(i)] = errorsList;
            }
            else
            {
                result.Add(keys.ElementAt(i), new List<string>() { errors.ElementAt(i) });
            }
        }

        return result;
    }
}