//HintName: EFlagsBackingEnumEnumJsonConverter.g.cs
#nullable enable

namespace ApplicationNamespace.Contracts;

internal class EFlagsBackingEnumEnumJsonConverter : Aviationexam.GeneratedJsonConverters.EnumJsonConvertor<ApplicationNamespace.Contracts.EFlagsBackingEnum, System.Int32>
{
    protected override System.TypeCode BackingTypeTypeCode => System.TypeCode.Int32;

    protected override Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy DeserializationStrategy => Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.UseBackingType;

     protected override Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy SerializationStrategy => Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy.BackingType | Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy.FlagsArray;

    public override bool TryToEnum(
        System.ReadOnlySpan<byte> enumName, out ApplicationNamespace.Contracts.EFlagsBackingEnum value
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support deserialization from enum name");

    public override bool TryToEnum(
        System.Int32 numericValue, out ApplicationNamespace.Contracts.EFlagsBackingEnum value
    )
    {
        (var tryValue, value) = numericValue switch
        {
            0 => (true, ApplicationNamespace.Contracts.EFlagsBackingEnum.None),
            1 => (true, ApplicationNamespace.Contracts.EFlagsBackingEnum.Read),
            2 => (true, ApplicationNamespace.Contracts.EFlagsBackingEnum.Write),
            4 => (true, ApplicationNamespace.Contracts.EFlagsBackingEnum.Execute),
            _ => (false, default(ApplicationNamespace.Contracts.EFlagsBackingEnum)),
        };

        return tryValue;
    }

    public override System.Int32 ToBackingType(
        ApplicationNamespace.Contracts.EFlagsBackingEnum value
    ) => value switch
    {
        ApplicationNamespace.Contracts.EFlagsBackingEnum.None => 0,
        ApplicationNamespace.Contracts.EFlagsBackingEnum.Read => 1,
        ApplicationNamespace.Contracts.EFlagsBackingEnum.Write => 2,
        ApplicationNamespace.Contracts.EFlagsBackingEnum.Execute => 4,
        _ => throw new System.Text.Json.JsonException($"Undefined mapping of '{value}' from enum 'ApplicationNamespace.Contracts.EFlagsBackingEnum'"),
    };

    public override System.ReadOnlySpan<byte> ToFirstEnumName(
        ApplicationNamespace.Contracts.EFlagsBackingEnum value
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support serialization to enum type");

    protected override void WriteFlagsAsArray(
        System.Text.Json.Utf8JsonWriter writer,
        ApplicationNamespace.Contracts.EFlagsBackingEnum value,
        System.Text.Json.JsonSerializerOptions options
    )
    {
        if (value == default)
        {
            writer.WriteStartArray();
            writer.WriteNumberValue((System.Int32)0);
            writer.WriteEndArray();
            return;
        }
        writer.WriteStartArray();
        if (value.HasFlag(ApplicationNamespace.Contracts.EFlagsBackingEnum.Read))
        {
            writer.WriteNumberValue((System.Int32)1);
        }
        if (value.HasFlag(ApplicationNamespace.Contracts.EFlagsBackingEnum.Write))
        {
            writer.WriteNumberValue((System.Int32)2);
        }
        if (value.HasFlag(ApplicationNamespace.Contracts.EFlagsBackingEnum.Execute))
        {
            writer.WriteNumberValue((System.Int32)4);
        }
        writer.WriteEndArray();
    }
}