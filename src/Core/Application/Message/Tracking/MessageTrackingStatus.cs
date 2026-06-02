namespace Hl7Engine.Core.Application.Message.Tracking;

public enum MessageTrackingStatus
{
    Received,
    ParsedSuccess,
    ValidationPassed,
    ValidationFailed,
    ParsingFailed,
    AckGenerated
}