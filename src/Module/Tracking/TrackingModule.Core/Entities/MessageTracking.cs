namespace TrackingModule.Core.Entities;

public sealed class MessageTracking
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public MessageTrackingStatus Status { get; set; }
    public byte[]? RawBytes { get; set; }
    public string? ParsedJson { get; set; }
    public string? ErrorsJson { get; set; }
    public string? AckMessage { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset LastUpdated { get; set; }
}
