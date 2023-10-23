//HintName: EMyEnumEnumJsonConverter.g.cs
#nullable enable

namespace ApplicationNamespace.Contracts;

internal class EMyEnumEnumJsonConverter : Aviationexam.GeneratedJsonConverters.EnumJsonConvertor<ApplicationNamespace.Contracts.EMyEnum, System.Int32>
{
    protected override System.TypeCode BackingTypeTypeCode => System.TypeCode.Int32;

    protected override Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy DeserializationStrategy => Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.UseBackingType | Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.UseEnumName;

    protected override Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy SerializationStrategy => Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy.BackingType;

    protected override bool TryToEnum(
        System.ReadOnlySpan<byte> enumName, out ApplicationNamespace.Contracts.EMyEnum value
    )
    {
        if (System.MemoryExtensions.SequenceEqual(enumName, "C"u8))
        {
            value = ApplicationNamespace.Contracts.EMyEnum.A;
            return true;
        }
        if (System.MemoryExtensions.SequenceEqual(enumName, "D"u8))
        {
            value = ApplicationNamespace.Contracts.EMyEnum.B;
            return true;
        }
        if (System.MemoryExtensions.SequenceEqual(enumName, "E"u8))
        {
            value = ApplicationNamespace.Contracts.EMyEnum.C;
            return true;
        }

        value = default(ApplicationNamespace.Contracts.EMyEnum);
        return false;
    }

    protected override bool TryToEnum(
        System.Int32 numericValue, out ApplicationNamespace.Contracts.EMyEnum value
    )
    {
        (var tryValue, value) = numericValue switch
        {
            0 => (true, ApplicationNamespace.Contracts.EMyEnum.A),
            1 => (true, ApplicationNamespace.Contracts.EMyEnum.B),
            _ => (false, default(ApplicationNamespace.Contracts.EMyEnum)),
        };

        return tryValue;
    }

    protected override System.Int32 ToBackingType(
        ApplicationNamespace.Contracts.EMyEnum value
    ) => value switch
    {
        ApplicationNamespace.Contracts.EMyEnum.A => 0,
        ApplicationNamespace.Contracts.EMyEnum.B => 1,
        _ => throw new System.Text.Json.JsonException($"Undefined mapping of '{value}' from enum 'ApplicationNamespace.Contracts.EMyEnum'"),
    };

    protected override System.ReadOnlySpan<byte> ToFirstEnumName(
        ApplicationNamespace.Contracts.EMyEnum value
    ) => throw new System.Text.Json.JsonException("Enum is not configured to support serialization to enum type");
}
