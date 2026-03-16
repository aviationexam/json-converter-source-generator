using System.Collections.Immutable;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

internal sealed record EnumJsonConverterConfiguration(
    ImmutableArray<EnumSerializationStrategy> SerializationStrategies,
    ImmutableArray<EnumDeserializationStrategy> DeserializationStrategies
)
{
    public static EnumJsonConverterConfiguration Empty => new(
        ImmutableArray<EnumSerializationStrategy>.Empty,
        ImmutableArray<EnumDeserializationStrategy>.Empty
    );
};
