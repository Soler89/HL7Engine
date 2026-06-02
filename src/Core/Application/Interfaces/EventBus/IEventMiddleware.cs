namespace Hl7Engine.Core.Application.Interfaces.EventBus;
public interface IEventMiddleware
{
    Task InvokeAsync<TEvent>(
        TEvent @event,
        Func<Task> next);
}