using System.Net.Sockets;

namespace Hl7Engine.Core.Application.Interfaces.SocketStore;

public interface ISocketStore
{
    void Register(
        string connectionId,
        TcpClient client);

    bool TryGet(
        string connectionId,
        out TcpClient? client);

    void Unregister(string connectionId);

   bool Contains(string connectionId);

    //int Count { get; }

    //IReadOnlyCollection<string> ConnectionIds { get; }
}
