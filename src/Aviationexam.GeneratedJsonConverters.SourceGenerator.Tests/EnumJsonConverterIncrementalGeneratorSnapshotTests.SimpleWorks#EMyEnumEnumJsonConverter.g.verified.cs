//HintName: EMyEnumEnumJsonConverter.g.cs
#nullable enable

namespace ApplicationNamespace.Contracts;

internal class EMyEnumEnumJsonConverter : Aviationexam.GeneratedJsonConverters.EnumJsonConvertor<ApplicationNamespace.Contracts.EMyEnum, System.Int32>
{
    protected override System.TypeCode BackingTypeTypeCode => System.TypeCode.Int32;

    protected override Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy DeserializationStrategy => Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.UseEnumName;

    protected override Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy SerializationStrategy => Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy.FirstEnumName;

    protected override T ToEnum(
        ReadOnlySpan<byte> enumName
    )
    {
        if (enumName.SequenceEqual("A"u8))
        {
            return ApplicationNamespace.Contracts.EMyEnum.A;
        }
        if (enumName.SequenceEqual("B"u8))
        {
            return ApplicationNamespace.Contracts.EMyEnum.B;
        }

        var stringValue = System.Text.Encoding.UTF8.GetString(enumName.ToArray());

        throw new System.Text.Json.JsonException($"Undefined mapping of '{stringValue}' to enum 'ApplicationNamespace.Contracts.EMyEnum'");
    }

    protected override T ToEnum(
        TBackingType numericValue
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support deserialization from backing type");
}
