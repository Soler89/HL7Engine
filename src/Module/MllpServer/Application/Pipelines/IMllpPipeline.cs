using Hl7Engine.Module.MllpServer.Domain.Entities;

namespace Hl7Engine.Module.MllpServer.Application.Pipelines;

public interface IMllpPipeline
{
    /// <summary>
    /// Ejecuta todos los pasos del pipeline de forma encadenada,
    /// pasando el contexto compartido y respetando la cancelación.
    /// </summary>
    Task ExecuteAsync(MllpConnectionContext ctx, CancellationToken ct);
}