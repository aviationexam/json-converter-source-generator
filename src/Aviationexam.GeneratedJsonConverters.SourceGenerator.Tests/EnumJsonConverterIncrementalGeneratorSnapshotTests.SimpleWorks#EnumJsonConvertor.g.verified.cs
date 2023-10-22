//HintName: EnumJsonConvertor.g.cs
using System;
using System.Text.Json.Serialization;

namespace Aviationexam.GeneratedJsonConverters;

internal abstract class EnumJsonConvertor<T, TBackingType> : JsonConverter<T>
    where T : struct, Enum
    where TBackingType : struct
{
    protected abstract EnumDeserializationStrategy DeserializationStrategy { get; }

    protected abstract EnumSerializationStrategy SerializationStrategy { get; }
}
