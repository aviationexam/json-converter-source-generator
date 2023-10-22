//HintName: EPropertyWithBackingEnumEnumJsonConverter.g.cs
#nullable enable

namespace ApplicationNamespace.Contracts;

internal class EPropertyWithBackingEnumEnumJsonConverter : Aviationexam.GeneratedJsonConverters.EnumJsonConvertor<ApplicationNamespace.Contracts.EPropertyWithBackingEnum, System.Int32>
{
    protected override System.TypeCode BackingTypeTypeCode => System.TypeCode.Int32;

    protected override Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy DeserializationStrategy => Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.UseBackingType | Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.UseEnumName;

    protected override Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy SerializationStrategy => Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy.FirstEnumName;

    protected override ApplicationNamespace.Contracts.EPropertyWithBackingEnum ToEnum(
        System.ReadOnlySpan<byte> enumName
    )
    {
        if (System.MemoryExtensions.SequenceEqual(enumName, "E"u8))
        {
            return ApplicationNamespace.Contracts.EPropertyWithBackingEnum.E;
        }
        if (System.MemoryExtensions.SequenceEqual(enumName, "F"u8))
        {
            return ApplicationNamespace.Contracts.EPropertyWithBackingEnum.F;
        }

        var stringValue = System.Text.Encoding.UTF8.GetString(enumName.ToArray());

        throw new System.Text.Json.JsonException($"Undefined mapping of '{stringValue}' to enum 'ApplicationNamespace.Contracts.EPropertyWithBackingEnum'");
    }

    protected override ApplicationNamespace.Contracts.EPropertyWithBackingEnum ToEnum(
        System.Int32 numericValue
    ) => numericValue switch
    {
        0 => ApplicationNamespace.Contracts.EPropertyWithBackingEnum.E,
        1 => ApplicationNamespace.Contracts.EPropertyWithBackingEnum.F,
        _ => throw new System.Text.Json.JsonException($"Undefined mapping of '{numericValue}' to enum 'ApplicationNamespace.Contracts.EPropertyWithBackingEnum'"),
    };
}
