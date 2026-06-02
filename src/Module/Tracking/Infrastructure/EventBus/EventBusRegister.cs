using Hl7Cloud.Module.Tracking.IntegrationEvents.Event;
using Hl7Engine.Core.Application.Interfaces.EventBus;
using HL7Engine.Module.Parser.IntegrationEvents.Event;
using Microsoft.Extensions.Hosting;

namespace HL7Engine.Module.Tracking.Infrastructure.EventBus;

public class EventBusRegister(IEventBus eventBus,IIntegrationEventHandler<MessageStatusChangedIntegrationEvent> eventHandler):IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
   {
      
        eventBus.Subscribe(eventHandler);
        return Task.CompletedTask;
   }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
    
}