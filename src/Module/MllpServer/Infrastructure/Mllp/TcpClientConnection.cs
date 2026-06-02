using System.Net.Sockets;
using Hl7Cloud.Core.Domain.Interfaces;

namespace Hl7Engine.Module.MllpServer.Infrastructure.Mllp;

internal sealed class TcpClientConnection(TcpClient client) : IConnection
{
    private NetworkStream Stream => client.GetStream();

    public bool IsConnected => client.Connected;

    public async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken ct)
        => await Stream.ReadAsync(buffer, offset, count, ct);

    public async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken ct)
        => await Stream.WriteAsync(buffer, offset, count, ct);

    public void Close() => client.Close();
}