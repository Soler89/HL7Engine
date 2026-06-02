 


using Hl7Cloud.Core.Domain.Interfaces;

namespace Hl7Engine.Module.MllpServer.Domain.Entities;

public class MllpConnectionContext
{
    

    /// <summary>Socket TCP de la conexión entrante.</summary>
    public required IConnection Client { get; init; }

    /// <summary>Sesión asociada a esta conexión.</summary>
    public ISession? Session { get; set; }

    /// <summary>Última trama MLLP extraída (payload sin delimitadores).</summary>
    public byte[]? Frame { get; set; }

    /// <summary>Indica si la conexión debe finalizar (por error o cancelación).</summary>
    public bool IsTerminated { get; private set; }

    /// <summary>Motivo del fallo, establecido al llamar a <see cref="Terminate"/>.</summary>
    public string? FailureReason { get; private set; }

    /// <summary>Acceso unificado al flujo de red.</summary>
    
    /// <summary>Marca la conexión como terminada y guarda el motivo.</summary>
    public void Terminate(string reason)
    {
        IsTerminated = true;
        FailureReason = reason;
    }

    /// <summary>Restablece el estado para procesar la siguiente trama (solo si no está terminado).</summary>
    public void ResetForNextFrame()
    {
        if (!IsTerminated)
        {
            Frame = null;
            FailureReason = null;
        }
    }
}