using Hl7Engine.Core.Application.Interfaces.EventBus;
using Microsoft.Extensions.Logging;

namespace Hl7Engine.Core.Infrastructure.EventBus
{
    public class InMemoryEventBusClient : IEventBus
    {
        private readonly ILogger<InMemoryEventBusClient> _logger;

        public InMemoryEventBusClient(ILogger<InMemoryEventBusClient> logger)
        {
            _logger = logger;
        }

        public async Task Publish<T>(T @event)
            where T : IntegrationEvent
        {
            _logger.LogInformation("Publishing {Event}", @event.GetType().FullName);
            await InMemoryEventBus.Instance.Publish(@event);
        }

        public void Subscribe<T>(IIntegrationEventHandler<T> handler)
            where T : IntegrationEvent
        {
            InMemoryEventBus.Instance.Subscribe(handler);
        }

        public void StartConsuming()
        {
        }
    }
}