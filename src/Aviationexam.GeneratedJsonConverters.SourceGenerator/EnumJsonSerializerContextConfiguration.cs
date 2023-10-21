using Microsoft.CodeAnalysis;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

internal record EnumJsonSerializerContextConfiguration(
    ISymbol EnumSymbol,
    EnumJsonConverterConfiguration? EnumJsonConverterConfiguration
);
