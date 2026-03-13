using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Parsers;

internal static class JsonSerializableAttributeParser
{
    public static ITypeSymbol? Parse(
        GeneratorSyntaxContext context, AttributeSyntax attributeSyntax
    )
    {
        var semanticModel = context.SemanticModel;

        if (
            attributeSyntax.ArgumentList is { Arguments: { Count: > 0 } arguments }
            && arguments[0] is { Expression: TypeOfExpressionSyntax typeOfExpressionSyntax }
        )
        {
            return semanticModel.GetTypeInfo(typeOfExpressionSyntax.Type).Type;
        }

        return null;
    }
}
