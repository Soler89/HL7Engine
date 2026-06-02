using Hl7Engine.Core.Application.Message.Dto;

namespace Hl7Engine.Core.Application.Interfaces.Module;

public interface IValidatedMessage
{
    MessageDto Message { get; }

    bool IsValid { get; }

    IReadOnlyList<string> Errors { get; }
}

 