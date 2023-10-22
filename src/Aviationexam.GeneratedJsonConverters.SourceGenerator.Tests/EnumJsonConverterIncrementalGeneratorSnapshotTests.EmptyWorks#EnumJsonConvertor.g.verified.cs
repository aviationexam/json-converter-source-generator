//HintName: EnumJsonConvertor.g.cs
using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aviationexam.GeneratedJsonConverters;

internal abstract class EnumJsonConvertor<T, TBackingType> : JsonConverter<T>
    where T : struct, Enum
    where TBackingType : struct
{
    protected abstract TypeCode BackingTypeTypeCode { get; }

    protected abstract EnumDeserializationStrategy DeserializationStrategy { get; }

    protected abstract EnumSerializationStrategy SerializationStrategy { get; }

    protected abstract T ToEnum(ReadOnlySpan<byte> enumName);

    protected abstract T ToEnum(TBackingType numericValue);

    public override T Read(
        ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options
    )
    {
        if (
            reader.TokenType is JsonTokenType.String
            && DeserializationStrategy.HasFlag(EnumDeserializationStrategy.UseEnumName)
        )
        {
            var enumName = reader.ValueSpan;

            return ToEnum(enumName);
        }

        if (reader.TokenType is JsonTokenType.Number)
        {
            var numericValue = ReadAsNumber(ref reader);

            if (numericValue.HasValue)
            {
                return ToEnum(numericValue.Value);
            }
        }

        var value = Encoding.UTF8.GetString(reader.ValueSpan.ToArray());

        throw new JsonException($"Unable to deserialize {value}('{reader.TokenType}') into {typeof(T).Name}");
    }

    private TBackingType? ReadAsNumber(ref Utf8JsonReader reader) => BackingTypeTypeCode switch
    {
        TypeCode.SByte => reader.GetSByte() is var numericValue ? Unsafe.As<sbyte, TBackingType>(ref numericValue) : null,
        TypeCode.Byte => reader.GetByte() is var numericValue ? Unsafe.As<byte, TBackingType>(ref numericValue) : null,
        TypeCode.Int16 => reader.GetInt16() is var numericValue ? Unsafe.As<short, TBackingType>(ref numericValue) : null,
        TypeCode.UInt16 => reader.GetUInt16() is var numericValue ? Unsafe.As<ushort, TBackingType>(ref numericValue) : null,
        TypeCode.Int32 => reader.GetInt32() is var numericValue ? Unsafe.As<int, TBackingType>(ref numericValue) : null,
        TypeCode.UInt32 => reader.GetUInt32() is var numericValue ? Unsafe.As<uint, TBackingType>(ref numericValue) : null,
        TypeCode.Int64 => reader.GetInt64() is var numericValue ? Unsafe.As<long, TBackingType>(ref numericValue) : null,
        TypeCode.UInt64 => reader.GetUInt64() is var numericValue ? Unsafe.As<ulong, TBackingType>(ref numericValue) : null,
        _ => throw new ArgumentOutOfRangeException(nameof(BackingTypeTypeCode), BackingTypeTypeCode, $"Unexpected TypeCode {BackingTypeTypeCode}")
    };
}
