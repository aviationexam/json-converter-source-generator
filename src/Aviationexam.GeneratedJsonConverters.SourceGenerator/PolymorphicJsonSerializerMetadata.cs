using Microsoft.CodeAnalysis;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

internal record PolymorphicJsonSerializerMetadata(
    INamedTypeSymbol JsonPolymorphicAttributeSymbol,
    INamedTypeSymbol JsonDerivedTypeAttributeSymbol
);
