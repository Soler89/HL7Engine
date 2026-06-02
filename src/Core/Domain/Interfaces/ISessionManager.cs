namespace Hl7Cloud.Core.Domain.Interfaces;

public interface ISessionManager
{
    void Register(ISession session);
    void Unregister(string sessionId);
    ISession? Get(string sessionId);
    IEnumerable<ISession> GetAll();
}
