using Hl7Engine.Core.Application.Interfaces.Module;
using Hl7Engine.Core.Application.Message.Dto;
using HL7Engine.Module.Validate.Domain.Interfaces;
using NHapi.Base.Model;
using NHapi.Base.Util;

namespace Hl7Engine.Module.Validate.Infrastructure.Rules.HL7v2;

public class RequiredFieldRule(string fieldName, Func<MessageDto, string?> valueSelector)
    : IValidationRule
{
    public string? Validate(MessageDto message)
    {
        var value = valueSelector(message);
       return string.IsNullOrWhiteSpace(value) ? $"{fieldName} es obligatorio" : null;
    }

    
   
}