using Microsoft.CodeAnalysis;
using ZLinq;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Parsers;

internal static class JsonPolymorphicAttributeParser
{
    public const string TypeDiscriminatorPropertyNamePropertyName = "TypeDiscriminatorPropertyName";

    public static JsonPolymorphicConfiguration? Parse(
        AttributeData attributeData
    )
    {
        if (
            attributeData.NamedArguments
                .AsValueEnumerable()
                .Where(x => x.Key == TypeDiscriminatorPropertyNamePropertyName)
                .Select(x => x.Value)
                .SingleOrDefault() is
            {
                Kind: TypedConstantKind.Primitive,
                Value: string discriminatorPropertyName,
            }
        )
        {
            return new JsonPolymorphicConfiguration(discriminatorPropertyName);
        }

        return null;
    }
}
