// ReSharper disable once RedundantNullableDirective

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aviationexam.GeneratedJsonConverters;

internal abstract class EnumJsonConvertor<T> : JsonConverter<T>
    where T : struct, Enum
{
    protected abstract TypeCode BackingTypeTypeCode { get; }

    protected abstract EnumDeserializationStrategy DeserializationStrategy { get; }

    protected abstract EnumSerializationStrategy SerializationStrategy { get; }

    public abstract bool TryToEnum(ReadOnlySpan<byte> enumName, out T value);

    public abstract ReadOnlySpan<byte> ToFirstEnumName(T value);

    protected abstract void WriteFlagsAsArray(Utf8JsonWriter writer, T value, JsonSerializerOptions options);
}

internal abstract class EnumJsonConvertor<T, TBackingType> : EnumJsonConvertor<T>
    where T : struct, Enum
    where TBackingType : struct
{
    protected override void WriteFlagsAsArray(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        throw new NotSupportedException($"Writing as '{EnumSerializationStrategy.FlagsArray}' is not supported by {GetType().Name}");
    }

    public abstract bool TryToEnum(TBackingType numericValue, out T value);

    public abstract TBackingType ToBackingType(T value);

    public override T Read(
        ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options
    )
    {
        if (reader.TokenType is JsonTokenType.StartArray)
        {
            return ReadFlagsFromArray(ref reader, typeToConvert, options);
        }

        if (
            reader.TokenType is JsonTokenType.String
            && DeserializationStrategy.HasFlag(EnumDeserializationStrategy.UseEnumName)
        )
        {
            var enumName = reader.ValueSpan;

            if (TryToEnum(enumName, out var enumValue))
            {
                return enumValue;
            }

            var stringValue = Encoding.UTF8.GetString(enumName.ToArray());

            throw new JsonException($"Undefined mapping of '{stringValue}' to enum '{typeof(T).FullName}'");
        }

        if (reader.TokenType is JsonTokenType.Number)
        {
            var numericValue = ReadAsNumber(ref reader);

            if (numericValue.HasValue)
            {
                if (TryToEnum(numericValue.Value, out var enumValue))
                {
                    return enumValue;
                }

                throw new JsonException($"Undefined mapping of '{numericValue}' to enum '{{enumFullName}}'");
            }
        }

        var value = Encoding.UTF8.GetString(reader.ValueSpan.ToArray());

        throw new JsonException($"Unable to deserialize {value}('{reader.TokenType}') into {typeof(T).Name}");
    }

    protected T ReadFlagsFromArray(
        ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options
    )
    {
        T result = default;
        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            T element;
            if (
                reader.TokenType is JsonTokenType.String
                && DeserializationStrategy.HasFlag(EnumDeserializationStrategy.UseEnumName)
            )
            {
                if (!TryToEnum(reader.ValueSpan, out element))
                {
                    var stringValue = Encoding.UTF8.GetString(reader.ValueSpan.ToArray());
                    throw new JsonException($"Undefined mapping of '{stringValue}' to enum '{typeof(T).FullName}'");
                }
            }
            else if (
                reader.TokenType is JsonTokenType.Number
                && DeserializationStrategy.HasFlag(EnumDeserializationStrategy.UseBackingType)
            )
            {
                var numericValue = ReadAsNumber(ref reader);
                if (!numericValue.HasValue || !TryToEnum(numericValue.Value, out element))
                {
                    throw new JsonException($"Unable to deserialize array element into {typeof(T).Name}");
                }
            }
            else
            {
                throw new JsonException($"Unexpected token type '{reader.TokenType}' in flags array for {typeof(T).Name}");
            }

            result = BitwiseOr(result, element);
        }

        return result;
    }

    public override T ReadAsPropertyName(
        ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options
    )
    {
        if (
            reader.TokenType is JsonTokenType.PropertyName
            && DeserializationStrategy.HasFlag(EnumDeserializationStrategy.UseEnumName)
        )
        {
            var enumName = reader.ValueSpan;

            if (TryToEnum(enumName, out var enumValue))
            {
                return enumValue;
            }
        }

        var value = Encoding.UTF8.GetString(reader.ValueSpan.ToArray());

        if (
            reader.TokenType is JsonTokenType.PropertyName
            && DeserializationStrategy.HasFlag(EnumDeserializationStrategy.UseBackingType)
        )
        {
            var numericValue = ParseAsNumber(value);

            if (numericValue.HasValue)
            {
                if (TryToEnum(numericValue.Value, out var enumValue))
                {
                    return enumValue;
                }
            }
        }

        throw new JsonException($"Unable to deserialize {value}('{reader.TokenType}') into {typeof(T).Name}");
    }

    protected TBackingType? ReadAsNumber(ref Utf8JsonReader reader) => BackingTypeTypeCode switch
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

    private T BitwiseOr(T left, T right)
    {
        switch (BackingTypeTypeCode)
        {
            case TypeCode.SByte:
                var sbyteLeft = Unsafe.As<T, sbyte>(ref left);
                var sbyteRight = Unsafe.As<T, sbyte>(ref right);
                var sbyteResult = (sbyte) (sbyteLeft | sbyteRight);
                return Unsafe.As<sbyte, T>(ref sbyteResult);
            case TypeCode.Byte:
                var byteLeft = Unsafe.As<T, byte>(ref left);
                var byteRight = Unsafe.As<T, byte>(ref right);
                var byteResult = (byte) (byteLeft | byteRight);
                return Unsafe.As<byte, T>(ref byteResult);
            case TypeCode.Int16:
                var int16Left = Unsafe.As<T, short>(ref left);
                var int16Right = Unsafe.As<T, short>(ref right);
                var int16Result = (short) (int16Left | int16Right);
                return Unsafe.As<short, T>(ref int16Result);
            case TypeCode.UInt16:
                var uint16Left = Unsafe.As<T, ushort>(ref left);
                var uint16Right = Unsafe.As<T, ushort>(ref right);
                var uint16Result = (ushort) (uint16Left | uint16Right);
                return Unsafe.As<ushort, T>(ref uint16Result);
            case TypeCode.Int32:
                var int32Left = Unsafe.As<T, int>(ref left);
                var int32Right = Unsafe.As<T, int>(ref right);
                var int32Result = int32Left | int32Right;
                return Unsafe.As<int, T>(ref int32Result);
            case TypeCode.UInt32:
                var uint32Left = Unsafe.As<T, uint>(ref left);
                var uint32Right = Unsafe.As<T, uint>(ref right);
                var uint32Result = uint32Left | uint32Right;
                return Unsafe.As<uint, T>(ref uint32Result);
            case TypeCode.Int64:
                var int64Left = Unsafe.As<T, long>(ref left);
                var int64Right = Unsafe.As<T, long>(ref right);
                var int64Result = int64Left | int64Right;
                return Unsafe.As<long, T>(ref int64Result);
            case TypeCode.UInt64:
                var uint64Left = Unsafe.As<T, ulong>(ref left);
                var uint64Right = Unsafe.As<T, ulong>(ref right);
                var uint64Result = uint64Left | uint64Right;
                return Unsafe.As<ulong, T>(ref uint64Result);
            default:
                throw new ArgumentOutOfRangeException(nameof(BackingTypeTypeCode), BackingTypeTypeCode, $"Unexpected TypeCode {BackingTypeTypeCode}");
        }
    }

    private bool HasMultipleFlags(T value)
    {
        var numericValue = ToUInt64(value);
        return numericValue != 0 && (numericValue & (numericValue - 1)) != 0;
    }

    private ulong ToUInt64(T value) => BackingTypeTypeCode switch
    {
        TypeCode.SByte => unchecked((ulong) Unsafe.As<T, sbyte>(ref value)),
        TypeCode.Byte => Unsafe.As<T, byte>(ref value),
        TypeCode.Int16 => unchecked((ulong) Unsafe.As<T, short>(ref value)),
        TypeCode.UInt16 => Unsafe.As<T, ushort>(ref value),
        TypeCode.Int32 => unchecked((ulong) Unsafe.As<T, int>(ref value)),
        TypeCode.UInt32 => Unsafe.As<T, uint>(ref value),
        TypeCode.Int64 => unchecked((ulong) Unsafe.As<T, long>(ref value)),
        TypeCode.UInt64 => Unsafe.As<T, ulong>(ref value),
        _ => throw new ArgumentOutOfRangeException(nameof(BackingTypeTypeCode), BackingTypeTypeCode, $"Unexpected TypeCode {BackingTypeTypeCode}")
    };

    private TBackingType? ParseAsNumber(
        string value
    ) => BackingTypeTypeCode switch
    {
        TypeCode.SByte => sbyte.TryParse(value, out var numericValue) ? Unsafe.As<sbyte, TBackingType>(ref numericValue) : null,
        TypeCode.Byte => byte.TryParse(value, out var numericValue) ? Unsafe.As<byte, TBackingType>(ref numericValue) : null,
        TypeCode.Int16 => short.TryParse(value, out var numericValue) ? Unsafe.As<short, TBackingType>(ref numericValue) : null,
        TypeCode.UInt16 => ushort.TryParse(value, out var numericValue) ? Unsafe.As<ushort, TBackingType>(ref numericValue) : null,
        TypeCode.Int32 => int.TryParse(value, out var numericValue) ? Unsafe.As<int, TBackingType>(ref numericValue) : null,
        TypeCode.UInt32 => uint.TryParse(value, out var numericValue) ? Unsafe.As<uint, TBackingType>(ref numericValue) : null,
        TypeCode.Int64 => long.TryParse(value, out var numericValue) ? Unsafe.As<long, TBackingType>(ref numericValue) : null,
        TypeCode.UInt64 => ulong.TryParse(value, out var numericValue) ? Unsafe.As<ulong, TBackingType>(ref numericValue) : null,
        _ => throw new ArgumentOutOfRangeException(nameof(BackingTypeTypeCode), BackingTypeTypeCode, $"Unexpected TypeCode {BackingTypeTypeCode}")
    };

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (SerializationStrategy.HasFlag(EnumSerializationStrategy.FlagsArray))
        {
            WriteFlagsAsArray(writer, value, options);
        }
        else if (SerializationStrategy.HasFlag(EnumSerializationStrategy.BackingType))
        {
            WriteAsBackingType(writer, value, options);
        }
        else if (SerializationStrategy.HasFlag(EnumSerializationStrategy.FirstEnumName))
        {
            WriteAsFirstEnumName(writer, value, options);
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(SerializationStrategy), SerializationStrategy, "Unknown serialization strategy");
        }
    }

    public override void WriteAsPropertyName(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (SerializationStrategy.HasFlag(EnumSerializationStrategy.FlagsArray))
        {
            if (HasMultipleFlags(value))
            {
                throw new JsonException($"Flags enum '{typeof(T).FullName}' value '{value}' contains multiple flags and cannot be used as a JSON property name");
            }

            if (SerializationStrategy.HasFlag(EnumSerializationStrategy.BackingType))
            {
                WriteAsPropertyNameAsBackingType(writer, value, options);
            }
            else if (SerializationStrategy.HasFlag(EnumSerializationStrategy.FirstEnumName))
            {
                WriteAsPropertyNameAsFirstEnumName(writer, value, options);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(SerializationStrategy), SerializationStrategy, "Unknown serialization strategy");
            }
        }
        else if (SerializationStrategy.HasFlag(EnumSerializationStrategy.BackingType))
        {
            WriteAsPropertyNameAsBackingType(writer, value, options);
        }
        else if (SerializationStrategy.HasFlag(EnumSerializationStrategy.FirstEnumName))
        {
            WriteAsPropertyNameAsFirstEnumName(writer, value, options);
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(SerializationStrategy), SerializationStrategy, "Unknown serialization strategy");
        }
    }

    private void WriteAsBackingType(
        Utf8JsonWriter writer,
        T value,
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        JsonSerializerOptions options
    )
    {
        var numericValue = ToBackingType(value);

        switch (BackingTypeTypeCode)
        {
            case TypeCode.SByte:
                writer.WriteNumberValue(Unsafe.As<TBackingType, sbyte>(ref numericValue));
                break;
            case TypeCode.Byte:
                writer.WriteNumberValue(Unsafe.As<TBackingType, byte>(ref numericValue));
                break;
            case TypeCode.Int16:
                writer.WriteNumberValue(Unsafe.As<TBackingType, short>(ref numericValue));
                break;
            case TypeCode.UInt16:
                writer.WriteNumberValue(Unsafe.As<TBackingType, ushort>(ref numericValue));
                break;
            case TypeCode.Int32:
                writer.WriteNumberValue(Unsafe.As<TBackingType, int>(ref numericValue));
                break;
            case TypeCode.UInt32:
                writer.WriteNumberValue(Unsafe.As<TBackingType, uint>(ref numericValue));
                break;
            case TypeCode.Int64:
                writer.WriteNumberValue(Unsafe.As<TBackingType, long>(ref numericValue));
                break;
            case TypeCode.UInt64:
                writer.WriteNumberValue(Unsafe.As<TBackingType, ulong>(ref numericValue));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(BackingTypeTypeCode), BackingTypeTypeCode, $"Unexpected TypeCode {BackingTypeTypeCode}");
        }
    }

    private void WriteAsPropertyNameAsBackingType(
        Utf8JsonWriter writer,
        T value,
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        JsonSerializerOptions options
    )
    {
        var numericValue = ToBackingType(value);

        writer.WritePropertyName($"{numericValue}");
    }

    private void WriteAsFirstEnumName(
        Utf8JsonWriter writer,
        T value,
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        JsonSerializerOptions options
    )
    {
        var enumValue = ToFirstEnumName(value);

        writer.WriteStringValue(enumValue);
    }

    private void WriteAsPropertyNameAsFirstEnumName(
        Utf8JsonWriter writer,
        T value,
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        JsonSerializerOptions options
    )
    {
        var enumValue = ToFirstEnumName(value);

        writer.WritePropertyName(enumValue);
    }
}
