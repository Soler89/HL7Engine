
using Hl7Engine.Module.MllpServer.Infrastructure.Extensions;
using  Hl7Engine.Module.Parser.Infrastructure.Extensions;
using HL7Engine.Module.Tracking.Infrastructure;
using Hl7Engine.Module.Validate.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hl7Engine.AppHost.Bootstrappers;

public static class ModuleBootstrapper
{
    public static IServiceCollection ModulesExtensions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        
        services.AddMllpServer(configuration)
        .AddParser(configuration)
        .AddValidate(configuration)
        .AddTrackingModule(configuration);
        
        return services;
    }
}