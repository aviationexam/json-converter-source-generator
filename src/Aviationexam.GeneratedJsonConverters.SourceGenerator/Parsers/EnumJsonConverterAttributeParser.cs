using Aviationexam.GeneratedJsonConverters.SourceGenerator.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Immutable;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Parsers;

internal static class EnumJsonConverterAttributeParser
{
    public static EnumJsonConverterConfiguration? Parse(
        GeneratorSyntaxContext context, AttributeSyntax attributeSyntax
    )
    {
        var jsonConverterConfiguration = EnumJsonConverterConfiguration.Empty;

        if (attributeSyntax.ArgumentList is { Arguments: var arguments })
        {
            foreach (var argument in arguments)
            {
                if (
                    argument is
                    {
                        NameEquals.Name.Identifier.Value: string argumentName,
                        Expression: { } expression
                    }
                )
                {
                    jsonConverterConfiguration = argumentName switch
                    {
                        EnumJsonConverterAttributeGenerator.DeserializationStrategyPropertyName => jsonConverterConfiguration with
                        {
                            DeserializationStrategies = ParseEnumAsArray<EnumDeserializationStrategy>(expression),
                        },
                        EnumJsonConverterAttributeGenerator.SerializationStrategyPropertyName => jsonConverterConfiguration with
                        {
                            SerializationStrategy = ParseEnum<EnumSerializationStrategy>(expression),
                        },
                        _ => throw new ArgumentOutOfRangeException(nameof(argumentName), argumentName, $"Unknown argumentName {argumentName}"),
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        return jsonConverterConfiguration;
    }

    private static ImmutableArray<TEnum> ParseEnumAsArray<TEnum>(
        ExpressionSyntax expressionSyntax
    ) where TEnum : Enum
    {
        if (expressionSyntax is MemberAccessExpressionSyntax)
        {
            return new ImmutableArray<TEnum>
            {
                ParseEnum<TEnum>(expressionSyntax),
            };
        }

        if (
            expressionSyntax is BinaryExpressionSyntax
            {
                Left: var leftExpression,
                Right: var rightExpression
            }
        )
        {
            return new ImmutableArray<TEnum>
            {
                ParseEnum<TEnum>(leftExpression),
                ParseEnum<TEnum>(rightExpression),
            };
        }

        throw new ArgumentOutOfRangeException(nameof(expressionSyntax), expressionSyntax, $"Not supported expression syntax {expressionSyntax.ToFullString()}");
    }

    private static TEnum ParseEnum<TEnum>(
        ExpressionSyntax expressionSyntax
    ) where TEnum : Enum
    {
        if (
            expressionSyntax is MemberAccessExpressionSyntax
            {
                Name: IdentifierNameSyntax
                {
                    Identifier.Value: string enumName
                }
            }
        )
        {
            return (TEnum) Enum.Parse(typeof(TEnum), enumName);
        }

        throw new ArgumentOutOfRangeException(nameof(expressionSyntax), expressionSyntax, $"Not supported expression syntax {expressionSyntax.ToFullString()}");
    }
}
