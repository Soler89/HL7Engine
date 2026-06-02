using Hl7Engine.Core.Application.Interfaces.EventBus;
using Hl7Engine.Core.Application.Message.Dto;


namespace HL7Engine.Module.Parser.IntegrationEvents.Event;

public class ParserSuccessIntegrationEvent(string id , MessageDto messageDto) : IntegrationEvent(id) {
   // public IParsedMessage Message { get; init; } = message;
    public MessageDto MessageDto { get; init; } = messageDto;
}