using Microsoft.OpenApi.Models;
using SproutSocial.Infrastructure.Filters;

namespace SproutSocial.API.Extensions.ServiceExtensions;

public static class SwaggerServiceExtension
{
    public static IServiceCollection AddSwaggerService(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.ResolveConflictingActions(apiDesc => apiDesc.First());
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "1",
                Title = "SproutSocial",
                Description = "SproutSocial api service documentations",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Example Contact",
                    Url = new Uri("https://example.com/contact"),
                    Email = "karimliabdulaziz5@gmail.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://example.com/license")
                }
            });

            //options.SwaggerDoc("v2", new OpenApiInfo
            //{
            //    Version = "2",
            //    Title = "SproutSocial api service",
            //    Description = "SproutSocial api service documentations",
            //    TermsOfService = new Uri("https://example.com/terms"),
            //    Contact = new OpenApiContact
            //    {
            //        Name = "Example Contact",
            //        Url = new Uri("https://example.com/contact"),
            //        Email = "karimliabdulaziz5@gmail.com"
            //    },
            //    License = new OpenApiLicense
            //    {
            //        Name = "Example License",
            //        Url = new Uri("https://example.com/license")
            //    }
            //});

            options.OperationFilter<RemoveVersionFromParameter>();
            options.DocumentFilter<ReplaceVersionWithExactValueInPath>();
        });
        return services;
    }
}
