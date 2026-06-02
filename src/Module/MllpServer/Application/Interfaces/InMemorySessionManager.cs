using System.Collections.Concurrent;
using Hl7Cloud.Core.Domain.Interfaces;

namespace Hl7Engine.Module.MllpServer.Application.Interfaces;

public class InMemorySessionManager : ISessionManager
{
    private readonly ConcurrentDictionary<string, ISession> _sessions = new();

    public void Register(ISession session) => _sessions.TryAdd(session.SessionId, session);

    public void Unregister(string sessionId) => _sessions.TryRemove(sessionId, out _);

    public ISession? Get(string sessionId) => _sessions.TryGetValue(sessionId, out var session) ? session : null;

    public IEnumerable<ISession> GetAll() => _sessions.Values;
}