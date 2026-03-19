//HintName: ENonFlagsEnumEnumJsonConverter.g.cs
#nullable enable

namespace ApplicationNamespace.Contracts;

internal class ENonFlagsEnumEnumJsonConverter : Aviationexam.GeneratedJsonConverters.EnumJsonConvertor<ApplicationNamespace.Contracts.ENonFlagsEnum, System.Int32>
{
    protected override System.TypeCode BackingTypeTypeCode => System.TypeCode.Int32;

    protected override Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy DeserializationStrategy => Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.UseEnumName;

     protected override Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy SerializationStrategy => Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy.FirstEnumName;

    public override bool TryToEnum(
        System.ReadOnlySpan<byte> enumName, out ApplicationNamespace.Contracts.ENonFlagsEnum value
    )
    {
        if (System.MemoryExtensions.SequenceEqual(enumName, "A"u8))
        {
            value = ApplicationNamespace.Contracts.ENonFlagsEnum.A;
            return true;
        }
        if (System.MemoryExtensions.SequenceEqual(enumName, "B"u8))
        {
            value = ApplicationNamespace.Contracts.ENonFlagsEnum.B;
            return true;
        }

        value = default(ApplicationNamespace.Contracts.ENonFlagsEnum);
        return false;
    }

    public override bool TryToEnum(
        System.Int32 numericValue, out ApplicationNamespace.Contracts.ENonFlagsEnum value
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support deserialization from backing type");

    public override System.Int32 ToBackingType(
        ApplicationNamespace.Contracts.ENonFlagsEnum value
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support serialization to backing type");

    public override System.ReadOnlySpan<byte> ToFirstEnumName(
        ApplicationNamespace.Contracts.ENonFlagsEnum value
    ) => value switch
    {
        ApplicationNamespace.Contracts.ENonFlagsEnum.A => "A"u8,
        ApplicationNamespace.Contracts.ENonFlagsEnum.B => "B"u8,
        _ => throw new System.Text.Json.JsonException($"Undefined mapping of '{value}' from enum 'ApplicationNamespace.Contracts.ENonFlagsEnum'"),
    };

    protected override void WriteFlagsAsArray(
        System.Text.Json.Utf8JsonWriter writer,
        ApplicationNamespace.Contracts.ENonFlagsEnum value,
        System.Text.Json.JsonSerializerOptions options
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support serialization to flags array");
}