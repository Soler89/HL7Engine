using Hl7Engine.Module.MllpServer.Domain.Entities;

namespace Hl7Engine.Module.MllpServer.Application.Pipelines;

public interface IMllpPipelineStep
{
    Task InvokeAsync(
        MllpConnectionContext context,
        Func<Task> next,
        CancellationToken ct);
}
