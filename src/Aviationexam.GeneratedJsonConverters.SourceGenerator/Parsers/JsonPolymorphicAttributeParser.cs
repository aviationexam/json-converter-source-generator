using Aviationexam.GeneratedJsonConverters.SourceGenerator.Generators;
using Microsoft.CodeAnalysis;
using System.Linq;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Parsers;

internal static class JsonPolymorphicAttributeParser
{
    public static JsonPolymorphicConfiguration? Parse(
        AttributeData attributeData
    )
    {
        if (
            attributeData.NamedArguments
                .Where(x => x.Key == JsonPolymorphicAttributeGenerator.TypeDiscriminatorPropertyNamePropertyName)
                .Select(x => x.Value).SingleOrDefault() is
            {
                Kind: TypedConstantKind.Primitive,
                Value: string discriminatorPropertyName
            }
        )
        {
            return new JsonPolymorphicConfiguration(discriminatorPropertyName);
        }

        return null;
    }
}
