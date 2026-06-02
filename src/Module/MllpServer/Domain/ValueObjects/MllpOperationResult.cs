namespace Hl7Engine.Module.MllpServer.Domain.ValueObjects;

public sealed class MllpOperationResult
{
    public bool IsValid { get; }
    public byte[]? Payload { get; }
    public string? ErrorMessage { get; }

    private MllpOperationResult(bool isValid, byte[]? payload, string? errorMessage)
    {
        IsValid = isValid;
        Payload = payload;
        ErrorMessage = errorMessage;
    }

    public static MllpOperationResult Success(byte[] payload)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));
        
        return new MllpOperationResult(true, payload, null);
    }

    public static MllpOperationResult Failure(string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(errorMessage))
            throw new ArgumentException("El mensaje de error no puede estar vacío", nameof(errorMessage));
        
        return new MllpOperationResult(false, null, errorMessage);
    }

    // Opcional: para deconstrucción o logging
    public void Deconstruct(out bool isValid, out byte[]? payload, out string? errorMessage)
    {
        isValid = IsValid;
        payload = Payload;
        errorMessage = ErrorMessage;
    }
}