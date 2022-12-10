using Microsoft.Extensions.Configuration;

namespace SproutSocial.Quartz;

internal static class Configuration
{
    public static string BaseUrl { get => "http://localhost:1901/api/v1"; }

    internal static string GetCronExpression(string jobType)
    {
        ConfigurationManager configurationManager = new();
        configurationManager.SetBasePath(Directory.GetCurrentDirectory());
        configurationManager.AddJsonFile("appsettings.json");

        return configurationManager[$"Quartz:{jobType}"];
    }
}