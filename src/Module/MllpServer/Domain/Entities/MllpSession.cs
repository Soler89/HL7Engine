using Hl7Cloud.Core.Domain.Interfaces;
 
namespace Hl7Engine.Module.MllpServer.Domain.Entities;

public sealed class MllpSession:ISession
{
    public string SessionId { get; } = Guid.NewGuid().ToString();
    

    public IConnection Client { get; }
    public long BytesRead { get; private set; }
    public long MessagesProcessed { get; private set; }

    public MllpSession(IConnection client)
    {
        Client = client;
    }

    public void AddBytesRead(long bytes) => BytesRead += bytes;
    public void IncrementMessages() => MessagesProcessed++;
}