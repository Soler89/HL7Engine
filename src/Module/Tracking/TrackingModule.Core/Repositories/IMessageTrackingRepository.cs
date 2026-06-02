using TrackingModule.Core.Entities;

namespace TrackingModule.Core.Repositories;

public interface IMessageTrackingRepository
{
    Task InsertOrUpdateAsync(MessageTracking tracking, CancellationToken cancellationToken = default);
    Task<MessageTracking?> GetByIdAsync(Guid correlationId, CancellationToken cancellationToken = default);
}
