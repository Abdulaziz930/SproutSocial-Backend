using AutoMapper;
using SproutSocial.Service.HelperServices.Interfaces;
using SproutSocial.Service.Profiles;

namespace SproutSocial.API.Extensions.ServiceExtensions
{
    public static class MapperServiceExtension
    {
        public static void AddMapperService(this IServiceCollection services)
        {
            services.AddSingleton(provider => new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile(provider.GetService<IHelperAccessor>()));
            }).CreateMapper());
        }
    }
}
