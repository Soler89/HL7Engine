using HL7Engine.Module.Tracking.Domain.Repositories;
using HL7Engine.Module.Tracking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HL7Engine.Module.Tracking.Infrastructure.Repositories;

public sealed class MessageTrackingRepository(TrackingDbContext dbContext) : IMessageTrackingRepository
{
   
    public async Task InsertOrUpdateAsync(MessageTracking tracking, CancellationToken cancellationToken = default)
    {
        var existing = await dbContext.MessageTrackings
            .FirstOrDefaultAsync(x => x.Id == tracking.Id, cancellationToken);

        if (existing is null)
        {
            await dbContext.MessageTrackings.AddAsync(tracking, cancellationToken);
        }
        else
        {
            existing.SessionId = tracking.SessionId;
            existing.Status = tracking.Status;
            existing.RawBytes = tracking.RawBytes ?? existing.RawBytes;
            existing.ParsedJson = tracking.ParsedJson ?? existing.ParsedJson;
            existing.ErrorsJson = tracking.ErrorsJson ?? existing.ErrorsJson;
            existing.AckMessage = tracking.AckMessage ?? existing.AckMessage;
            existing.LastUpdated = tracking.LastUpdated;
            dbContext.MessageTrackings.Update(existing);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    

    public Task<MessageTracking?> GetByIdAsync(string correlationId, CancellationToken cancellationToken = default)
    {
        return  dbContext.MessageTrackings
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == correlationId, cancellationToken);
    }
}
