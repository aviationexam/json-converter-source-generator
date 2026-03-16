using Microsoft.CodeAnalysis;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

internal static class GeneratorGenerationRules
{
    public static readonly DiagnosticDescriptor PolymorphicJsonUnableToParseAttribute = new(
        $"{JsonPolymorphicConverterIncrementalGenerator.Id}_0001",
        "Unable to parse marking attribute",
        "Unable to parse marking attribute {0}",
        "Aviationexam.GeneratedJsonConverters.SourceGenerator",
        DiagnosticSeverity.Error,
        true
    );

    public static readonly DiagnosticDescriptor EnumJsonUnableToParseAttribute = new(
        $"{EnumJsonConverterIncrementalGenerator.Id}_0001",
        "Unable to parse marking attribute",
        "Unable to parse marking attribute {0}",
        "Aviationexam.GeneratedJsonConverters.SourceGenerator",
        DiagnosticSeverity.Error,
        true
    );

    public static readonly DiagnosticDescriptor EnumWithoutJsonConverterConfiguration = new(
        $"{EnumJsonConverterIncrementalGenerator.Id}_0002",
        "There is an enum without json converter configuration",
        "There is an enum '{0}' without json converter configuration. Add [EnumJsonConverterAttribute] or disable warning using [DisableEnumJsonConverterAttribute].",
        "Aviationexam.GeneratedJsonConverters.SourceGenerator",
        DiagnosticSeverity.Warning,
        true
    );

    public static readonly DiagnosticDescriptor FlagsArrayOnNonFlagsEnum = new(
        id: "AVI_EJC_0003",
        title: "FlagsArray strategy on non-[Flags] enum",
        messageFormat: "Enum '{0}' uses FlagsArray serialization strategy but does not have [Flags] attribute",
        category: "Aviationexam.GeneratedJsonConverters",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );
}
