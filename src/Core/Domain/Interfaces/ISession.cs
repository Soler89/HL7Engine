namespace Hl7Cloud.Core.Domain.Interfaces;

public interface ISession
{
    /// <summary>Identificador único de la sesión.</summary>
    string SessionId { get; }

    /// <summary>Cliente TCP asociado a esta sesión.</summary>
    IConnection Client { get; }

    /// <summary>Total de bytes leídos en esta sesión.</summary>
    long BytesRead { get; }

    /// <summary>Cantidad de mensajes MLLP procesados en esta sesión.</summary>
    long MessagesProcessed { get; }

    /// <summary>Acumula bytes leídos tras procesar una trama.</summary>
    void AddBytesRead(long bytes);

    /// <summary>Incrementa el contador de mensajes procesados.</summary>
    void IncrementMessages();
}