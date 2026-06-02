using Hl7Engine.Core.Application.Interfaces.EventBus;
using Hl7Engine.Core.Application.Message.Tracking;


namespace Hl7Cloud.Module.Tracking.IntegrationEvents.Event;

public class MessageStatusChangedIntegrationEvent(string id,MessageTrackingUpdateDto messageTrackingDto ) : IntegrationEvent(id)
{

    public MessageTrackingUpdateDto MessageTracking { get; init; } = messageTrackingDto;
}