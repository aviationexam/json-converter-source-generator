//HintName: EFlagsEnumEnumJsonConverter.g.cs
#nullable enable

namespace ApplicationNamespace.Contracts;

internal class EFlagsEnumEnumJsonConverter : Aviationexam.GeneratedJsonConverters.EnumJsonConvertor<ApplicationNamespace.Contracts.EFlagsEnum, System.Int32>
{
    protected override System.TypeCode BackingTypeTypeCode => System.TypeCode.Int32;

    protected override Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy DeserializationStrategy => Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.UseEnumName;

     protected override Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy SerializationStrategy => Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy.FirstEnumName | Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy.FlagsArray;

    public override bool TryToEnum(
        System.ReadOnlySpan<byte> enumName, out ApplicationNamespace.Contracts.EFlagsEnum value
    )
    {
        if (System.MemoryExtensions.SequenceEqual(enumName, "None"u8))
        {
            value = ApplicationNamespace.Contracts.EFlagsEnum.None;
            return true;
        }
        if (System.MemoryExtensions.SequenceEqual(enumName, "Read"u8))
        {
            value = ApplicationNamespace.Contracts.EFlagsEnum.Read;
            return true;
        }
        if (System.MemoryExtensions.SequenceEqual(enumName, "Write"u8))
        {
            value = ApplicationNamespace.Contracts.EFlagsEnum.Write;
            return true;
        }
        if (System.MemoryExtensions.SequenceEqual(enumName, "Execute"u8))
        {
            value = ApplicationNamespace.Contracts.EFlagsEnum.Execute;
            return true;
        }

        value = default(ApplicationNamespace.Contracts.EFlagsEnum);
        return false;
    }

    public override bool TryToEnum(
        System.Int32 numericValue, out ApplicationNamespace.Contracts.EFlagsEnum value
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support deserialization from backing type");

    public override System.Int32 ToBackingType(
        ApplicationNamespace.Contracts.EFlagsEnum value
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support serialization to backing type");

    public override System.ReadOnlySpan<byte> ToFirstEnumName(
        ApplicationNamespace.Contracts.EFlagsEnum value
    ) => value switch
    {
        ApplicationNamespace.Contracts.EFlagsEnum.None => "None"u8,
        ApplicationNamespace.Contracts.EFlagsEnum.Read => "Read"u8,
        ApplicationNamespace.Contracts.EFlagsEnum.Write => "Write"u8,
        ApplicationNamespace.Contracts.EFlagsEnum.Execute => "Execute"u8,
        _ => throw new System.Text.Json.JsonException($"Undefined mapping of '{value}' from enum 'ApplicationNamespace.Contracts.EFlagsEnum'"),
    };

    protected override void WriteFlagsAsArray(
        System.Text.Json.Utf8JsonWriter writer,
        ApplicationNamespace.Contracts.EFlagsEnum value,
        System.Text.Json.JsonSerializerOptions options
    )
    {
        if (value == default)
        {
            writer.WriteStartArray();
            writer.WriteStringValue("None"u8);
            writer.WriteEndArray();
            return;
        }
        writer.WriteStartArray();
        if (value.HasFlag(ApplicationNamespace.Contracts.EFlagsEnum.Read))
        {
            writer.WriteStringValue("Read"u8);
        }
        if (value.HasFlag(ApplicationNamespace.Contracts.EFlagsEnum.Write))
        {
            writer.WriteStringValue("Write"u8);
        }
        if (value.HasFlag(ApplicationNamespace.Contracts.EFlagsEnum.Execute))
        {
            writer.WriteStringValue("Execute"u8);
        }
        writer.WriteEndArray();
    }
}