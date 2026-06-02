using Hl7Engine.Core.Application.Interfaces.Module;
using Hl7Engine.Core.Application.Message.Dto;
using HL7Engine.Module.Validate.Domain.Interfaces;

namespace HL7Engine.Module.Validate.Domain.Entities;

public class ValidationRulesRepository
{
    // Reglas que se aplican a todos los mensajes
    private readonly List<IValidationRule> _globalRules = new();

    // Reglas específicas por tipo de mensaje (clave = tipo, ej: "ADT", "ORU")
    private readonly Dictionary<string, List<IValidationRule>> _typeRules = new();

    /// <summary>
    /// Añade una regla global.
    /// </summary>
    public ValidationRulesRepository AddRule(IValidationRule rule)
    {
        _globalRules.Add(rule);
        return this;
    }

    /// <summary>
    /// Añade una regla que solo se ejecutará si el mensaje es del tipo indicado.
    /// </summary>
    /// <param name="messageType">Código del tipo (ej: "ADT", "ORU").</param>
    public ValidationRulesRepository AddRuleForType(string messageType, IValidationRule rule)
    {
        if (!_typeRules.ContainsKey(messageType))
            _typeRules[messageType] = new List<IValidationRule>();
        _typeRules[messageType].Add(rule);
        return this;
    }

    /// <summary>
    /// Ejecuta todas las reglas aplicables al mensaje.
    /// </summary>
    public IEnumerable<string> Validate(MessageDto message)
    {
        
        // 1. Aplicar reglas globales
        foreach (var rule in _globalRules)
        {
            var error = rule.Validate(message);
            if (error != null)
                yield return error;
        }

        // 2. Determinar el tipo de mensaje (parte antes de '^')
        /*string? typeCode = message.MessageType?.Split('^').FirstOrDefault();

        if (typeCode != null && _typeRules.TryGetValue(typeCode, out var specificRules))
        {
            foreach (var rule in specificRules)
            {
                
                var error = rule.Validate(message);
                if (error != null)
                    yield return error;
            }
        }*/
    }
}