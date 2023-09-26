using H.Generators.Extensions;
using Microsoft.CodeAnalysis;

namespace Aviationexam.JsonConverter.SourceGenerator;

public sealed record JsonSerializerContextWithContextDataConfiguration(
    ISymbol JsonSerializerContextClassType,
    INamedTypeSymbol JsonPolymorphicAttributeSymbol,
    EquatableArray<JsonSerializableConfiguration> JsonSerializableCollection
) : JsonSerializerContextConfiguration(
    JsonSerializerContextClassType,
    JsonSerializableCollection
);