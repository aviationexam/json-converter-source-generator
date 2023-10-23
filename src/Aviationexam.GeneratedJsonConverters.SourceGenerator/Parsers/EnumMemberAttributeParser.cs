using Microsoft.CodeAnalysis;
using System.Linq;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Parsers;

internal static class EnumMemberAttributeParser
{
    public static string? Parse(
        AttributeData attributeData
    )
    {
        if (
            attributeData.NamedArguments
                .Where(x => x.Key == "Value")
                .Select(x => x.Value).SingleOrDefault() is
            {
                Kind: TypedConstantKind.Primitive,
                Value: string enumMemberValue
            }
        )
        {
            return enumMemberValue;
        }

        return null;
    }
}
