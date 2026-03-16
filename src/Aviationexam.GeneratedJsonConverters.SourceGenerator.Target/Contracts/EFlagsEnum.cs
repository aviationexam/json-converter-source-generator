using Aviationexam.GeneratedJsonConverters.Attributes;
using System;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Contracts;

[Flags]
[EnumJsonConverter(
    SerializationStrategy = EnumSerializationStrategy.FirstEnumName | EnumSerializationStrategy.FlagsArray,
    DeserializationStrategy = EnumDeserializationStrategy.UseEnumName
)]
public enum EFlagsEnum
{
    None = 0,
    Read = 1 << 0,
    Write = 1 << 1,
    Execute = 1 << 2,
    ReadWrite = Read | Write,
}
