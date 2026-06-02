using System.Net.Sockets;
using Hl7Engine.Core.Application.Interfaces.SocketStore;


namespace Hl7Engine.Core.Infrastructure.SocketStore;

public class SocketStore:ISocketStore
{
    private readonly Dictionary<string, TcpClient> _sockets = new();
    public void Register(string connectionId, TcpClient client)
    {
        if (!Contains(connectionId))
            _sockets[connectionId] = client;
    }

    public bool TryGet(string connectionId, out TcpClient? client)
    {
        client = Contains(connectionId) ? _sockets[connectionId] : null;
        return client != null;
    }

    public void Unregister(string connectionId)
    {
        if (Contains(connectionId))
         _sockets.Remove(connectionId);
    }

    public bool Contains(string connectionId)
    {
        return _sockets.ContainsKey(connectionId);
    }
}
