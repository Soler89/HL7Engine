using Hl7Engine.Core.Application.Interfaces.EventBus;
using System.Collections.Concurrent;
using System.Threading.Channels;

public sealed class ChannelEventBus : IEventBus, IDisposable
{
    private readonly ConcurrentDictionary<string, ConcurrentBag<IIntegrationEventHandler>> _handlers = new();
    private readonly Channel<IntegrationEvent> _channel;
    private readonly CancellationTokenSource _cts;
    private readonly Task _worker;

    public ChannelEventBus()
    {
        // Canal con capacidad 1000, comportamiento "esperar" cuando está lleno
        var options = new BoundedChannelOptions(1000)
        {
            FullMode = BoundedChannelFullMode.Wait
        };
        _channel = Channel.CreateBounded<IntegrationEvent>(options);
        _cts = new CancellationTokenSource();
        _worker = Task.Run(ProcessEventsAsync);
    }

    public void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent
    {
        var eventType = typeof(T).FullName!;
        var bag = _handlers.GetOrAdd(eventType, _ => new ConcurrentBag<IIntegrationEventHandler>());
        bag.Add(handler);
    }

    public async Task Publish<T>(T @event) where T : IntegrationEvent
    {
        // Solo escribe en el canal - retorna en microsegundos
        await _channel.Writer.WriteAsync(@event);
    }

    private async Task ProcessEventsAsync()
    {
        var reader = _channel.Reader;
        while (await reader.WaitToReadAsync(_cts.Token))
        {
            while (reader.TryRead(out var @event))
            {
                await DispatchAsync(@event);
            }
        }
    }

    private async Task DispatchAsync(IntegrationEvent @event)
    {
        var eventType = @event.GetType().FullName;
        if (eventType == null) return;
        if (!_handlers.TryGetValue(eventType, out var handlers)) return;

        // Ejecuta todos los handlers en paralelo (Task.WhenAll)
        var tasks = handlers
            .OfType<dynamic>()  // O usa reflexión/patrón visitor
            .Select(h => ((dynamic)h).Handle((dynamic)@event))
            .Cast<Task>();

        await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    public void Dispose()
    {
        _cts.Cancel();
        _worker.Wait();
        _cts.Dispose();
    }
}
