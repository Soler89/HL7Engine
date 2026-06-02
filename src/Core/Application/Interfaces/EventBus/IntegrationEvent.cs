namespace Hl7Engine.Core.Application.Interfaces.EventBus;

public abstract class IntegrationEvent(string id)
{
        public string Id { get; set; } = id;

        public DateTime ReceivedAt { get; } = DateTime.Now;
}

