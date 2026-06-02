using Hl7Engine.Core.Application.Interfaces.EventBus;
using Hl7Engine.Module.MllpServer.IntegrationEvents.Event;
using HL7Engine.Module.Parser.Application.Handlers;
using Microsoft.Extensions.Hosting;

namespace Hl7Engine.Module.Parser.Infrastructure.EventBus;

public class EventBusRegister(IEventBus eventBus,IIntegrationEventHandler<MllpMessageIncomingIntegrationEvent> eventHandler):IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
   {
      
        eventBus.Subscribe(eventHandler);
        return Task.CompletedTask;
   }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
    
}