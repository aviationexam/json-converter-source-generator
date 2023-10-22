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
                "EnumJsonConvertor",
            });

            i.GenerateDisableEnumJsonConverterAttribute();
            i.GenerateEnumJsonConverterAttribute();
        });

        var enumJsonConverterOptions = context.AnalyzerConfigOptionsProvider.Select((x, _) => new EnumJsonConverterOptions(
            x.GetGlobalOption("DefaultJsonSerializerContext_ClassAccessibility", Id) is { } defaultJsonSerializerContextClassAccessibility
            && x.GetGlobalOption("DefaultJsonSerializerContext_Namespace", Id) is { } defaultJsonSerializerContextNamespace
            && x.GetGlobalOption("DefaultJsonSerializerContext_ClassName", Id) is { } defaultJsonSerializerContextClassName
                ? new JsonSerializerContext(
                    defaultJsonSerializerContextClassAccessibility,
                    defaultJsonSerializerContextNamespace,
                    defaultJsonSerializerContextClassName
                )
                : null,
            x.GetGlobalOption("DefaultEnumSerializationStrategy", Id) is { } defaultEnumSerializationStrategyString
            && Enum.TryParse<EnumSerializationStrategy>(defaultEnumSerializationStrategyString, out var defaultEnumSerializationStrategy)
            && defaultEnumSerializationStrategy != EnumSerializationStrategy.ProjectDefault
                ? defaultEnumSerializationStrategy
                : DefaultEnumSerializationStrategy,
            x.GetGlobalOption("DefaultEnumDeserializationStrategy", Id) is { } defaultEnumDeserializationStrategyString
            && defaultEnumDeserializationStrategyString.Split(
                new[] { '|' }, StringSplitOptions.RemoveEmptyEntries
            ) is { } defaultEnumDeserializationStrategiesString
            && defaultEnumDeserializationStrategiesString.Select(s =>
                Enum.TryParse<EnumDeserializationStrategy>(s, out var defaultEnumDeserializationStrategy)
                    ? defaultEnumDeserializationStrategy
                    : EnumDeserializationStrategy.ProjectDefault
            ).ToArray() is { } defaultEnumDeserializationStrategies
            && defaultEnumDeserializationStrategies.All(s => s != EnumDeserializationStrategy.ProjectDefault)
                ? defaultEnumDeserializationStrategies.ToImmutableArray()
                : new[] { DefaultEnumDeserializationStrategy }.ToImmutableArray()
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

        var diagnostics = resultObject.Diagnostics;

        var files = new List<FileWithName>();
        var converters = new List<JsonConverter>();

        foreach (var context in contexts)
        {
            var convertersTargetNamespace = context.EnumSymbol.ContainingNamespace.ToDisplayString(NamespaceFormat);

            if (
                context is
                {
                    EnumJsonConverterConfiguration: { } enumJsonConverterConfiguration,
                    EnumMemberAttributeSymbol: { } enumMemberAttributeSymbol,
                    EnumSymbol: INamedTypeSymbol enumSymbol
                }
            )
            {
                var enumConverter = EnumJsonConverterGenerator.Generate(
                    enumJsonConverterOptions,
                    convertersTargetNamespace,
                    enumJsonConverterConfiguration,
                    enumSymbol,
                    enumMemberAttributeSymbol,
                    out var converterName
                );

                if (!enumConverter.HasValue)
                {
                    continue;
                }

                files.Add(enumConverter.Value);

                converters.Add(new JsonConverter(
                    convertersTargetNamespace,
                    converterName
                ));
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
