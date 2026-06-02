using NHapi.Base.Model;

namespace HL7Engine.Module.Parser.Application.Interfaces;

public interface IParserServices
{
    IMessage Parser(byte[] data);
  
}