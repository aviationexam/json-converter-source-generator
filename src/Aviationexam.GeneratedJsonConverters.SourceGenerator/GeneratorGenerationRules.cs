using Microsoft.CodeAnalysis;

namespace Aviationexam.JsonConverter.SourceGenerator;

internal static class GeneratorGenerationRules
{
    public static readonly DiagnosticDescriptor UnableToParseAttribute = new(
        $"{JsonConverterGenerator.Id}_0001",
        "Unable to parse marking attribute",
        "Unable to parse marking attribute {0}",
        "Aviationexam.Core.Common.GeneratedGenericProxy",
        DiagnosticSeverity.Error,
        true
    );
}
