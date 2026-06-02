using Hl7Engine.Core.Application.Interfaces.EventBus;
using Microsoft.Extensions.Logging;

namespace Hl7Engine.Core.Infrastructure.EventBus.Middleware;
public class LoggingEventMiddleware(ILogger<LoggingEventMiddleware> logger) : IEventMiddleware
{
    public async Task InvokeAsync<TEvent>(
        TEvent @event,
        Func<Task> next)
    {
        logger.LogInformation("EVENT START {Event}", typeof(TEvent).Name);

        await next();

        logger.LogInformation("EVENT END {Event}", typeof(TEvent).Name);
    }
}