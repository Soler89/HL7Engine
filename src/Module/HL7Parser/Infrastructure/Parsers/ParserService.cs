using HL7Engine.Module.Parser.Application.Configuration;
using HL7Engine.Module.Parser.Application.Interfaces;
using Hl7Engine.Module.Parser.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using static HL7Engine.Module.Parser.Application.Configuration.Encoding;

namespace Hl7Engine.Module.Parser.Infrastructure.Parsers;

public class ParserServices(PipeParser parser,IOptions<ParserConfig> config):IParserServices
{
    private string Encoder { get; set; } =  "UTF-8";

    public string GetString(byte[] data)
    {
        switch (Encoder)
        {
            case Encoding.UTF8:
                return  System.Text.Encoding.UTF8.GetString(data);
        }

        return String.Empty;

    }

    

    

    public IMessage Parser(byte[] data)
    {
        
        switch (Encoder)
        {   
            case Encoding.UTF8:
                string message = System.Text.Encoding.UTF8.GetString(data);
                return parser.Parse(message);
                break;
            default:
                break;
            
        }

        return null;

    }


    
}