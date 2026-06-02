 

namespace HL7Engine.Module.Tracking.Domain.Repositories;

public interface IMessageTrackingRepository
{
    Task InsertOrUpdateAsync(MessageTracking tracking, CancellationToken cancellationToken = default);
    Task<MessageTracking?> GetByIdAsync(string correlationId, CancellationToken cancellationToken = default);
}
