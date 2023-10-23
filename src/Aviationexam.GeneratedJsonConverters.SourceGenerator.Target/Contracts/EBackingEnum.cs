using Aviationexam.GeneratedJsonConverters.Attributes;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Contracts;

[EnumJsonConverter(
    SerializationStrategy = EnumSerializationStrategy.BackingType,
    DeserializationStrategy = EnumDeserializationStrategy.UseBackingType
)]
public enum EBackingEnum
{
    A,
    B,
}
