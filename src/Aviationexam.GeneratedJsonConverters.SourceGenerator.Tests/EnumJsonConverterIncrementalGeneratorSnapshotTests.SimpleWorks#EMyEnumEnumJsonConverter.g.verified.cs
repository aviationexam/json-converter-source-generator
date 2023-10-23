//HintName: EMyEnumEnumJsonConverter.g.cs
#nullable enable

namespace ApplicationNamespace.Contracts;

internal class EMyEnumEnumJsonConverter : Aviationexam.GeneratedJsonConverters.EnumJsonConvertor<ApplicationNamespace.Contracts.EMyEnum, System.Int32>
{
    protected override System.TypeCode BackingTypeTypeCode => System.TypeCode.Int32;

    protected override Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy DeserializationStrategy => Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.UseEnumName;

    protected override Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy SerializationStrategy => Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy.FirstEnumName;

    protected override bool TryToEnum(
        System.ReadOnlySpan<byte> enumName, out ApplicationNamespace.Contracts.EMyEnum value
    )
    {
        if (System.MemoryExtensions.SequenceEqual(enumName, "A"u8))
        {
            value = ApplicationNamespace.Contracts.EMyEnum.A;
            return true;
        }
        if (System.MemoryExtensions.SequenceEqual(enumName, "B"u8))
        {
            value = ApplicationNamespace.Contracts.EMyEnum.B;
            return true;
        }

        value = default(ApplicationNamespace.Contracts.EMyEnum);
        return false;
    }

    protected override bool TryToEnum(
        System.Int32 numericValue, out ApplicationNamespace.Contracts.EMyEnum value
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support deserialization from backing type");

    protected override System.Int32 ToBackingType(
        ApplicationNamespace.Contracts.EMyEnum value
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support serialization to backing type");

    protected override System.ReadOnlySpan<byte> ToFirstEnumName(
        ApplicationNamespace.Contracts.EMyEnum value
    ) => value switch
    {
        ApplicationNamespace.Contracts.EMyEnum.A => "A"u8,
        ApplicationNamespace.Contracts.EMyEnum.B => "B"u8,
        _ => throw new System.Text.Json.JsonException($"Undefined mapping of '{value}' from enum 'ApplicationNamespace.Contracts.EMyEnum'"),
    };
}
