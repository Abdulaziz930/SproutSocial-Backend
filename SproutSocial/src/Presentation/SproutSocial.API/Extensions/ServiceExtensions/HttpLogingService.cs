using Microsoft.AspNetCore.HttpLogging;

namespace SproutSocial.API.Extensions.ServiceExtensions;

public static class HttpLogingService
{
    public static IServiceCollection AddHttpLogingService(this IServiceCollection services)
    {
        services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All;
            logging.RequestHeaders.Add("sec-ch-ua");
            logging.MediaTypeOptions.AddText("application/javascript");
            logging.RequestBodyLogLimit = 4096;
            logging.ResponseBodyLogLimit = 4096;
        });
        return services;
    }
}