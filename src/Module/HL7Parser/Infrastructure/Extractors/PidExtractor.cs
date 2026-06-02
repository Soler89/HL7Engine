using Hl7Engine.Core.Application.Message.Dto;
using Hl7Engine.Module.Parser.Infrastructure.Interfaces;
using NHapi.Base.Model;
using NHapi.Base.Util;

namespace Hl7Engine.Module.Parser.Infrastructure.Extractors;

public class PidExtractor : ISegmentExtractor
{
    public string SegmentName => "PID";
 


    public void Extract(IMessage hl7Msg, MessageDto dto)
    {
        // MSH siempre existe (el parseo ya lo garantiza), pero por seguridad verificamos
        var segment = hl7Msg.GetAll(SegmentName);
        if (segment.Length == 0 ) return;
        
        dto.AddSegment(SegmentName);
        var terser = new Terser(hl7Msg);
        dto.AddField("MSH-3-1", terser.Get("/PID-3-1")?.Trim()!);
        dto.AddField("MSH-5-1",terser.Get("/PID-5-1")?.Trim()!);        // Primer apellido del paciente
        dto.AddField("MSH-7",terser.Get("/PID-7")?.Trim()!);       // Fecha de nacimiento (formato yyyyMMdd)
        dto.AddField("MSH-8",terser.Get("/PID-8")?.Trim()!);       
        // Extraer los campos más relevantes
        /* dto.PatientId          = terser.Get("/PID-3-1")?.Trim();        // Identificador del paciente (ej: historia clínica)
         dto.PatientName        = terser.Get("/PID-5-1")?.Trim();        // Primer apellido del paciente
         dto.DateOfBirth        = ParseDate(terser.Get("/PID-7"));       // Fecha de nacimiento (formato yyyyMMdd)
         dto.AdministrativeSex  = terser.Get("/PID-8")?.Trim();          // Sexo administrativo (F/M/O/U)
         */
    }

    private static DateTime? ParseDate(string? hl7Date)
    {
        if (string.IsNullOrWhiteSpace(hl7Date) || hl7Date.Length < 8)
            return null;

        if (DateTime.TryParseExact(hl7Date.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out var date))
        {
            return date;
        }
        return null;
    }
}