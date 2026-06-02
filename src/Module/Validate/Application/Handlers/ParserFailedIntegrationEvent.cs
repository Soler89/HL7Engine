using Hl7Engine.Core.Application.Interfaces.EventBus;
using Hl7Engine.Core.Application.Interfaces.Module;
using Hl7Engine.Core.Application.Message.Dto;

namespace Hl7Cloud.Module.Hl7Validate.Application.Handlers;

public class ParserFailedIntegrationEvent(string id , MessageDto message) : IntegrationEvent(id)
{
    public MessageDto Message { get; init; } = message;
}