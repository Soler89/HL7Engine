namespace Hl7Engine.Core.Application.Message.Tracking;

public enum MessageTrackingStatus
{
    Received,
    Parsed,
    ValidationPassed,
    ValidationFailed,
    ParsingFailed,
    AckGenerated
}