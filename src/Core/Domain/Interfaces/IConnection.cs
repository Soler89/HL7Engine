namespace Hl7Cloud.Core.Domain.Interfaces;
public interface IConnection
{
    Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken ct);
    Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken ct);
    bool IsConnected { get; }
    void Close();
}