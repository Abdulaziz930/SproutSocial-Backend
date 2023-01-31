using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SproutSocial.Application.Helpers.Extesions;

public static class AutoMapperExtension
{
    public static void AddAutoMapperProfiles<T>(this IServiceCollection services) where T : Profile
    {
        var assembly = typeof(T).Assembly;

        var mappingTypes = assembly.GetTypes().Where(t => t.IsClass && t.IsSubclassOf(typeof(Profile)));

        services.AddSingleton(provider => new MapperConfiguration(mapperConfig =>
        {
            foreach (var mappingType in mappingTypes)
            {
                var parameters = mappingType.GetConstructors()[0].GetParameters();
                if (parameters != null && parameters.Count() > 0)
                {
                    foreach (var parameter in parameters)
                    {
                        mapperConfig.AddProfile(Activator.CreateInstance(mappingType, args: provider.GetService(parameter.ParameterType)) as Profile);
                    }
                }
                else
                {
                    mapperConfig.AddProfile(mappingType);
                }
            }
        }).CreateMapper());
    }
}