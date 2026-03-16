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
                "IPolymorphicJsonConvertor",
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
            .MergeJsonSerializerContextConfiguration()
            .SelectAndReportExceptions(GetSourceCode, context, Id)
            .CollectAsEquatableArray()
            .SelectAndReportExceptions(GetConverterSourceCode, context, Id)
            .SelectAndReportDiagnostics(context)
            .AddSource(context);
    }

    private static ResultWithDiagnostics<PolymorphicJsonConverterProduced> GetSourceCode(
        ResultWithDiagnostics<PolymorphicJsonSerializerContextConfiguration> resultObject,
        CancellationToken cancellationToken
    )
    {
        var context = resultObject.Result;

        if (context.JsonSerializableCollection.IsEmpty)
        {
            return new PolymorphicJsonConverterProduced(
                ImmutableArray<FileWithName>.Empty.AsEquatableArray(),
                ImmutableArray<JsonConverterWithJsonContexts>.Empty.AsEquatableArray()
            ).ToResultWithDiagnostics(resultObject.Diagnostics);
        }

        var diagnostics = resultObject.Diagnostics.AsImmutableArray();

        var files = new List<FileWithName>();
        var converters = new List<JsonConverterWithJsonContexts>();

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
                        var conversion = jsonSerializableConfiguration.Compilation.ClassifyConversion(
                            jsonDerivedTypeConfiguration.TargetType,
                            jsonSerializableConfiguration.JsonSerializableAttributeTypeArgument
                        );

                        if (
                            (conversion.IsIdentity || conversion.IsBoxing || conversion.IsImplicit)
                            && (
                                jsonDerivedTypeConfiguration.TargetType.IsNullable()
                                || !jsonSerializableConfiguration.JsonSerializableAttributeTypeArgument.IsNullable()
                            )
                        )
                        {
                            derivedTypes.Add(jsonDerivedTypeConfiguration);
                        }
                    }
                }
            }

            foreach (
                var grouping
                in context.JsonSerializerContextClassType
                    .AsValueEnumerable()
                    .GroupBy(static x => x.ContainingNamespace.ToDisplayString(NamespaceFormat))
            )
            {
                var jsonSerializerContextClassType = grouping.AsValueEnumerable().First();

                var convertersTargetNamespace = jsonSerializerContextClassType.ContainingNamespace.IsGlobalNamespace
                    ? EmptyPolymorphicNamespace
                    : jsonSerializerContextClassType.ContainingNamespace.ToDisplayString(NamespaceFormat);

                files.Add(JsonPolymorphicConverterGenerator.Generate(
                    convertersTargetNamespace,
                    jsonSerializableConfiguration,
                    jsonPolymorphicConfiguration,
                    SortDerivedTypes(derivedTypes),
                    out var converterName
                ));

                converters.AddRange(grouping.AsValueEnumerable().Select(x => new JsonConverterWithJsonContexts(
                    x,
                    new JsonConverter(
                        convertersTargetNamespace,
                        converterName
                    )
                )).ToList());
            }
        }

        return new PolymorphicJsonConverterProduced(
            files.ToImmutableArray().AsEquatableArray(),
            converters.ToImmutableArray().AsEquatableArray()
        ).ToResultWithDiagnostics(diagnostics);
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

    private static ResultWithDiagnostics<EquatableArray<FileWithName>> GetConverterSourceCode(
        EquatableArray<ResultWithDiagnostics<PolymorphicJsonConverterProduced>> resultObject,
        CancellationToken cancellationToken
    )
    {
        if (resultObject.IsEmpty)
        {
            return ImmutableArray<FileWithName>.Empty
                .AsEquatableArray()
                .ToResultWithDiagnostics();
        }

        var files = resultObject.AsValueEnumerable().SelectMany(x => x.Result.Files).ToList();
        var producedConverters = resultObject.AsValueEnumerable().SelectMany(x => x.Result.ProducedConverters).ToList();
        var diagnostics = resultObject.AsValueEnumerable().SelectMany(x => x.Diagnostics).ToList();

        foreach (
            var item
            in producedConverters.AsValueEnumerable()
                .GroupBy(x => x.JsonSerializerContextClassType)
        )
        {
            var jsonSerializerContextClassType = item.AsValueEnumerable().Select(x => x.JsonSerializerContextClassType).First();
            var converters = item.AsValueEnumerable().Select(x => x.Converter).ToList();

            files.Add(JsonConvertersSerializerJsonContextGenerator.Generate(
                EJsonConverterType.Polymorphic,
                jsonSerializerContextClassType,
                converters
            ));
        }

        return files.ToImmutableArray().AsEquatableArray().ToResultWithDiagnostics([.. diagnostics]);
    }
}
