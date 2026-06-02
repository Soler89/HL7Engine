using System.Text;
using Hl7Cloud.Module.Tracking.IntegrationEvents.Event;
using Hl7Engine.Core.Application.Interfaces.EventBus;
using Hl7Engine.Core.Application.Message.Tracking;
using Hl7Engine.Module.MllpServer.Application.Pipelines;
using Hl7Engine.Module.MllpServer.Domain.Entities;
using Hl7Engine.Module.MllpServer.IntegrationEvents.Event;


namespace Hl7Engine.Module.MllpServer.Infrastructure.PipelineSteps;

public sealed class PublishIntegrationEventStep(IEventBus eventBus) : IMllpPipelineStep
{
    public async Task InvokeAsync(MllpConnectionContext ctx, Func<Task> next, CancellationToken ct)
    {
        await next();

        if (ctx.IsTerminated)
        {
            // Evento de fallo (crea una clase para esto)
            // ctx.Frame != null ? Encoding.UTF8.GetString(ctx.Frame) : null
            //});  await _eventBus.Publish(new MllpMessageFailedIntegrationEvent
            //{
            //    ConnectionId = ctx.Session!.SessionId,
            //    Error = ctx.FailureReason!,
            //    RawHl7 =
        }
        else
        {
            string id = ctx.Session!.SessionId;
            var rawHl7 =   ctx.Frame;

            await eventBus.Publish(
                new MessageStatusChangedIntegrationEvent(id,
                    new MessageTrackingUpdateDto() { RawBytes = rawHl7,Status = MessageTrackingStatus.Received}));
               
            await eventBus.Publish(new MllpMessageIncomingIntegrationEvent(
               id,rawHl7!
            ));
        }
    }
}