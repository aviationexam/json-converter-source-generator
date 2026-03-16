using Microsoft.CodeAnalysis;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

public readonly record struct JsonConverterWithJsonContexts(
    ISymbol JsonSerializerContextClassType,
    JsonConverter Converter
);
