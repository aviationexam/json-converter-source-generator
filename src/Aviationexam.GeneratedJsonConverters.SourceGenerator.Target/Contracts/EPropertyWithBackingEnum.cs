using Aviationexam.GeneratedJsonConverters.Attributes;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Contracts;

[EnumJsonConverter(
    SerializationStrategy = EnumSerializationStrategy.FirstEnumName,
    DeserializationStrategy = EnumDeserializationStrategy.UseBackingType | EnumDeserializationStrategy.UseEnumName
)]
public enum EPropertyWithBackingEnum
{
    E,
    F,
}
