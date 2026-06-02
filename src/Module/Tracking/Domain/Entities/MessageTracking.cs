 

using Hl7Engine.Core.Application.Message.Tracking;

public sealed class MessageTracking
{
    public string Id { get; set; }
    public string SessionId { get; set; }
    public MessageTrackingStatus Status { get; set; }
    public byte[]? RawBytes { get; set; }
    public string? ParsedJson { get; set; }
    public string? ErrorsJson { get; set; }
    public string? AckMessage { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset LastUpdated { get; set; }
}
