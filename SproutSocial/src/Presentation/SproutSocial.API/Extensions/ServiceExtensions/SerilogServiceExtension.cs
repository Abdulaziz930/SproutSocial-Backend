using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace SproutSocial.API.Extensions.ServiceExtensions;

public static class SerilogServiceExtension
{
    public static IHostBuilder AddSerilogService(this IHostBuilder builder, string connectionString)
    {
        Logger logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.MSSqlServer(
                connectionString: connectionString,
                sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true }
            )
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .CreateLogger();

        builder.UseSerilog(logger);
        return builder;
    }
}