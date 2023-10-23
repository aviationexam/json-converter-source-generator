using Aviationexam.GeneratedJsonConverters.Attributes;
using System.Runtime.Serialization;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Contracts;

[EnumJsonConverter(
    SerializationStrategy = EnumSerializationStrategy.FirstEnumName,
    DeserializationStrategy = EnumDeserializationStrategy.UseEnumName
)]
public enum EConfiguredPropertyEnum
{
    [EnumMember(Value = "C")]
    A,

    [EnumMember(Value = "D")]
    B,
}
