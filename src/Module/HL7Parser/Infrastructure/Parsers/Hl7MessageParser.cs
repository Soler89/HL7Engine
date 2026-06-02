using Hl7Engine.Core.Application.Message.Dto;
using HL7Engine.Module.Parser.Application.Interfaces;
using Hl7Engine.Module.Parser.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using NHapi.Base.Parser;
using NHapi.Base.Util;

namespace Hl7Engine.Module.Parser.Infrastructure.Parsers;

public class Hl7MessageParser:IMessageParser<Hl7MessageDto>
{
    private readonly ILogger<Hl7MessageParser> _logger;
    private readonly PipeParser _parser;
    private readonly Dictionary<string, ISegmentExtractor> _segmentExtractors;

    // Mapa de segmentos requeridos por tipo de mensaje
    private readonly Dictionary<string, string[]> _messageSegmentMap = new()
    {
        ["ADT"] = new[] { "MSH", "EVN", "PID", "PV1" },
        ["ORU"] = new[] { "MSH", "PID", "OBR", "OBX" },
        // etc.
    };

    public Hl7MessageParser(ILogger<Hl7MessageParser> _logger,PipeParser parser, IEnumerable<ISegmentExtractor> extractors) 
    {
        this._logger = _logger;
        _parser = parser;
        _segmentExtractors = extractors.ToDictionary(e => e.SegmentName, e => e);
    }

     
    
    public Hl7MessageDto Parser(byte[] mesage)
    {var dto = new Hl7MessageDto
        {
            Format = "HL7v2"
            // La mayoría de campos ya serán llenados por los extractores
        };
        try
        {
             
            var msg = _parser.Parse(System.Text.Encoding.UTF8.GetString(mesage));
             
            

            // Determinar el tipo de mensaje a partir de MSH-9 (sin extractor aún)
            var terser = new Terser(msg);
            string msgType = terser.Get("/MSH-9") ?? "";
           
           string messageTypeCode = msgType.Split('^').FirstOrDefault() ?? "";
            // Llenar la lista de segmentos presentes para la validación
            
           
             
            // Aplicar los extractores configurados para el tipo de mensaje
            if (_messageSegmentMap.TryGetValue(messageTypeCode, out var segmentNames))
            {
                foreach (var seg in segmentNames)
                {
                    if (_segmentExtractors.TryGetValue(seg, out var extractor))
                    {
                        extractor.Extract(msg, dto);
                    }
                }
            }

            return dto;
        }
        catch (Exception ex)
        {
            dto.IsValid = false;
            // Manejar error, publicar evento de fallo, etc.
            return null;
        }
    }
}
