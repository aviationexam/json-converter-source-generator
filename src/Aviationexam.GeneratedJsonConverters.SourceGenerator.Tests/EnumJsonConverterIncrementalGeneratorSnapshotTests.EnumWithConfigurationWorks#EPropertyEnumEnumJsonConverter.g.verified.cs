//HintName: EPropertyEnumEnumJsonConverter.g.cs
#nullable enable

namespace ApplicationNamespace.Contracts;

internal class EPropertyEnumEnumJsonConverter : Aviationexam.GeneratedJsonConverters.EnumJsonConvertor<ApplicationNamespace.Contracts.EPropertyEnum, System.Byte>
{
    protected override System.TypeCode BackingTypeTypeCode => System.TypeCode.Byte;

    protected override Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy DeserializationStrategy => Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.UseEnumName;

    protected override Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy SerializationStrategy => Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy.FirstEnumName;

    protected override bool TryToEnum(
        System.ReadOnlySpan<byte> enumName, out ApplicationNamespace.Contracts.EPropertyEnum value
    )
    {
        if (System.MemoryExtensions.SequenceEqual(enumName, "C"u8))
        {
            value = ApplicationNamespace.Contracts.EPropertyEnum.C;
            return true;
        }
        if (System.MemoryExtensions.SequenceEqual(enumName, "D"u8))
        {
            value = ApplicationNamespace.Contracts.EPropertyEnum.D;
            return true;
        }

        value = default(ApplicationNamespace.Contracts.EPropertyEnum);
        return false;
    }

    protected override bool TryToEnum(
        System.Byte numericValue, out ApplicationNamespace.Contracts.EPropertyEnum value
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support deserialization from backing type");

    protected override System.Byte ToBackingType(
        ApplicationNamespace.Contracts.EPropertyEnum value
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support serialization to backing type");

    protected override System.ReadOnlySpan<byte> ToFirstEnumName(
        ApplicationNamespace.Contracts.EPropertyEnum value
    ) => value switch
    {
        ApplicationNamespace.Contracts.EPropertyEnum.C => "C"u8,
        ApplicationNamespace.Contracts.EPropertyEnum.D => "D"u8,
        _ => throw new System.Text.Json.JsonException($"Undefined mapping of '{value}' from enum 'ApplicationNamespace.Contracts.EPropertyEnum'"),
    };
}
