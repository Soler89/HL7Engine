using Hl7Cloud.Module.Tracking.IntegrationEvents.Event;
using Hl7Engine.Core.Application.Interfaces.EventBus;
using Hl7Engine.Core.Application.Interfaces.Module;
using Hl7Engine.Core.Application.Message.Dto;
using Hl7Engine.Core.Application.Message.Tracking;
using HL7Engine.Module.Parser.IntegrationEvents.Event;
using Microsoft.Extensions.Logging;

namespace Hl7Cloud.Module.Hl7Validate.Application.Handlers;

public class ParserSuccessIntegrationEventHandler(
  ILogger<ParserSuccessIntegrationEvent> logger,
  IEventBus eventBus,
  IEnumerable<IValidateMessage<MessageDto>> validateMessage) : IIntegrationEventHandler<ParserSuccessIntegrationEvent>
{



  public async Task Handle(ParserSuccessIntegrationEvent @event)
  {

        var validateResult = await Validate(@event.MessageDto);

        var status = validateResult.IsValid ? MessageTrackingStatus.ValidationSuccess : MessageTrackingStatus.ValidationFailed;

        await eventBus.Publish(new MessageStatusChangedIntegrationEvent(@event.Id,
          new MessageTrackingUpdateDto() { Status = status,Errors = validateResult.Errors }));

        if (validateResult.IsValid)
        { 
        
        
        }

       


        if (!validateResult.IsValid)
    {
      foreach (var validateResultError in validateResult.Errors)
      {
         Console.WriteLine(validateResultError);
      }




    }

  }

    public async Task<ValidationResult> Validate(MessageDto message)
    {
        var taskResult = await Task.WhenAll(validateMessage.Select(handler => handler.ValidateAsync(message)));
                
        bool isValid = taskResult.All(m=>m.IsValid == true);
        List<string>  allErrors = taskResult.Where(v=>v.IsValid==false).SelectMany(v=>v.Errors).ToList();


        
        /*
        foreach (var item in validateMessage)
        {
            ValidationResult result = await item.ValidateAsync(message);
            isValid = isValid && result.IsValid;
            if (!result.IsValid)
                allErrors.AddRange(result.Errors);
        }
        */
        
        ValidationResult validateResult = new ValidationResult(isValid, allErrors);

        return validateResult;


    }
}
