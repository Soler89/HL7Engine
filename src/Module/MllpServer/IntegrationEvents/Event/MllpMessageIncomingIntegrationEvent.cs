
using Hl7Engine.Core.Application.Interfaces;
using Hl7Engine.Core.Application.Interfaces.EventBus;

namespace Hl7Engine.Module.MllpServer.IntegrationEvents.Event;

public class MllpMessageIncomingIntegrationEvent(string id,byte[] rawHl7) : IntegrationEvent(id)
{
  
    public byte [] RawHl7 { get; init; } = rawHl7;
}