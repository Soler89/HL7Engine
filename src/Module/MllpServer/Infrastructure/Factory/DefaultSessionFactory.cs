using System.Net.Sockets;
using Hl7Cloud.Core.Domain.Interfaces;
using Hl7Engine.Module.MllpServer.Domain.Entities;

namespace Hl7Engine.Module.MllpServer.Infrastructure.Factory;

public class DefaultSessionFactory : ISessionFactory
{
    public ISession Create(IConnection client) => new MllpSession(client);
}