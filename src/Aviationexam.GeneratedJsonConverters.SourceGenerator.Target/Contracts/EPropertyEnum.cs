using Aviationexam.GeneratedJsonConverters.Attributes;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Contracts;

[EnumJsonConverter(
    SerializationStrategy = EnumSerializationStrategy.FirstEnumName,
    DeserializationStrategy = EnumDeserializationStrategy.UseEnumName
)]
public enum EPropertyEnum : byte
{
    C,
    D,
}
