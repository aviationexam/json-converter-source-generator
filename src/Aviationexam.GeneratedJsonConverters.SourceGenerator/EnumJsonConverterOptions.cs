using System.Collections.Immutable;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

internal record EnumJsonConverterOptions(
    JsonSerializerContext? DefaultJsonSerializerContext,
    EnumSerializationStrategy DefaultEnumSerializationStrategy,
    ImmutableArray<EnumDeserializationStrategy> DefaultEnumDeserializationStrategies
);
