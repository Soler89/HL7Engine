using Hl7Engine.Module.MllpServer.Application.Pipelines;
using Hl7Engine.Module.MllpServer.Domain.Entities;
using Hl7Engine.Module.MllpServer.Infrastructure.Mllp;

namespace Hl7Engine.Module.MllpServer.Infrastructure.PipelineSteps;

public sealed class ExtractMllpFrameStep : IMllpPipelineStep
{
    public async Task InvokeAsync(MllpConnectionContext ctx, Func<Task> next, CancellationToken ct)
    {
        var result = await MllpHelper.ReadMllpMessageAsync(ctx.Client);
        if (!result.IsValid || result.Payload == null)
            throw new Exception("Trama MLLP inválida");

        ctx.Frame = result.Payload;
        await next();
    }
}