using Hl7Engine.Module.MllpServer.Domain.Entities;

namespace Hl7Engine.Module.MllpServer.Application.Pipelines;

public sealed class MllpPipeline(IMllpPipelineStep[] steps) : IMllpPipeline
{
    public Task ExecuteAsync(MllpConnectionContext ctx, CancellationToken ct)
    {
        Func<Task> next = () => Task.CompletedTask;
        foreach (var step in steps.Reverse())
        {
            var current = next;
            next = () => step.InvokeAsync(ctx, current, ct);
        }
        return next();
    }
      
}