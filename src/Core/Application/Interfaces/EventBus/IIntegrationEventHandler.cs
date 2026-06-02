namespace Hl7Engine.Core.Application.Interfaces.EventBus;

public interface IIntegrationEventHandler
{
}

public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
       where TIntegrationEvent : IntegrationEvent
{
    Task Handle(TIntegrationEvent @event);
}
