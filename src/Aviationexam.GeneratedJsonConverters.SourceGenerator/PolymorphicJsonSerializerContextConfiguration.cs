using H.Generators.Extensions;
using Microsoft.CodeAnalysis;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

internal record PolymorphicJsonSerializerContextConfiguration(
    EquatableArray<ISymbol> JsonSerializerContextClassType,
    PolymorphicJsonSerializerMetadata Metadata,
    EquatableArray<JsonSerializableConfiguration> JsonSerializableCollection
);
