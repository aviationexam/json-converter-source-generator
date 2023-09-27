using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aviationexam.JsonConverter.SourceGenerator.Parsers;

public static class JsonSerializableAttributeParser
{
    public static ITypeSymbol? Parse(
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
}
