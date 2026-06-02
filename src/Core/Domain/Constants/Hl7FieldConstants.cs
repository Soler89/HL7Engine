namespace Hl7Cloud.Core.Domain.Constants;

public static class Hl7FieldConstants
{
    public const string MessageId = "MSH-10";


    public static string SendingFacility { get; set; } = "MSH-4";
    public static string ReceivingFacility { get; set; } = "MSH-6";
}   