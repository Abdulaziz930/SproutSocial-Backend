using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SproutSocial.Application.Helpers.Extesions;

public static class ServiceExtension
{
    /// <summary>
    /// Register all business services
    /// </summary>
    /// <typeparam name="T">Service type (gets the assembly of this type)</typeparam>
    /// <typeparam name="K">Implementation (gets the assembly of this type)</typeparam>
    /// <param name="services"></param>
    public static void RegisterServices<T, K>(this IServiceCollection services) where K : class, T
    {
        var assemblyService = Assembly.GetAssembly(typeof(T));
        var assemblyImplementation = Assembly.GetAssembly(typeof(K));

        var serviceTypes = assemblyService.GetTypes().Where(t => t.IsInterface && t.IsAbstract && t.Namespace.Contains("Services"))
            .Select(t => new
            {
                Interface = t,
                Implementation = assemblyImplementation.GetTypes().FirstOrDefault(x => t.IsAssignableFrom(x))
            }).ToList();

        foreach (var type in serviceTypes)
        {
            services.AddScoped(type.Interface, type.Implementation);
        }
    }
}
