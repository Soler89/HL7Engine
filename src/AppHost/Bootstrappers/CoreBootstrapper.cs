using Hl7Engine.Core.Infrastructure.Extensions;
using Hl7Engine.Module.MllpServer.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hl7Engine.AppHost.Bootstrappers;

public static class CoreBootstrapper
{
    public static IServiceCollection CoreExtensions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCoreExtensions(configuration);
        return services;
    }
}