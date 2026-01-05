using Aviationexam.GeneratedJsonConverters.SourceGenerator.Extensions;
using Aviationexam.GeneratedJsonConverters.SourceGenerator.Filters;
using Aviationexam.GeneratedJsonConverters.SourceGenerator.Generators;
using Aviationexam.GeneratedJsonConverters.SourceGenerator.Parsers;
using Aviationexam.GeneratedJsonConverters.SourceGenerator.Transformers;
using H.Generators.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using ZLinq;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

[Generator]
public class JsonPolymorphicConverterIncrementalGenerator : IIncrementalGenerator
{
    public const string Id = "AVI_JPC";

    private const string EmptyPolymorphicNamespace = "PolymorphicGlobalNamespace";

    internal static readonly SymbolDisplayFormat NamespaceFormat = new(
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters
    );

    internal static readonly SymbolDisplayFormat NamespaceFormatWithGenericArguments = new(
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Included,
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters
    );

    public void Initialize(
        IncrementalGeneratorInitializationContext context
    )
    {
        context.RegisterPostInitializationOutput(i =>
        {
            i.AddEmbeddedResources<JsonPolymorphicConverterIncrementalGenerator>([
                "DiscriminatorStruct",
                "IDiscriminatorStruct",
                "PolymorphicJsonConvertor",
            ]);

            i.GenerateJsonPolymorphicAttribute();
            i.GenerateJsonDerivedTypeAttribute();
        });

        context.SyntaxProvider.CreateSyntaxProvider(
                predicate: static (node, _) => node is ClassDeclarationSyntax { AttributeLists.Count: > 0 },
                transform: PolymorphicJsonSerializerContextTransformer.GetJsonSerializerContextClassDeclarationSyntax
            )
            .Where(x => !x.Result.JsonSerializableCollection.IsEmpty)
            .Select(PolymorphicJsonSerializerContextConfigurationFilter.FilterJsonSerializerContextConfiguration)
            .CollectAsEquatableArray()
            .SelectMany((x, cancellationToken) =>
            {
                var results = new List<ResultWithDiagnostics<PolymorphicJsonSerializerContextConfiguration>>();
                foreach (
                    var grouping in
                    x.AsValueEnumerable()
                        .GroupBy(r => r.Result
                            .JsonSerializableCollection
                            .AsValueEnumerable()
                            .Select(c => c.JsonSerializableAttributeTypeArgument.ToDisplayString(NamespaceFormatWithGenericArguments))
                            .JoinToString(',')
                        )
                )
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var jsonSerializerContextClassTypes = new List<EquatableArray<ISymbol>>();
                    PolymorphicJsonSerializerMetadata? jsonPolymorphicMetadata = null;
                    var jsonSerializableCollections = new List<EquatableArray<JsonSerializableConfiguration>>();
                    var diagnostics = new List<EquatableArray<Diagnostic>>();
                    foreach (var item in grouping)
                    {
                        jsonSerializerContextClassTypes.Add(item.Result.JsonSerializerContextClassType);
                        jsonPolymorphicMetadata ??= item.Result.Metadata;
                        jsonSerializableCollections.Add(item.Result.JsonSerializableCollection);
                        diagnostics.Add(item.Diagnostics);
                    }

                    if (
                        jsonPolymorphicMetadata is null
                    )
                    {
                        continue;
                    }

                    results.Add(new PolymorphicJsonSerializerContextConfiguration(
                        jsonSerializerContextClassTypes.AsValueEnumerable().SelectMany(static x => x).ToArray().ToImmutableArray().AsEquatableArray(),
                        jsonPolymorphicMetadata,
                        jsonSerializableCollections
                            .AsValueEnumerable()
                            .SelectMany(static x => x)
                            .DistinctBy(c => c
                                .JsonSerializableAttributeTypeArgument.ToDisplayString(NamespaceFormatWithGenericArguments)
                            )
                            .ToArray()
                            .ToImmutableArray()
                            .AsEquatableArray()
                    ).ToResultWithDiagnostics(diagnostics.AsValueEnumerable().SelectMany(static x => x).ToArray().ToImmutableArray().AsEquatableArray()));
                }

                return results;
            })
            .SelectAndReportExceptions(GetSourceCode, context, Id)
            .SelectAndReportDiagnostics(context)
            .AddSource(context);
    }

    private static ResultWithDiagnostics<EquatableArray<FileWithName>> GetSourceCode(
        ResultWithDiagnostics<PolymorphicJsonSerializerContextConfiguration> resultObject,
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

        var diagnostics = resultObject.Diagnostics.AsImmutableArray();

        var files = new List<FileWithName>();
        var converters = new Dictionary<string, ICollection<JsonConverter>>();

        foreach (var jsonSerializableConfiguration in context.JsonSerializableCollection)
        {
            var attributes = jsonSerializableConfiguration.JsonSerializableAttributeTypeArgument.GetAttributes();

            JsonPolymorphicConfiguration? jsonPolymorphicConfiguration = null;
            var derivedTypes = new List<JsonDerivedTypeConfiguration>();
            foreach (var attribute in attributes)
            {
                if (SymbolEqualityComparer.Default.Equals(attribute.AttributeClass, context.Metadata.JsonPolymorphicAttributeSymbol))
                {
                    jsonPolymorphicConfiguration = JsonPolymorphicAttributeParser.Parse(attribute);
                }

                if (
                    SymbolEqualityComparer.Default.Equals(attribute.AttributeClass, context.Metadata.JsonDerivedTypeAttributeSymbol)
                    || (
                        attribute.AttributeClass is { IsGenericType: true } attributeClass
                        && SymbolEqualityComparer.Default.Equals(attributeClass.BaseType, context.Metadata.JsonDerivedTypeAttributeSymbol)
                    )
                )
                {
                    var jsonDerivedTypeConfiguration = JsonDerivedTypeAttributeParser.Parse(attribute);

                    if (jsonDerivedTypeConfiguration is not null)
                    {
                        derivedTypes.Add(jsonDerivedTypeConfiguration);
                    }
                }
            }

            foreach (
                var jsonSerializerContextClassType
                in context.JsonSerializerContextClassType
                    .AsValueEnumerable()
                    .GroupBy(static x => x.ContainingNamespace.ToDisplayString(NamespaceFormat))
                    .Select(static x => x.AsValueEnumerable().First())
            )
            {
                var convertersTargetNamespace = jsonSerializerContextClassType.ContainingNamespace.IsGlobalNamespace
                    ? EmptyPolymorphicNamespace
                    : jsonSerializerContextClassType.ContainingNamespace.ToDisplayString(NamespaceFormat);

                if (!converters.TryGetValue(convertersTargetNamespace, out var convertersCollection))
                {
                    convertersCollection = new List<JsonConverter>();
                    converters.Add(convertersTargetNamespace, convertersCollection);
                }

                files.Add(JsonPolymorphicConverterGenerator.Generate(
                    convertersTargetNamespace,
                    jsonSerializableConfiguration,
                    jsonPolymorphicConfiguration,
                    SortDerivedTypes(derivedTypes),
                    out var converterName
                ));

                convertersCollection.Add(new JsonConverter(
                    convertersTargetNamespace,
                    converterName
                ));
            }
        }

        if (converters.AsValueEnumerable().Any())
        {
            foreach (
                var jsonSerializerContextClassType
                in context.JsonSerializerContextClassType
                    .AsValueEnumerable()
                    .GroupBy(static x => x.ContainingNamespace.ToDisplayString(NamespaceFormatWithGenericArguments))
                    .SelectMany(static x => x)
            )
            {
                var convertersTargetNamespace = jsonSerializerContextClassType.ContainingNamespace.IsGlobalNamespace
                    ? EmptyPolymorphicNamespace
                    : jsonSerializerContextClassType.ContainingNamespace.ToDisplayString(NamespaceFormat);

                files.Add(JsonConvertersSerializerJsonContextGenerator.Generate(
                    EJsonConverterType.Polymorphic,
                    jsonSerializerContextClassType,
                    [.. converters[convertersTargetNamespace]]
                ));
            }
        }

        return files.ToImmutableArray().AsEquatableArray().ToResultWithDiagnostics(diagnostics);
    }

    private static IReadOnlyCollection<JsonDerivedTypeConfiguration> SortDerivedTypes(
        IReadOnlyCollection<JsonDerivedTypeConfiguration> derivedTypes
    )
    {
        var baseTypeDict = derivedTypes.AsValueEnumerable().ToDictionary(
            x => x.TargetType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            x => (
                configuration: x,
                baseTypes: GetAllBaseTypes(x.TargetType)
            )
        );

        var inheritanceMap = new List<(JsonDerivedTypeConfiguration Parent, JsonDerivedTypeConfiguration Child)>();

        foreach (var baseTypeMetadata in baseTypeDict)
        {
            foreach (var baseType in baseTypeMetadata.Value.baseTypes)
            {
                if (baseTypeDict.TryGetValue(baseType, out var derivedType))
                {
                    inheritanceMap.Add((
                        Parent: baseTypeMetadata.Value.configuration,
                        Child: derivedType.configuration
                    ));
                }
            }
        }

        var queue = new Queue<JsonDerivedTypeConfiguration>(derivedTypes);
        var orderedDerivedTypes = new List<JsonDerivedTypeConfiguration>(derivedTypes.Count);

        while (queue.Count > 0)
        {
            var configuration = queue.Dequeue();

            if (inheritanceMap.AsValueEnumerable().All(x => x.Child != configuration))
            {
                orderedDerivedTypes.Add(configuration);

                inheritanceMap.RemoveAll(x => x.Parent == configuration);
            }
            else
            {
                queue.Enqueue(configuration);
            }
        }

        return orderedDerivedTypes;
    }

    private static IReadOnlyCollection<string> GetAllBaseTypes(ITypeSymbol type)
    {
        var result = new List<string>();

        var current = type.BaseType;

        while (current is not null)
        {
            result.Add(current.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));

            current = current.BaseType;
        }

        return result;
    }
}
