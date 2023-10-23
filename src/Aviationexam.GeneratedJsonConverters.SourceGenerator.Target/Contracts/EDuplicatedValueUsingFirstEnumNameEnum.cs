using Aviationexam.GeneratedJsonConverters.Attributes;
using System.Runtime.Serialization;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Contracts;

[EnumJsonConverter(SerializationStrategy = EnumSerializationStrategy.FirstEnumName, DeserializationStrategy = EnumDeserializationStrategy.UseEnumName)]
public enum EDuplicatedValueUsingFirstEnumNameEnum
{
    [EnumMember(Value = "C")]
    A = 0,

    [EnumMember(Value = "D")]
    B = 1,

    [EnumMember(Value = "E")]
    C = 1,
}
