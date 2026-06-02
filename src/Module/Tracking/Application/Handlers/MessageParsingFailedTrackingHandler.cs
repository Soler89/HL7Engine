/*using System.Text.Json;
using MediatR;
using ParserModule.Integration;
using Shared.Infrastructure.EventBus;
using TrackingModule.Core;
using TrackingModule.Core.Entities;
using TrackingModule.Core.Repositories;

namespace HL7Engine.Module.Tracking.Application.Handlers;

public sealed class MessageParsingFailedTrackingHandler(IMessageTrackingRepository repository)
    : INotificationHandler<IntegrationEventNotification<MessageParsingFailedEvent>>
{
    public async Task Handle(IntegrationEventNotification<MessageParsingFailedEvent> notification, CancellationToken cancellationToken)
    {
        var evt = notification.IntegrationEvent;
        var existing = await repository.GetByIdAsync(evt.CorrelationId, cancellationToken);
        var now = DateTimeOffset.UtcNow;

        await repository.InsertOrUpdateAsync(new MessageTracking
        {
            Id = evt.CorrelationId,
            SessionId = existing?.SessionId ?? Guid.Empty,
            Status = MessageTrackingStatus.ParsingFailed,
            RawBytes = evt.RawBytes,
            ErrorsJson = JsonSerializer.Serialize(evt.Errors),
            CreatedAt = existing?.CreatedAt ?? now,
            LastUpdated = now
        }, cancellationToken);
    }
}
*/