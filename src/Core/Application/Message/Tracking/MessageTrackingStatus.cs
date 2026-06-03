namespace Hl7Engine.Core.Application.Message.Tracking;

public enum MessageTrackingStatus
{
    Received,
    ParsedSuccess,
    ValidationSuccess,
    ValidationFailed,
    ParsingFailed,
    AckGenerated
}