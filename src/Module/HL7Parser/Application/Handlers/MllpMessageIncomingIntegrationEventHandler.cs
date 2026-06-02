using Hl7Cloud.Core.Domain.Constants;
using Hl7Cloud.Module.Tracking.IntegrationEvents.Event;
using Hl7Engine.Core.Application.Interfaces.EventBus;
using Hl7Engine.Core.Application.Interfaces.Module;
using Hl7Engine.Core.Application.Message.Dto;
using Hl7Engine.Core.Application.Message.Tracking;
using Hl7Engine.Module.MllpServer.IntegrationEvents.Event;
using HL7Engine.Module.Parser.Application.Interfaces;
using HL7Engine.Module.Parser.IntegrationEvents.Event;
using Microsoft.Extensions.Logging;
namespace HL7Engine.Module.Parser.Application.Handlers;

public class MllpMessageIncomingIntegrationEventHandler(
  ILogger<MllpMessageIncomingIntegrationEventHandler> logger,
  IMessageParser<Hl7MessageDto> parserService,
  IEventBus eventBus) : IIntegrationEventHandler<MllpMessageIncomingIntegrationEvent>
{



  public async Task Handle(MllpMessageIncomingIntegrationEvent @event)
  {
    logger.LogInformation("Received Integration Event: {IntegrationEventId}", @event.Id);

    var parsedMessage = parserService.Parser(@event.RawHl7);
     
    var messageId = (parsedMessage.Format == MessageFormat.HL7v2)
      ? parsedMessage.GetField(Hl7FieldConstants.MessageId)
      : string.Empty;
    var status = parsedMessage.IsValid ? MessageTrackingStatus.ParsedSuccess : MessageTrackingStatus.ParsingFailed;
  
    await eventBus.Publish(new MessageStatusChangedIntegrationEvent(@event.Id,
      new MessageTrackingUpdateDto() { ParsedString =parsedMessage.ParsedString,  RawBytes = @event.RawHl7 ,Status = status}));

    
    if (parsedMessage.IsValid == true)
    {
     
       
      logger.LogInformation("Message parsed successfully | MessageId={MessageId}, ConnectionId={ConnectionId}",
        messageId, @event.Id);
      await eventBus.Publish(new ParserSuccessIntegrationEvent(@event.Id, parsedMessage));
    }
    else
    {
      logger.LogWarning("Message parsed failed | MessageId={MessageId}, ConnectionId={ConnectionId}",
        messageId, @event.Id);
      await eventBus.Publish(new ParserSuccessIntegrationEvent(@event.Id,parsedMessage));

    }
    
   




  }

}