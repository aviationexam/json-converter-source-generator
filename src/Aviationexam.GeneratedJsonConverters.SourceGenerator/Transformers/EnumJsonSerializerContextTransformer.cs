﻿using Aviationexam.GeneratedJsonConverters.SourceGenerator.Parsers;
using H.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Transformers;

internal static class EnumJsonSerializerContextTransformer
{
    private const string DisableEnumJsonConverterAttribute = "Aviationexam.GeneratedJsonConverters.Attributes.DisableEnumJsonConverterAttribute";
    private const string EnumJsonConverterAttribute = "Aviationexam.GeneratedJsonConverters.Attributes.EnumJsonConverterAttribute";

    public static ResultWithDiagnostics<EnumJsonSerializerContextConfiguration> GetJsonSerializerContextClassDeclarationSyntax(
        GeneratorSyntaxContext context,
        CancellationToken cancellationToken
    )
    {
        var enumDeclarationSyntax = (EnumDeclarationSyntax) context.Node;

        var disableEnumJsonConverterAttributeSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName(DisableEnumJsonConverterAttribute);
        var enumJsonConverterAttributeSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName(EnumJsonConverterAttribute);

        var diagnostics = new List<Diagnostic>();

        var jsonSerializerContextEnumType = context.SemanticModel.GetDeclaredSymbol(
            enumDeclarationSyntax
        ) ?? throw new NullReferenceException(nameof(enumDeclarationSyntax));

        EnumJsonConverterConfiguration? enumJsonConverterConfiguration = null;

        var ignoreEnum = false;

        foreach (var attributeListSyntax in enumDeclarationSyntax.AttributeLists)
        {
            foreach (var attributeSyntax in attributeListSyntax.Attributes)
            {
                if (context.SemanticModel.GetSymbolInfo(attributeSyntax, cancellationToken).Symbol is not IMethodSymbol attributeSymbol)
                {
                    // weird, we couldn't get the symbol, ignore it
                    continue;
                }

                var attributeContainingTypeSymbol = attributeSymbol.ContainingType;

                // Is the attribute the [EnumJsonConverter] attribute?
                if (SymbolEqualityComparer.Default.Equals(attributeContainingTypeSymbol.OriginalDefinition, disableEnumJsonConverterAttributeSymbol))
                {
                    ignoreEnum = true;
                }
            }
        }

        if (ignoreEnum)
        {
            return new EnumJsonSerializerContextConfiguration(
                    jsonSerializerContextEnumType,
                    enumJsonConverterConfiguration
                )
                .ToResultWithDiagnostics(diagnostics.ToImmutableArray());
        }

        foreach (var attributeListSyntax in enumDeclarationSyntax.AttributeLists)
        {
            foreach (var attributeSyntax in attributeListSyntax.Attributes)
            {
                if (context.SemanticModel.GetSymbolInfo(attributeSyntax, cancellationToken).Symbol is not IMethodSymbol attributeSymbol)
                {
                    // weird, we couldn't get the symbol, ignore it
                    continue;
                }

                var attributeContainingTypeSymbol = attributeSymbol.ContainingType;

                // Is the attribute the [EnumJsonConverter] attribute?
                if (SymbolEqualityComparer.Default.Equals(attributeContainingTypeSymbol.OriginalDefinition, enumJsonConverterAttributeSymbol))
                {
                    enumJsonConverterConfiguration = EnumJsonConverterAttributeParser.Parse(context, attributeSyntax);

                    if (enumJsonConverterConfiguration is null)
                    {
                        diagnostics.Add(
                            Diagnostic.Create(
                                GeneratorGenerationRules.EnumJsonUnableToParseAttribute,
                                attributeSyntax.GetLocation(),
                                attributeSyntax.ToFullString()
                            )
                        );
                    }
                }
            }
        }

        if (enumJsonConverterConfiguration is null)
        {
            diagnostics.Add(
                Diagnostic.Create(
                    GeneratorGenerationRules.EnumWithoutJsonConverterConfiguration,
                    enumDeclarationSyntax.GetLocation(),
                    jsonSerializerContextEnumType.ToDisplayString()
                )
            );
        }

        return new EnumJsonSerializerContextConfiguration(
                jsonSerializerContextEnumType,
                enumJsonConverterConfiguration
            )
            .ToResultWithDiagnostics(diagnostics.ToImmutableArray());
    }
}
