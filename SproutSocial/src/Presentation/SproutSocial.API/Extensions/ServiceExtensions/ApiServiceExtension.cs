using FluentValidation.AspNetCore;
using SproutSocial.API.Filters;
using SproutSocial.Application.Behaviours;
using SproutSocial.Application.Features.Commands.Topic.CreateTopic;

namespace SproutSocial.API.Extensions.ServiceExtensions;

public static class ApiServiceExtension
{
    public static IServiceCollection AddApiService(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ApiExceptionFilterAttribute>();
            options.Filters.Add<ValidationBehaviour>();
        }).AddFluentValidation(
            configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateTopicCommandValidator>());

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        return services;
    }
}
