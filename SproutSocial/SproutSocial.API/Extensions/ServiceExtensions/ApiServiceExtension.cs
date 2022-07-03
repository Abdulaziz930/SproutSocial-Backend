using FluentValidation.AspNetCore;
using SproutSocial.Service.Dtos.TopicDtos;

namespace SproutSocial.API.Extensions.ServiceExtensions
{
    public static class ApiServiceExtension
    {
        public static void AddApiService(this IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<TopicPostDtoValidator>());
        }
    }
}
