using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SproutSocial.Infrastructure.Filters;

public class RemoveVersionFromParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters.Count() > 0)
        {
            var versionParameter = operation.Parameters.SingleOrDefault(s => s.Name == "ver");
            operation.Parameters.Remove(versionParameter);
        }
    }
}
