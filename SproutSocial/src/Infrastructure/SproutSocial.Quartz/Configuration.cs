using Microsoft.Extensions.Configuration;

namespace SproutSocial.Quartz;

internal static class Configuration
{
    internal static string GetCronExpression(string jobType)
    {
        ConfigurationManager configurationManager = new();
        configurationManager.SetBasePath(Directory.GetCurrentDirectory());
        configurationManager.AddJsonFile("appsettings.json");

        return configurationManager[$"Quartz:{jobType}"];
    }
}