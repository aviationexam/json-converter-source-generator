namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

internal sealed record EnumJsonConverterConfiguration(
    EnumSerializationStrategy SerializationStrategy,
    EnumDeserializationStrategy DeserializationStrategy
)
{
    public static EnumJsonConverterConfiguration Empty => new(
        EnumSerializationStrategy.ProjectDefault,
        EnumDeserializationStrategy.ProjectDefault
    );
};
