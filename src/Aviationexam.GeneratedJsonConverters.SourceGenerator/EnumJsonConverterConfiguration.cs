using System;
using System.Collections.Immutable;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

internal sealed record EnumJsonConverterConfiguration(
    EnumSerializationStrategy SerializationStrategy,
    ImmutableArray<EnumDeserializationStrategy> DeserializationStrategies
)
{
    public static EnumJsonConverterConfiguration Empty => new(
        EnumSerializationStrategy.ProjectDefault,
        Array.Empty<EnumDeserializationStrategy>().ToImmutableArray()
    );
};
