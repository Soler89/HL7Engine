using Hl7Engine.Core.Application.Message.Dto;
using NHapi.Base.Model;

namespace Hl7Engine.Module.Parser.Infrastructure.Interfaces;

public interface ISegmentExtractor
{
    /// <summary>
    /// Nombre del segmento que maneja (ej: "PID", "PV1", "OBR", "OBX").
    /// </summary>
    string SegmentName { get; }

    /// <summary>
    /// Extrae los datos del segmento desde el mensaje HL7 y los vuelca en el DTO.
    /// </summary>
    /// <param name="hl7Msg">Mensaje HL7 completo parseado por nHapi.</param>
    /// <param name="dto">DTO que se rellenará con los datos extraídos.</param>
    void Extract(IMessage hl7Msg, MessageDto dto);
}