//HintName: EMyEnumEnumJsonConverter.g.cs
#nullable enable

namespace ApplicationNamespace.Contracts;

internal class EMyEnumEnumJsonConverter : Aviationexam.GeneratedJsonConverters.EnumJsonConvertor<ApplicationNamespace.Contracts.EMyEnum, System.Int32>
{
    protected override System.TypeCode BackingTypeTypeCode => System.TypeCode.Int32;

    protected override Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy DeserializationStrategy => Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.UseEnumName;

    protected override Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy SerializationStrategy => Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy.FirstEnumName;

    protected override ApplicationNamespace.Contracts.EMyEnum ToEnum(
        System.ReadOnlySpan<byte> enumName
    )
    {
        if (System.MemoryExtensions.SequenceEqual(enumName, "C"u8))
        {
            return ApplicationNamespace.Contracts.EMyEnum.A;
        }
        if (System.MemoryExtensions.SequenceEqual(enumName, "D"u8))
        {
            return ApplicationNamespace.Contracts.EMyEnum.B;
        }

        var stringValue = System.Text.Encoding.UTF8.GetString(enumName.ToArray());

        throw new System.Text.Json.JsonException($"Undefined mapping of '{stringValue}' to enum 'ApplicationNamespace.Contracts.EMyEnum'");
    }

    protected override ApplicationNamespace.Contracts.EMyEnum ToEnum(
        System.Int32 numericValue
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support deserialization from backing type");

    protected override System.Int32 ToBackingType(
        ApplicationNamespace.Contracts.EMyEnum value
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support serialization to backing type");

    protected override System.ReadOnlySpan<byte> ToFirstEnumName(
        ApplicationNamespace.Contracts.EMyEnum value
    ) => value switch
    {
        ApplicationNamespace.Contracts.EMyEnum.A => "C"u8,
        ApplicationNamespace.Contracts.EMyEnum.B => "D"u8,
        _ => throw new System.Text.Json.JsonException($"Undefined mapping of '{value}' from enum 'ApplicationNamespace.Contracts.EMyEnum'"),
    };
}
