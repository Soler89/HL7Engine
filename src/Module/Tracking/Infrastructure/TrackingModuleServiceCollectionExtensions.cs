

using Hl7Cloud.Module.Tracking.IntegrationEvents.Event;
using Hl7Engine.Core.Application.Interfaces.EventBus;
using HL7Engine.Module.Tracking.Application.Handlers;
using HL7Engine.Module.Tracking.Domain.Repositories;
using HL7Engine.Module.Tracking.Infrastructure.Configuration;
using HL7Engine.Module.Tracking.Infrastructure.Persistence;
using HL7Engine.Module.Tracking.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
 

// Esta debe ser la primera línea de tu programa

namespace HL7Engine.Module.Tracking.Infrastructure;

public static class TrackingModuleServiceCollectionExtensions
{
    public static IServiceCollection AddTrackingModule(
        this IServiceCollection services,
        IConfiguration configuration)
    
    {
         
        services.AddOptions<ConnectionStringsOptions>()
            .Bind(configuration.GetSection(ConnectionStringsOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddDbContext<TrackingDbContext>((sp, options) =>
        {
            var connectionStrings = sp.GetRequiredService<IOptions<ConnectionStringsOptions>>().Value;
            options.UseSqlite(connectionStrings.Tracking);
        });
        

        services.AddScoped<IMessageTrackingRepository, MessageTrackingRepository>();
        services.AddScoped<IIntegrationEventHandler<MessageStatusChangedIntegrationEvent>,MessageStatusChangedIntegrationEventHandler>();
        return services;
    }

    public static async Task MigrateTrackingDatabaseAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<TrackingDbContext>();
        await db.Database.EnsureCreatedAsync(cancellationToken);
    }
}
