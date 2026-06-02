namespace Hl7Engine.Core.Application.Message.Tracking;

public class MessageTrackingUpdateDto
{
     
    
    public   MessageTrackingStatus Status { get;
        set;
    }

    public byte[]? RawBytes { get; set; }
    public string? ParsedString { get; set; }
    
    
   
}