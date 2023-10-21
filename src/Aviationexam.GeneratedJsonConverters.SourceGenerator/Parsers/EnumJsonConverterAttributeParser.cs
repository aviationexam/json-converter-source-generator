using Aviationexam.GeneratedJsonConverters.SourceGenerator.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Runtime.CompilerServices;

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
                            DeserializationStrategy = ParseEnum<EnumDeserializationStrategy>(expression),
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

    private static TEnum ParseEnum<TEnum>(ExpressionSyntax expressionSyntax)
        where TEnum : Enum
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

        if (
            expressionSyntax is BinaryExpressionSyntax
            {
                Left: var leftExpression,
                Right: var rightExpression
            }
        )
        {
            var leftEnum = ParseEnum<TEnum>(leftExpression);
            var rightEnum = ParseEnum<TEnum>(rightExpression);

            return Type.GetTypeCode(typeof(TEnum)) switch
            {
                TypeCode.SByte => EnumBinaryOr<TEnum, sbyte>(leftEnum, rightEnum, (x, y) => (sbyte) (x | y)),
                TypeCode.Int16 => EnumBinaryOr<TEnum, short>(leftEnum, rightEnum, (x, y) => (short) (x | y)),
                TypeCode.Int32 => EnumBinaryOr<TEnum, int>(leftEnum, rightEnum, (x, y) => x | y),
                TypeCode.Byte => EnumBinaryOr<TEnum, byte>(leftEnum, rightEnum, (x, y) => (byte) (x | y)),
                TypeCode.UInt16 => EnumBinaryOr<TEnum, ushort>(leftEnum, rightEnum, (x, y) => (ushort) (x | y)),
                TypeCode.UInt32 => EnumBinaryOr<TEnum, uint>(leftEnum, rightEnum, (x, y) => x | y),
                TypeCode.Int64 => EnumBinaryOr<TEnum, long>(leftEnum, rightEnum, (x, y) => x | y),
                TypeCode.UInt64 => EnumBinaryOr<TEnum, long>(leftEnum, rightEnum, (x, y) => x | y),
                _ => EnumBinaryOr<TEnum, int>(leftEnum, rightEnum, (x, y) => x | y),
            };
        }

        throw new ArgumentOutOfRangeException(nameof(expressionSyntax), expressionSyntax, $"Not supported expression syntax {expressionSyntax.ToFullString()}");
    }

    private static TEnum EnumBinaryOr<TEnum, TBackingType>(
        TEnum left,
        TEnum right,
        Func<TBackingType, TBackingType, TBackingType> merge
    )
        where TBackingType : struct
    {
        var backingLeft = Unsafe.As<TEnum, TBackingType>(ref left);
        var backingRight = Unsafe.As<TEnum, TBackingType>(ref right);

        var result = merge(backingLeft, backingRight);

        return Unsafe.As<TBackingType, TEnum>(ref result);
    }
}
