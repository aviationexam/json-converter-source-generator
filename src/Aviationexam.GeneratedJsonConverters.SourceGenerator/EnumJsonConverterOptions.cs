using System.Collections.Immutable;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

internal record EnumJsonConverterOptions(
    JsonSerializerContext? DefaultJsonSerializerContext,
    ImmutableArray<EnumSerializationStrategy> DefaultEnumSerializationStrategies,
    ImmutableArray<EnumDeserializationStrategy> DefaultEnumDeserializationStrategies
);
