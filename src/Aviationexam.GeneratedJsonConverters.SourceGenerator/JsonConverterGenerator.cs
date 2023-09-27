using Aviationexam.JsonConverter.SourceGenerator.Filters;
using Aviationexam.JsonConverter.SourceGenerator.Generators;
using Aviationexam.JsonConverter.SourceGenerator.Parsers;
using Aviationexam.JsonConverter.SourceGenerator.Transformers;
using H.Generators;
using H.Generators.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;

namespace Aviationexam.JsonConverter.SourceGenerator;

[Generator]
public class JsonConverterGenerator : IIncrementalGenerator
{
    public const string Id = "AVI_JC";

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
            var type = GetType();
            var generatorNamespace = type.Namespace!;

            var manifestResourceNames = type.Assembly.GetManifestResourceNames();

            foreach (var manifestResourceName in manifestResourceNames)
            {
                i.CancellationToken.ThrowIfCancellationRequested();

                var fileName = manifestResourceName[(generatorNamespace.Length + 1)..^3];

                var manifestResourceInfo = type.Assembly.GetManifestResourceStream(manifestResourceName);

                if (manifestResourceInfo is null)
                {
                    continue;
                }

                i.AddSource(
                    $"{fileName}.g.cs",
                    SourceText.From(manifestResourceInfo, Encoding.UTF8, canBeEmbedded: true)
                );
            }

            i.GenerateJsonPolymorphicAttribute();
            i.GenerateJsonDerivedTypeAttribute();
            //i.GenerateIDiscriminatorStruct();
        });

        context.SyntaxProvider.CreateSyntaxProvider(
                predicate: static (node, _) => node is ClassDeclarationSyntax { AttributeLists.Count: > 0 },
                transform: JsonSerializerContextTransformer.GetJsonSerializerContextClassDeclarationSyntax
            )
            .Where(x => !x.Result.JsonSerializableCollection.IsEmpty)
            .Select(JsonSerializerContextConfigurationFilter.FilterJsonSerializerContextConfiguration)
            .SelectAndReportExceptions(GetSourceCode, context, Id)
            .SelectAndReportDiagnostics(context)
            .AddSource(context);
    }

    private static ResultWithDiagnostics<EquatableArray<FileWithName>> GetSourceCode(
        ResultWithDiagnostics<JsonSerializerContextConfiguration> resultObject,
        CancellationToken cancellationToken
    )
    {
        var context = resultObject.Result;

        if (context.JsonSerializableCollection.IsEmpty)
        {
            return ImmutableArray<FileWithName>.Empty
                .AsEquatableArray()
                .ToResultWithDiagnostics(resultObject.Diagnostics);
        }

        var diagnostics = ImmutableArray<Diagnostic>.Empty;

        diagnostics = resultObject.Diagnostics.Concat(diagnostics).ToImmutableArray();

        var files = new List<FileWithName>();
        var converters = new List<string>();

        var convertersTargetNamespace = context.JsonSerializerContextClassType.ContainingNamespace.ToDisplayString(NamespaceFormat);

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

        if (converters.Any())
        {
            files.Add(JsonSerializerContextGenerator.Generate(
                context.JsonSerializerContextClassType,
                convertersTargetNamespace,
                converters
            ));
        }

        return files.ToImmutableArray().AsEquatableArray().ToResultWithDiagnostics(diagnostics);
    }
}
