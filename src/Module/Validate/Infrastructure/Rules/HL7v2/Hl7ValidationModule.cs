 
using Hl7Cloud.Core.Domain.Constants;
using Hl7Cloud.Module.Hl7Validate.Application;
using Hl7Engine.Core.Application.Interfaces.Module;
using Hl7Engine.Core.Application.Message.Dto;
using HL7Engine.Module.Validate.Domain.Entities;

namespace Hl7Engine.Module.Validate.Infrastructure.Rules.HL7v2;

public class Hl7ValidationModule : IValidateMessage<MessageDto>
{
    private readonly ValidationRulesRepository? _rules;

    public Hl7ValidationModule()
    {
        _rules = new ValidationRulesRepository();
        _rules.AddRuleForType("ADT", new RequiredSegmentRule("EVN"))
            .AddRuleForType("ADT", new RequiredSegmentRule("PID"))
            .AddRuleForType("ADT", new RequiredSegmentRule("PV1"))
            .AddRule(new RequiredFieldRule("MSH-4",   m =>m.GetField(Hl7FieldConstants.SendingFacility))) // Receiving Application
            .AddRule(new RequiredFieldRule("MSH-6", m =>m.GetField(Hl7FieldConstants.ReceivingFacility))); // Receiving Facility;

        /*  _rules?
              .AddRule(new RequiredFieldRule("MSH-9",m=>) // Message Type
              .AddRule(new RequiredFieldRule("MSH-10")) // Message Control ID
              .AddRule(new RequiredFieldRule("MSH-3")) // Sending Application
              .AddRule(new RequiredFieldRule("MSH-4")) // Sending Facility
              .AddRule(new RequiredFieldRule("MSH-5")) // Receiving Application
              .AddRule(new RequiredFieldRule("MSH-6")); // Receiving Facility

          _rules?
              .AddRuleForType("ADT", new RequiredSegmentRule("PID")).
          AddRuleForType("ADT", new RequiredFieldRule("PID-3"))    // Patient ID
          .AddRuleForType("ADT", new RequiredFieldRule("PID-5.1"))  // Family Name
          .AddRuleForType("ADT", new RequiredFieldRule("PID-7"))    // Date of Birth
          .AddRuleForType("ADT", new RequiredFieldRule("PV1-2"))    // Patient Class
          .AddRuleForType("ADT", new RequiredFieldRule("PV1-44")); // Admit Date/Time
      */
    }

    

    

    Task<ValidationResult> IValidateMessage<MessageDto>.ValidateAsync(MessageDto message)
    {
        var errors = _rules?.Validate(message).ToList();
        return Task.FromResult(new ValidationResult(errors.Count == 0, errors));
    }
}
