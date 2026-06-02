using Hl7Engine.Core.Application.Interfaces.EventBus;
using Hl7Engine.Core.Application.Interfaces.Module;
using HL7Engine.Module.Parser.IntegrationEvents.Event;
using Microsoft.Extensions.Logging;

namespace Hl7Cloud.Module.Hl7Validate.Application.Handlers;

public class ParserSuccessIntegrationEventHandler(
  ILogger<ParserSuccessIntegrationEvent> logger,
  IEventBus eventBus,
  IValidateMessage validateMessage) : IIntegrationEventHandler<ParserSuccessIntegrationEvent>
{



  public async Task Handle(ParserSuccessIntegrationEvent @event)
  {


    var validateResult = await validateMessage.ValidateAsync(@event.MessageDto);
    if (!validateResult.IsValid)
    {
      foreach (var validateResultError in validateResult.Errors)
      {
         Console.WriteLine(validateResultError);
      }




    }

  }
}
