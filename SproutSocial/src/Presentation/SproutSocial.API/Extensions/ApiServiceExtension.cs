using FluentValidation.AspNetCore;
using SproutSocial.Application.Features.Commands.Product.CreateTopic;

namespace SproutSocial.API.Extensions;

public static class ApiServiceExtension
{
    public static IServiceCollection AddApiService(this IServiceCollection services)
    {
        services.AddControllers().AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateTopicCommandValidator>())
            .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
        return services;
    }
}
