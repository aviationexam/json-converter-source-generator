using Aviationexam.GeneratedJsonConverters.Attributes;
using System;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Contracts;

[Flags]
[EnumJsonConverter(
    SerializationStrategy = EnumSerializationStrategy.BackingType | EnumSerializationStrategy.FlagsArray,
    DeserializationStrategy = EnumDeserializationStrategy.UseBackingType
)]
public enum EFlagsBackingEnum
{
    None = 0,
    Read = 1 << 0,
    Write = 1 << 1,
    Execute = 1 << 2,
}
