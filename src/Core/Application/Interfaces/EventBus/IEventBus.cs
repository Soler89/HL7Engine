namespace Hl7Engine.Core.Application.Interfaces.EventBus;

public interface IEventBus
{
    Task Publish<T>(T @event)
        where T : IntegrationEvent;

    void Subscribe<T>(IIntegrationEventHandler<T> handler)  where T : IntegrationEvent;
}