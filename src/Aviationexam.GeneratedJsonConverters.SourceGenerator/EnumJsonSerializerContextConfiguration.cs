using Microsoft.CodeAnalysis;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

internal record EnumJsonSerializerContextConfiguration(
    ISymbol EnumSymbol,
    INamedTypeSymbol? EnumMemberAttributeSymbol,
    EnumJsonConverterConfiguration? EnumJsonConverterConfiguration
);
