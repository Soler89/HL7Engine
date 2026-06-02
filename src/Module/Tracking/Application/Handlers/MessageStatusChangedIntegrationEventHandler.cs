
   using System.Text.Json;
   using Hl7Cloud.Module.Tracking.IntegrationEvents.Event;
   using Hl7Engine.Core.Application.Interfaces.EventBus;
   using Hl7Engine.Module.MllpServer.IntegrationEvents.Event;
   using HL7Engine.Module.Tracking.Domain.Repositories;
   using Microsoft.Extensions.Logging;
   using TrackingModule.Core;
 

   namespace HL7Engine.Module.Tracking.Application.Handlers;
   
   public class MessageStatusChangedIntegrationEventHandler(
     ILogger<MessageStatusChangedIntegrationEventHandler> logger,IMessageTrackingRepository repository
     ,
     IEventBus eventBus) : IIntegrationEventHandler<MessageStatusChangedIntegrationEvent>
   {
   
   
   
     

     public async Task Handle(MessageStatusChangedIntegrationEvent @event)
     {
       logger.LogInformation("Received Integration Event: {IntegrationEventId}", @event.Id);
      
       
       var existing = await repository.GetByIdAsync(@event.Id, default);
       var now = DateTimeOffset.UtcNow;

       await repository.InsertOrUpdateAsync(new MessageTracking
       {
         Id =  @event.Id ,
         SessionId = existing?. SessionId ??  @event.Id,
         Status = @event.MessageTracking.Status,
         RawBytes = @event.MessageTracking.RawBytes,
         CreatedAt = existing?.CreatedAt ?? now,
         LastUpdated = now
       }, default);
     }
   }
   
   
   