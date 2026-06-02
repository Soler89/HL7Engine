

using Hl7Engine.Core.Application.Interfaces.Module;
using Hl7Engine.Core.Application.Message.Dto;


namespace HL7Engine.Module.Validate.Domain.Interfaces;

public interface IValidationRule
{
    string? Validate(MessageDto message);
}