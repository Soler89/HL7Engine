namespace HL7Engine.Module.Parser.Application.Interfaces;

public interface IMessageParser<out T>
{
 T Parser(byte[] mesage);
}