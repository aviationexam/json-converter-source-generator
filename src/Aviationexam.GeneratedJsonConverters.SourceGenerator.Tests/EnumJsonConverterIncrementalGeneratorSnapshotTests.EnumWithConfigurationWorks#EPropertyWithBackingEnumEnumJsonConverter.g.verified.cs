//HintName: EPropertyWithBackingEnumEnumJsonConverter.g.cs
#nullable enable

namespace ApplicationNamespace.Contracts;

internal class EPropertyWithBackingEnumEnumJsonConverter : Aviationexam.GeneratedJsonConverters.EnumJsonConvertor<ApplicationNamespace.Contracts.EPropertyWithBackingEnum, System.Int32>
{
    protected override System.TypeCode BackingTypeTypeCode => System.TypeCode.Int32;

    protected override Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy DeserializationStrategy => Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.UseBackingType | Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.UseEnumName;

    protected override Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy SerializationStrategy => Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy.FirstEnumName;

    public override bool TryToEnum(
        System.ReadOnlySpan<byte> enumName, out ApplicationNamespace.Contracts.EPropertyWithBackingEnum value
    )
    {
        if (System.MemoryExtensions.SequenceEqual(enumName, "E"u8))
        {
            value = ApplicationNamespace.Contracts.EPropertyWithBackingEnum.E;
            return true;
        }
        if (System.MemoryExtensions.SequenceEqual(enumName, "F"u8))
        {
            value = ApplicationNamespace.Contracts.EPropertyWithBackingEnum.F;
            return true;
        }

        value = default(ApplicationNamespace.Contracts.EPropertyWithBackingEnum);
        return false;
    }

    public override bool TryToEnum(
        System.Int32 numericValue, out ApplicationNamespace.Contracts.EPropertyWithBackingEnum value
    )
    {
        (var tryValue, value) = numericValue switch
        {
            0 => (true, ApplicationNamespace.Contracts.EPropertyWithBackingEnum.E),
            1 => (true, ApplicationNamespace.Contracts.EPropertyWithBackingEnum.F),
            _ => (false, default(ApplicationNamespace.Contracts.EPropertyWithBackingEnum)),
        };

        return tryValue;
    }

    public override System.Int32 ToBackingType(
        ApplicationNamespace.Contracts.EPropertyWithBackingEnum value
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support serialization to backing type");

    public override System.ReadOnlySpan<byte> ToFirstEnumName(
        ApplicationNamespace.Contracts.EPropertyWithBackingEnum value
    ) => value switch
    {
        ApplicationNamespace.Contracts.EPropertyWithBackingEnum.E => "E"u8,
        ApplicationNamespace.Contracts.EPropertyWithBackingEnum.F => "F"u8,
        _ => throw new System.Text.Json.JsonException($"Undefined mapping of '{value}' from enum 'ApplicationNamespace.Contracts.EPropertyWithBackingEnum'"),
    };
}
