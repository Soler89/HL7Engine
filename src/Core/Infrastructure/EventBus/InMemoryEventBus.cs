using System.Collections.Concurrent;
using Hl7Engine.Core.Application.Interfaces.EventBus;

namespace Hl7Engine.Core.Infrastructure.EventBus
{
    public sealed class InMemoryEventBus : IEventBus
    {
        static InMemoryEventBus()
        {
        }

        public static InMemoryEventBus Instance { get; } = new();

        private readonly ConcurrentDictionary<string, ConcurrentBag<IIntegrationEventHandler>> _handlersDictionary = new();

        public void Subscribe<T>(IIntegrationEventHandler<T> handler)
            where T : IntegrationEvent
        {

            var eventType = typeof(T).FullName;
            if (eventType != null)
            {
                var bag = _handlersDictionary.GetOrAdd(eventType, _ => new ConcurrentBag<IIntegrationEventHandler>());
                if (handler is IIntegrationEventHandler baseHandler)
                {
                    bag.Add(baseHandler);
                }
            }
        }

        public async Task Publish<T>(T @event)
            where T : IntegrationEvent
        {
            var eventType = @event.GetType().FullName;

            if (eventType == null)
            {
                return;
            }

            if (!_handlersDictionary.TryGetValue(eventType, out var integrationEventHandlers))
            {
                return;
            }

            var tasks = integrationEventHandlers.OfType<IIntegrationEventHandler<T>>().Select(m => m.Handle(@event));
            await Task.WhenAll(tasks);
        }
    }
}