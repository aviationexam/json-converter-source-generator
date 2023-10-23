//HintName: EBackingEnumEnumJsonConverter.g.cs
#nullable enable

namespace ApplicationNamespace.Contracts;

internal class EBackingEnumEnumJsonConverter : Aviationexam.GeneratedJsonConverters.EnumJsonConvertor<ApplicationNamespace.Contracts.EBackingEnum, System.Int32>
{
    protected override System.TypeCode BackingTypeTypeCode => System.TypeCode.Int32;

    protected override Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy DeserializationStrategy => Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.UseBackingType;

    protected override Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy SerializationStrategy => Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy.BackingType;

    protected override bool TryToEnum(
        System.ReadOnlySpan<byte> enumName, out ApplicationNamespace.Contracts.EBackingEnum value
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support deserialization from enum name");

    protected override bool TryToEnum(
        System.Int32 numericValue, out ApplicationNamespace.Contracts.EBackingEnum value
    )
    {
        (var tryValue, value) = numericValue switch
        {
            0 => (true, ApplicationNamespace.Contracts.EBackingEnum.A),
            1 => (true, ApplicationNamespace.Contracts.EBackingEnum.B),
            _ => (false, default(ApplicationNamespace.Contracts.EBackingEnum)),
        };

        return tryValue;
    }

    protected override System.Int32 ToBackingType(
        ApplicationNamespace.Contracts.EBackingEnum value
    ) => value switch
    {
        ApplicationNamespace.Contracts.EBackingEnum.A => 0,
        ApplicationNamespace.Contracts.EBackingEnum.B => 1,
        _ => throw new System.Text.Json.JsonException($"Undefined mapping of '{value}' from enum 'ApplicationNamespace.Contracts.EBackingEnum'"),
    };

    protected override System.ReadOnlySpan<byte> ToFirstEnumName(
        ApplicationNamespace.Contracts.EBackingEnum value
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support serialization to enum type");
}
