namespace TrackingModule.Core;

public enum MessageTrackingStatus
{
    Received,
    Parsed,
    ValidationPassed,
    ValidationFailed,
    ParsingFailed,
    AckGenerated
}
