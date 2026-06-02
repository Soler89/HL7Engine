using System.Net.Sockets;
using Hl7Cloud.Core.Domain.Interfaces;
using Hl7Engine.Module.MllpServer.Domain.ValueObjects;

namespace Hl7Engine.Module.MllpServer.Infrastructure.Mllp;

public  static class MllpHelper
{
    private const byte MllpStartBlock = 0x0B; // VT
    private const byte MllpEndBlock = 0x1C;   // FS
    private const byte CarriageReturn = 0x0D; // CR
    
 

    public static async Task<MllpOperationResult> ReadMllpMessageAsync(
        IConnection connection, 
        CancellationToken ct = default)
    {
        using var ms = new MemoryStream();
        bool started = false;
        byte? previous = null;

        while (!ct.IsCancellationRequested)
        {
            byte[] buffer = new byte[1];
            int read = await connection.ReadAsync(buffer, 0, 1, ct);
        
            if (read == 0)
                return MllpOperationResult.Failure("Conexión cerrada antes de recibir el mensaje");

            byte b = buffer[0];

            // Opcional: buscar el byte de inicio (si se requiere)
            if (!started)
            {
                if (b == MllpStartBlock)  // 0x0B
                {
                    started = true;
                    continue;
                }
                // Si no se espera un start block, tratamos el primer byte como parte del mensaje
                // (algunos sistemas no envían el VT). Para hacerlo obligatorio, devuelve error.
                // Aquí lo hacemos opcional:
                started = true;
                // No hacemos continue, procesamos este byte como contenido.
            }

            // Detectar el final: FS (0x1C) seguido de CR (0x0D)
            if (previous == MllpEndBlock && b == CarriageReturn)
            {
                // Quitamos el FS que ya añadimos al stream
                ms.SetLength(ms.Length - 1);
                return MllpOperationResult.Success(ms.ToArray());
            }

            ms.WriteByte(b);
            previous = b;
        }

        return MllpOperationResult.Failure("Cancelado por el token");
    
 
    }
}