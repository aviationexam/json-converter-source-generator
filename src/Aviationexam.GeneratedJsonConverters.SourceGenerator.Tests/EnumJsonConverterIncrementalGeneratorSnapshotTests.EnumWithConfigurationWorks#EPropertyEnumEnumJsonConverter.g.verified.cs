//HintName: EPropertyEnumEnumJsonConverter.g.cs
#nullable enable

namespace ApplicationNamespace.Contracts;

internal class EPropertyEnumEnumJsonConverter : Aviationexam.GeneratedJsonConverters.EnumJsonConvertor<ApplicationNamespace.Contracts.EPropertyEnum, System.Byte>
{
    protected override System.TypeCode BackingTypeTypeCode => System.TypeCode.Byte;

    protected override Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy DeserializationStrategy => Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.UseEnumName;

    protected override Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy SerializationStrategy => Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy.FirstEnumName;

    protected override T ToEnum(
        ReadOnlySpan<byte> enumName
    )
    {
        if (enumName.SequenceEqual("C"u8))
        {
            return ApplicationNamespace.Contracts.EPropertyEnum.C;
        }
        if (enumName.SequenceEqual("D"u8))
        {
            return ApplicationNamespace.Contracts.EPropertyEnum.D;
        }

        var stringValue = System.Text.Encoding.UTF8.GetString(enumName.ToArray());

        throw new System.Text.Json.JsonException($"Undefined mapping of '{stringValue}' to enum 'ApplicationNamespace.Contracts.EPropertyEnum'");
    }

    protected override T ToEnum(
        TBackingType numericValue
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support deserialization from backing type");
}
