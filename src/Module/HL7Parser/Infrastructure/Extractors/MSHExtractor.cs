using Hl7Engine.Core.Application.Message.Dto;
using Hl7Engine.Module.Parser.Infrastructure.Interfaces;
using NHapi.Base.Model;
using NHapi.Base.Util;


namespace Hl7Engine.Module.Parser.Infrastructure.Extractors;

public class MshExtractor : ISegmentExtractor
{
    public string SegmentName => "MSH";
  

    private static void AddIfNotNull(Dictionary<string, string> fields, string key, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
            fields.TryAdd(key, value);
    }

    public void Extract(IMessage hl7Msg, MessageDto dto)
    {
        // MSH siempre existe (el parseo ya lo garantiza), pero por seguridad verificamos
        var segment = hl7Msg.GetAll(SegmentName);
        if (segment.Length == 0 ) return;
        
         dto.AddSegment(SegmentName);
         var terser = new Terser(hl7Msg);

        // MSH-3   : Sending Application (campo completo) – Aplicación que envía el mensaje
        dto.AddField("MSH-3", terser.Get("/MSH-3")?.Trim()!);

        // MSH-4   : Sending Facility (campo completo) – Instalación/Centro que envía el mensaje
        dto.AddField("MSH-4", terser.Get("/MSH-4")?.Trim()!);

        // MSH-5-1 : Receiving Application  – Identificador  de la aplicación que recibe
        dto.AddField("MSH-5", terser.Get("/MSH-5")?.Trim()!);

        // MSH-6-1 : Receiving Facility   – Identificador  de la instalación que recibe
        dto.AddField("MSH-6", terser.Get("/MSH-6")?.Trim()!);

        // MSH-7   : Date/Time of Message – Fecha y hora del mensaje en formato YYYYMMDD[HHMM[SS]]
        dto.AddField("MSH-7", terser.Get("/MSH-7")?.Trim()!);

        // MSH-9   : Message Type (campo completo) – Tipo de mensaje compuesto (ej: ADT^A01)
        dto.AddField("MSH-9", terser.Get("/MSH-9")?.Trim()!);

        // MSH-9-1 : Message Code – Código del tipo de mensaje (ej: ADT)
        dto.AddField("MSH-9-1", terser.Get("/MSH-9-1")?.Trim()!);

        // MSH-9-2 : Trigger Event – Evento disparador (ej: A01, A04, A08)
        dto.AddField("MSH-9-2", terser.Get("/MSH-9-2")?.Trim()!);

        // MSH-10  : Message Control ID – Identificador único del mensaje
        dto.AddField("MSH-10", terser.Get("/MSH-10")?.Trim()!);

        // MSH-12  : Version ID – Versión del estándar HL7 (ej: 2.5, 2.3.1)
        dto.AddField("MSH-12", terser.Get("/MSH-12")?.Trim()!);


        // Emisor y receptor (MSH-3 a MSH-6)
        /*  dto.SendingApplication    = terser.Get("/MSH-3-1")?.Trim();
          dto.SendingFacility       = terser.Get("/MSH-4-1")?.Trim();
          dto.ReceivingApplication  = terser.Get("/MSH-5-1")?.Trim();
          dto.ReceivingFacility     = terser.Get("/MSH-6-1")?.Trim();

          // Fecha/hora del mensaje (MSH-7)
          dto.MessageDateTime = ParseDateTime(terser.Get("/MSH-7"));

          // Tipo de mensaje (MSH-9) y sus componentes
          dto.MessageType       = terser.Get("/MSH-9")?.Trim();
          dto.MessageTypeCode   = terser.Get("/MSH-9-1")?.Trim();  // ej: "ADT"
          dto.TriggerEvent      = terser.Get("/MSH-9-2")?.Trim();  // ej: "A01"

          // ID de control (MSH-10)
          dto.MessageId = terser.Get("/MSH-10")?.Trim();

          // Versión de HL7 (MSH-12)
          dto.VersionId = terser.Get("/MSH-12")?.Trim();*/
    }

    private static DateTime? ParseDateTime(string? hl7DateTime)
    {
        if (string.IsNullOrWhiteSpace(hl7DateTime) || hl7DateTime.Length < 14)
            return null;

        if (DateTime.TryParseExact(hl7DateTime.Substring(0, 14), "yyyyMMddHHmmss",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out var date))
        {
            return date;
        }
        return null;
    }
}