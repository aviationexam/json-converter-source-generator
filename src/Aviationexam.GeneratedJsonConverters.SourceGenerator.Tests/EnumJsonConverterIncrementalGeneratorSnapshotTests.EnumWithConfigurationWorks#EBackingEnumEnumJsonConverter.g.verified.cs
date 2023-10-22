//HintName: EBackingEnumEnumJsonConverter.g.cs
#nullable enable

namespace ApplicationNamespace.Contracts;

internal class EBackingEnumEnumJsonConverter : Aviationexam.GeneratedJsonConverters.EnumJsonConvertor<ApplicationNamespace.Contracts.EBackingEnum, System.Int32>
{
    protected override System.TypeCode BackingTypeTypeCode => System.TypeCode.Int32;

    protected override Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy DeserializationStrategy => Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.UseBackingType;

    protected override Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy SerializationStrategy => Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy.BackingType;

    protected override ApplicationNamespace.Contracts.EBackingEnum ToEnum(
        System.ReadOnlySpan<byte> enumName
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support deserialization from enum name");

    protected override ApplicationNamespace.Contracts.EBackingEnum ToEnum(
        System.Int32 numericValue
    ) => numericValue switch
    {
        0 => ApplicationNamespace.Contracts.EBackingEnum.A,
        1 => ApplicationNamespace.Contracts.EBackingEnum.B,
        _ => throw new System.Text.Json.JsonException($"Undefined mapping of '{numericValue}' to enum 'ApplicationNamespace.Contracts.EBackingEnum'"),
    };
}
