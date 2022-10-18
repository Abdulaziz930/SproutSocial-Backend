using FluentValidation.AspNetCore;
using SproutSocial.API.Filters;
using SproutSocial.Application.Features.Commands.Topic.CreateTopic;

namespace SproutSocial.API.Extensions.ServiceExtensions;

public static class ApiServiceExtension
{
    public static IServiceCollection AddApiService(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ApiExceptionFilterAttribute>();
        }).AddFluentValidation(
            configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateTopicCommandValidator>());
        return services;
    }
}
