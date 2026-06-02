namespace Hl7Cloud.Core.Domain.Interfaces;

public interface ISessionFactory
{
    ISession Create(IConnection client);
}