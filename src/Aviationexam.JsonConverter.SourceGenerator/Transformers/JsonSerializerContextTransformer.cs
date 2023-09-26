using Aviationexam.JsonConverter.SourceGenerator.Parsers;
using H.Generators;
using H.Generators.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;

namespace Aviationexam.JsonConverter.SourceGenerator.Transformers;

public static class JsonSerializerContextTransformer
{
    private const string JsonSerializableAttribute = "System.Text.Json.Serialization.JsonSerializableAttribute";
    private const string JsonPolymorphicAttribute = "System.Text.Json.Serialization.JsonPolymorphicAttribute";
    private const string JsonDerivedTypeAttribute = "System.Text.Json.Serialization.JsonDerivedTypeAttribute";

    public static ResultWithDiagnostics<JsonSerializerContextConfiguration> GetJsonSerializerContextClassDeclarationSyntax(
        GeneratorSyntaxContext context,
        CancellationToken cancellationToken
    )
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax) context.Node;

        var jsonSerializableAttributeSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName(JsonSerializableAttribute);
        var jsonPolymorphicAttributeSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName(JsonPolymorphicAttribute);
        var jsonDerivedTypeAttributeSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName(JsonDerivedTypeAttribute);

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
                    var jsonSerializableAttributeTypeArgument = JsonSerializableAttributeParser.Parse(context, attributeSyntax);

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

        return new JsonSerializerContextConfiguration(
                jsonSerializerContextClassType,
                jsonPolymorphicAttributeSymbol!,
                jsonDerivedTypeAttributeSymbol!,
                jsonConverterConfiguration
                    .ToImmutableArray()
                    .AsEquatableArray()
            )
            .ToResultWithDiagnostics(diagnostics.ToImmutableArray());
    }



}
