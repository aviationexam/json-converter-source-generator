using H.Generators.Extensions;
using Microsoft.CodeAnalysis;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

internal record PolymorphicJsonSerializerContextConfiguration(
    ISymbol JsonSerializerContextClassType,
    INamedTypeSymbol JsonPolymorphicAttributeSymbol,
    INamedTypeSymbol JsonDerivedTypeAttributeSymbol,
    EquatableArray<JsonSerializableConfiguration> JsonSerializableCollection
);
