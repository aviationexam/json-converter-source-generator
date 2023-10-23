//HintName: EPropertyEnumEnumJsonConverter.g.cs
#nullable enable

namespace ApplicationNamespace.Contracts;

internal class EPropertyEnumEnumJsonConverter : Aviationexam.GeneratedJsonConverters.EnumJsonConvertor<ApplicationNamespace.Contracts.EPropertyEnum, System.Byte>
{
    protected override System.TypeCode BackingTypeTypeCode => System.TypeCode.Byte;

    protected override Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy DeserializationStrategy => Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.UseEnumName;

    protected override Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy SerializationStrategy => Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy.FirstEnumName;

    protected override ApplicationNamespace.Contracts.EPropertyEnum ToEnum(
        System.ReadOnlySpan<byte> enumName
    )
    {
        if (System.MemoryExtensions.SequenceEqual(enumName, "C"u8))
        {
            return ApplicationNamespace.Contracts.EPropertyEnum.C;
        }
        if (System.MemoryExtensions.SequenceEqual(enumName, "D"u8))
        {
            return ApplicationNamespace.Contracts.EPropertyEnum.D;
        }

        var stringValue = System.Text.Encoding.UTF8.GetString(enumName.ToArray());

        throw new System.Text.Json.JsonException($"Undefined mapping of '{stringValue}' to enum 'ApplicationNamespace.Contracts.EPropertyEnum'");
    }

    protected override ApplicationNamespace.Contracts.EPropertyEnum ToEnum(
        System.Byte numericValue
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
