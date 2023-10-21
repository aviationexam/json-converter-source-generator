using Aviationexam.GeneratedJsonConverters.SourceGenerator.Parsers;
using H.Generators;
using H.Generators.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Transformers;

internal static class PolymorphicJsonSerializerContextTransformer
{
    private const string JsonSerializableAttribute = "System.Text.Json.Serialization.JsonSerializableAttribute";

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private const string RealJsonPolymorphicAttribute = "System.Text.Json.Serialization.JsonPolymorphicAttribute";

    private const string JsonPolymorphicAttribute = "Aviationexam.GeneratedJsonConverters.Attributes.JsonPolymorphicAttribute";

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private const string RealJsonDerivedTypeAttribute = "System.Text.Json.Serialization.JsonDerivedTypeAttribute";

    private const string JsonDerivedTypeAttribute = "Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedTypeAttribute";

    public static ResultWithDiagnostics<PolymorphicJsonSerializerContextConfiguration> GetJsonSerializerContextClassDeclarationSyntax(
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

                // Is the attribute the [JsonSerializable] attribute?
                if (SymbolEqualityComparer.Default.Equals(attributeContainingTypeSymbol.OriginalDefinition, jsonSerializableAttributeSymbol))
                {
                    var jsonSerializableAttributeTypeArgument = JsonSerializableAttributeParser.Parse(context, attributeSyntax);

                    if (jsonSerializableAttributeTypeArgument is null)
                    {
                        diagnostics.Add(
                            Diagnostic.Create(GeneratorGenerationRules.PolymorphicJsonUnableToParseAttribute, attributeSyntax.GetLocation(), attributeSyntax.ToFullString())
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

        return new PolymorphicJsonSerializerContextConfiguration(
                jsonSerializerContextClassType,
                jsonPolymorphicAttributeSymbol!,
                jsonDerivedTypeAttributeSymbol!,
                jsonConverterConfiguration
                    .Distinct()
                    .ToImmutableArray()
                    .AsEquatableArray()
            )
            .ToResultWithDiagnostics(diagnostics.ToImmutableArray());
    }
}
