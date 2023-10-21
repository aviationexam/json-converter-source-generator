using Microsoft.CodeAnalysis;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

internal static class GeneratorGenerationRules
{
    public static readonly DiagnosticDescriptor UnableToParseAttribute = new(
        $"{JsonPolymorphicConverterIncrementalGenerator.Id}_0001",
        "Unable to parse marking attribute",
        "Unable to parse marking attribute {0}",
        "Aviationexam.GeneratedJsonConverters.SourceGenerator",
        DiagnosticSeverity.Error,
        true
    );
}
