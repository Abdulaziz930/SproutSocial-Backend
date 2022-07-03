using System.Reflection;

namespace SproutSocial.API.Extensions.ServiceExtensions
{
    public static class RegisterServiceExtension
    {
        public static IServiceCollection AddService(this IServiceCollection services, ServiceLifetime serviceLifetime, Type param)
        {
            var assembly = Assembly.GetAssembly(param);

            var types = assembly!.GetTypes().Where(x => !x.Namespace!.Contains("HelperServices") && x.IsInterface && x.IsAbstract)
                .Select(t => new
                {
                    Service = t,
                    Implementation = assembly.GetTypes().FirstOrDefault(x => t.IsAssignableFrom(x) && !x.IsInterface)
                }).ToList();

            foreach (var type in types)
            {
                if (serviceLifetime == ServiceLifetime.Scoped)
                {
                    services.AddScoped(type.Service, type.Implementation);
                }
                else if (serviceLifetime == ServiceLifetime.Singleton)
                {
                    services.AddSingleton(type.Service, type.Implementation);
                }
                else
                {
                    services.AddTransient(type.Service, type.Implementation);
                }
            }

            return services;
        }
    }
}
