using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Parsers;

internal static class EnumJsonConverterAttributeParser
{
    public static EnumJsonConverterConfiguration? Parse(
        GeneratorSyntaxContext context, AttributeSyntax attributeSyntax
    )
    {
        var semanticModel = context.SemanticModel;

        if (attributeSyntax.ArgumentList is { Arguments: [{ Expression: TypeOfExpressionSyntax typeOfExpressionSyntax }] })
        {
//            return semanticModel.GetTypeInfo(typeOfExpressionSyntax.Type).Type;
            return null;
        }

        return null;
    }
}
