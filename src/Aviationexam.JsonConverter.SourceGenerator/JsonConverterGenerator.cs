using H.Generators;
using H.Generators.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

namespace Aviationexam.JsonConverter.SourceGenerator;

[Generator]
public class JsonConverterGenerator : IIncrementalGenerator
{
    public const string Id = "AVI_JC";

    private const string JsonSerializableAttribute = "System.Text.Json.Serialization.JsonSerializableAttribute";
    private const string JsonPolymorphicAttribute = "System.Text.Json.Serialization.JsonPolymorphicAttribute";

    public void Initialize(
        IncrementalGeneratorInitializationContext context
    )
    {
        context.SyntaxProvider.CreateSyntaxProvider(
                predicate: static (node, _) => node is ClassDeclarationSyntax { AttributeLists.Count: > 0 },
                transform: static (context, cancellationToken) => GetClassDeclarationSyntax(context, cancellationToken)
            )
            .Select(FilterJsonSerializerContextConfiguration)
            .SelectAndReportExceptions(GetSourceCode, context, Id)
            .SelectAndReportDiagnostics(context)
            .AddSource(context);
    }

    private static ResultWithDiagnostics<JsonSerializerContextWithContextDataConfiguration> GetClassDeclarationSyntax(GeneratorSyntaxContext context,
        CancellationToken cancellationToken)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax) context.Node;

        var jsonSerializableAttributeSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName(JsonSerializableAttribute);
        var jsonPolymorphicAttributeSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName(JsonPolymorphicAttribute);

        var diagnostics = new List<Diagnostic>();

        var jsonSerializerContextClassType = context.SemanticModel.GetDeclaredSymbol(
            classDeclarationSyntax
        ) ?? throw new NullReferenceException(nameof(classDeclarationSyntax));

        var jsonConverterConfiguration = new List<JsonSerializableConfiguration>();

        foreach (var attributeListSyntax in classDeclarationSyntax.AttributeLists)
        {
            foreach (var attributeSyntax in attributeListSyntax.Attributes)
            {
                if (context.SemanticModel.GetSymbolInfo(attributeSyntax, cancellationToken).Symbol is not IMethodSymbol attributeSymbol)
                {
                    // weird, we couldn't get the symbol, ignore it
                    continue;
                }

                var attributeContainingTypeSymbol = attributeSymbol.ContainingType;

                // Is the attribute the [GenerateProxy] attribute?
                if (SymbolEqualityComparer.Default.Equals(attributeContainingTypeSymbol.OriginalDefinition, jsonSerializableAttributeSymbol))
                {
                    var jsonSerializableAttributeTypeArgument = ParseJsonSerializableAttribute(context, attributeSyntax);

                    if (jsonSerializableAttributeTypeArgument is null)
                    {
                        diagnostics.Add(
                            Diagnostic.Create(GeneratorGenerationRules.UnableToParseAttribute, attributeSyntax.GetLocation(), attributeSyntax.ToFullString())
                        );

                        continue;
                    }

                    // return the enum
                    jsonConverterConfiguration.Add(new JsonSerializableConfiguration(
                        jsonSerializableAttributeTypeArgument
                    ));
                }
            }
        }

        return new JsonSerializerContextWithContextDataConfiguration(
                jsonSerializerContextClassType,
                jsonPolymorphicAttributeSymbol!,
                jsonConverterConfiguration
                    .ToImmutableArray()
                    .AsEquatableArray()
            )
            .ToResultWithDiagnostics(diagnostics.ToImmutableArray());
    }

    private static ITypeSymbol? ParseJsonSerializableAttribute(
        GeneratorSyntaxContext context, AttributeSyntax attributeSyntax
    )
    {
        var semanticModel = context.SemanticModel;

        if (attributeSyntax.ArgumentList is { Arguments: [{ Expression: TypeOfExpressionSyntax typeOfExpressionSyntax }] })
        {
            return semanticModel.GetTypeInfo(typeOfExpressionSyntax.Type).Type;
        }

        return null;
    }

    private sealed record JsonSerializableConfiguration(
        ITypeSymbol JsonSerializableAttributeTypeArgument
    );

    private record JsonSerializerContextConfiguration(
        ISymbol JsonSerializerContextClassType,
        EquatableArray<JsonSerializableConfiguration> JsonSerializableCollection
    );

    private sealed record JsonSerializerContextWithContextDataConfiguration(
        ISymbol JsonSerializerContextClassType,
        INamedTypeSymbol JsonPolymorphicAttributeSymbol,
        EquatableArray<JsonSerializableConfiguration> JsonSerializableCollection
    ) : JsonSerializerContextConfiguration(
        JsonSerializerContextClassType,
        JsonSerializableCollection
    );

    private static ResultWithDiagnostics<JsonSerializerContextConfiguration> FilterJsonSerializerContextConfiguration(
        ResultWithDiagnostics<JsonSerializerContextWithContextDataConfiguration> x, CancellationToken cancellationToken
    )
    {
        if (
            x.Result is
            {
                JsonSerializableCollection: { IsEmpty: false } jsonConverterConfigurations,
                JsonPolymorphicAttributeSymbol: { } jsonPolymorphicAttributeSymbol
            }
        )
        {
            var filteredJsonSerializableCollection = new List<JsonSerializableConfiguration>(jsonConverterConfigurations.Count());

            foreach (var jsonConverterConfiguration in jsonConverterConfigurations)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var jsonSerializableTargetAttributes = jsonConverterConfiguration.JsonSerializableAttributeTypeArgument.GetAttributes();

                foreach (var jsonSerializableTargetAttribute in jsonSerializableTargetAttributes)
                {
                    if (SymbolEqualityComparer.Default.Equals(jsonSerializableTargetAttribute.AttributeClass, jsonPolymorphicAttributeSymbol))
                    {
                        filteredJsonSerializableCollection.Add(jsonConverterConfiguration);
                    }
                }
            }

            var filteredConfiguration = x.Result with
            {
                JsonSerializableCollection = filteredJsonSerializableCollection.ToImmutableArray().AsEquatableArray()
            };

            return filteredConfiguration.ToResultWithDiagnostics<JsonSerializerContextConfiguration>(x.Diagnostics);
        }

        return x.Result.ToResultWithDiagnostics<JsonSerializerContextConfiguration>(x.Diagnostics);
    }

    private static ResultWithDiagnostics<FileWithName> GetSourceCode(
        ResultWithDiagnostics<JsonSerializerContextConfiguration> resultObject,
        CancellationToken cancellationToken
    )
    {
        var context = resultObject.Result;

        if (context.JsonSerializableCollection.IsEmpty)
        {
            return FileWithName.Empty.ToResultWithDiagnostics(resultObject.Diagnostics);
        }

        const string fileName = "GitInfo.g.cs";

        var diagnostics = ImmutableArray<Diagnostic>.Empty;

        diagnostics = resultObject.Diagnostics.Concat(diagnostics).ToImmutableArray();

        return new FileWithName(
            fileName,
            string.Empty
        ).ToResultWithDiagnostics(diagnostics);
    }
}
