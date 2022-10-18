using FluentValidation.AspNetCore;
using SproutSocial.Application.Features.Commands.Topic.CreateTopic;

namespace SproutSocial.API.Extensions.ServiceExtensions;

public static class ApiServiceExtension
{
    public static IServiceCollection AddApiService(this IServiceCollection services)
    {
        services.AddControllers().AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateTopicCommandValidator>());
        return services;
    }
}
