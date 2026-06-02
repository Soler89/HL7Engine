namespace Hl7Engine.Core.Infrastructure.Attribute;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class Hl7FieldAttribute(string fieldRef) : System.Attribute
{
    public string FieldRef { get; } = fieldRef;
}