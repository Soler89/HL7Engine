using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Hl7Engine.AppHost.Bootstrappers;

public static class LoggingBootstrapper
{
    public static IServiceCollection LoggerConfigExtensions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        services.AddSerilog();
       return services;
        
    }

  
}