using Microsoft.CodeAnalysis;

namespace Aviationexam.JsonConverter.SourceGenerator.Parsers;

internal static class JsonPolymorphicAttributeParser
{
    public static JsonPolymorphicConfiguration? Parse(
        AttributeData attributeData
    )
    {
        var arguments = attributeData.ConstructorArguments;

        if (arguments is [var typedConstant])
        {
            if (typedConstant is { Kind: TypedConstantKind.Primitive, Value: string discriminatorPropertyName })
            {
                return new JsonPolymorphicConfiguration(discriminatorPropertyName);
            }
        }

        return null;
    }
}
