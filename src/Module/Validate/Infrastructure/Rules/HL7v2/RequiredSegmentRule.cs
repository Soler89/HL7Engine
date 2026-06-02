using Hl7Engine.Core.Application.Interfaces.Module;
using Hl7Engine.Core.Application.Message.Dto;
using HL7Engine.Module.Validate.Domain.Interfaces;
using NHapi.Base.Util;

namespace Hl7Engine.Module.Validate.Infrastructure.Rules.HL7v2;

public class RequiredSegmentRule(string segmentName) : IValidationRule
{
    public string? Validate(MessageDto message)
    {
        if (!message.HasSegments(segmentName))
            return $"Segmento {segmentName} es obligatorio";

        return null; // El segmento existe
    }
}