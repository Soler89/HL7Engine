using Hl7Engine.Core.Application.Message.Dto;

namespace Hl7Cloud.Module.Hl7Validate.Application
{
    /// <summary>
    /// Servicio de validación de mensajes clínicos parseados.
    /// </summary>
    public interface IValidateMessage
    {
        /// <summary>
        /// Valida un mensaje clínico y devuelve un resultado con los errores encontrados.
        /// </summary>
        /// <param name="message">Mensaje parseado a validar.</param>
        /// <param name="connectionId">ID de conexión para trazabilidad.</param>
        /// <returns>Resultado de la validación con indicador de éxito y lista de errores.</returns>
        Task<ValidationResult> ValidateAsync(MessageDto message);
    }

    /// <summary>
    /// Resultado inmutable de la validación.
    /// </summary>
    public record ValidationResult(bool IsValid, IReadOnlyCollection<string> Errors);
}