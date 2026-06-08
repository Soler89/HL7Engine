using Hl7Engine.Core.Application.Interfaces.EventBus;
using Hl7Engine.Core.Application.Interfaces.SocketStore;
using Hl7Engine.Core.Infrastructure.EventBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hl7Engine.Core.Infrastructure.Extensions;

public static class CoreConfigurateExtensions
{

    public static IServiceCollection AddCoreExtensions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        
      //  services.AddSingleton<ISocketStore, SocketStore.SocketStore>();
        services.AddSingleton<IEventBus, ChannelEventBus>();
        return services;
    }
}
