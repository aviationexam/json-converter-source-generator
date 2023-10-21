using Aviationexam.GeneratedJsonConverters.SourceGenerator.Extensions;
using Aviationexam.GeneratedJsonConverters.SourceGenerator.Generators;
using Aviationexam.GeneratedJsonConverters.SourceGenerator.Transformers;
using H.Generators;
using H.Generators.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

[Generator]
public class EnumJsonConverterIncrementalGenerator : IIncrementalGenerator
{
    public const string Id = "AVI_EJC";

    private const EnumSerializationStrategy DefaultEnumSerializationStrategy = EnumSerializationStrategy.FirstEnumName;
    private const EnumDeserializationStrategy DefaultEnumDeserializationStrategy = EnumDeserializationStrategy.UseEnumName;

    internal static readonly SymbolDisplayFormat NamespaceFormat = new(
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces
    );

    internal static readonly SymbolDisplayFormat NamespaceFormatWithGenericArguments = new(
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters
    );

    public void Initialize(
        IncrementalGeneratorInitializationContext context
    )
    {
        context.RegisterPostInitializationOutput(i =>
        {
            i.AddEmbeddedResources<EnumJsonConverterIncrementalGenerator>(new[]
            {
                "EnumSerializationStrategy",
                "EnumDeserializationStrategy",
            });

            i.GenerateDisableEnumJsonConverterAttribute();
            i.GenerateEnumJsonConverterAttribute();
        });

        var enumJsonConverterOptions = context.AnalyzerConfigOptionsProvider.Select((x, _) => new EnumJsonConverterOptions(
            x.GlobalOptions.TryGetValue($"{Id}_DefaultJsonSerializerContext_ClassAccessibility", out var defaultJsonSerializerContextClassAccessibility)
            && x.GlobalOptions.TryGetValue($"{Id}_DefaultJsonSerializerContext_Namespace", out var defaultJsonSerializerContextNamespace)
            && x.GlobalOptions.TryGetValue($"{Id}_DefaultJsonSerializerContext_ClassName", out var defaultJsonSerializerContextClassName)
                ? new JsonSerializerContext(
                    defaultJsonSerializerContextClassAccessibility,
                    defaultJsonSerializerContextNamespace,
                    defaultJsonSerializerContextClassName
                )
                : null,
            x.GlobalOptions.TryGetValue($"{Id}_DefaultEnumSerializationStrategy", out var defaultEnumSerializationStrategyString)
            && Enum.TryParse<EnumSerializationStrategy>(defaultEnumSerializationStrategyString, out var defaultEnumSerializationStrategy)
                ? defaultEnumSerializationStrategy
                : DefaultEnumSerializationStrategy,
            x.GlobalOptions.TryGetValue($"{Id}_DefaultEnumDeserializationStrategy", out var defaultEnumDeserializationStrategyString)
            && Enum.TryParse<EnumDeserializationStrategy>(defaultEnumDeserializationStrategyString, out var defaultEnumDeserializationStrategy)
                ? defaultEnumDeserializationStrategy
                : DefaultEnumDeserializationStrategy
        ));

        context.SyntaxProvider.CreateSyntaxProvider(
                predicate: static (node, _) => node is EnumDeclarationSyntax,
                transform: EnumJsonSerializerContextTransformer.GetJsonSerializerContextClassDeclarationSyntax
            )
            .Where(x => x.Result.EnumJsonConverterConfiguration is not null)
            .Collect()
            .Select((x, _) =>
            {
                var diagnostics = new List<Diagnostic>();
                var results = new EnumJsonSerializerContextConfiguration[x.Length];
                for (var i = 0; i < x.Length; i++)
                {
                    results[i] = x[i].Result;
                    diagnostics.AddRange(x[i].Diagnostics);
                }

                return results.ToImmutableArray().AsEquatableArray()
                    .ToResultWithDiagnostics(diagnostics.ToImmutableArray());
            })
            .Combine(enumJsonConverterOptions)
            .SelectAndReportExceptions(GetSourceCode, context, Id)
            .SelectAndReportDiagnostics(context)
            .AddSource(context);
    }

    private static ResultWithDiagnostics<EquatableArray<FileWithName>> GetSourceCode(
        (ResultWithDiagnostics<EquatableArray<EnumJsonSerializerContextConfiguration>> ResultWithDiagnostics, EnumJsonConverterOptions EnumJsonConverterOptions)
            tuple,
        CancellationToken cancellationToken
    )
    {
        var resultObject = tuple.ResultWithDiagnostics;
        var enumJsonConverterOptions = tuple.EnumJsonConverterOptions;
        var contexts = resultObject.Result;

        var diagnostics = ImmutableArray<Diagnostic>.Empty;

        diagnostics = resultObject.Diagnostics.Concat(diagnostics).ToImmutableArray();

        var files = new List<FileWithName>();
        var converters = new List<JsonConverter>();

        foreach (var context in contexts)
        {
            var convertersTargetNamespace = context.EnumSymbol.ContainingNamespace.ToDisplayString(NamespaceFormat);

            if (context.EnumJsonConverterConfiguration is { } enumJsonConverterConfiguration)
            {
                /*
                foreach (var jsonSerializableConfiguration in context.JsonSerializableCollection)
                {
                    var attributes = jsonSerializableConfiguration.JsonSerializableAttributeTypeArgument.GetAttributes();

                    JsonPolymorphicConfiguration? jsonPolymorphicConfiguration = null;
                    var derivedTypes = new List<JsonDerivedTypeConfiguration>();
                    foreach (var attribute in attributes)
                    {
                        if (SymbolEqualityComparer.Default.Equals(attribute.AttributeClass, context.JsonPolymorphicAttributeSymbol))
                        {
                            jsonPolymorphicConfiguration = JsonPolymorphicAttributeParser.Parse(attribute);
                        }

                        if (SymbolEqualityComparer.Default.Equals(attribute.AttributeClass, context.JsonDerivedTypeAttributeSymbol))
                        {
                            var jsonDerivedTypeConfiguration = JsonDerivedTypeAttributeParser.Parse(attribute);

                            if (jsonDerivedTypeConfiguration is not null)
                            {
                                derivedTypes.Add(jsonDerivedTypeConfiguration);
                            }
                        }
                    }

                    files.Add(JsonPolymorphicConverterGenerator.Generate(
                        convertersTargetNamespace,
                        jsonSerializableConfiguration,
                        jsonPolymorphicConfiguration,
                        derivedTypes,
                        out var converterName
                    ));

                    converters.Add(converterName);
                }
                */
            }
        }

        if (
            enumJsonConverterOptions.DefaultJsonSerializerContext is { } defaultJsonSerializerContext
            && converters.Any()
        )
        {
            files.Add(JsonConvertersSerializerJsonContextGenerator.Generate(
                EJsonConverterType.Enum,
                defaultJsonSerializerContext,
                converters
            ));
        }

        return files.ToImmutableArray().AsEquatableArray().ToResultWithDiagnostics(diagnostics);
    }
}
