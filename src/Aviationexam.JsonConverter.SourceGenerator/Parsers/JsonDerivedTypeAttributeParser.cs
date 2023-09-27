using Microsoft.CodeAnalysis;

namespace Aviationexam.JsonConverter.SourceGenerator.Parsers;

public static class JsonDerivedTypeAttributeParser
{
    public static JsonDerivedTypeConfiguration? Parse(
        AttributeData attributeData
    )
    {
        var arguments = attributeData.ConstructorArguments;

        ITypeSymbol? targetType;
        string? discriminator = null;
        if (arguments is [var typeArgument])
        {
            targetType = GetTargetTypeSymbol(typeArgument);
        }
        else if (arguments is [var typeArgument2, var discriminatorArgument])
        {
            targetType = GetTargetTypeSymbol(typeArgument2);
            discriminator = GetDiscriminatorSymbol(discriminatorArgument);
        }
        else
        {
            return null;
        }

        if (targetType is null)
        {
            return null;
        }

        return new JsonDerivedTypeConfiguration(
            targetType,
            discriminator
        );
    }

    private static ITypeSymbol? GetTargetTypeSymbol(TypedConstant typedConstant)
    {
        if (typedConstant is { Kind: TypedConstantKind.Type, Value: INamedTypeSymbol namedTypeSymbol })
        {
            return namedTypeSymbol;
        }

        return null;
    }

    private static string? GetDiscriminatorSymbol(TypedConstant typedConstant)
    {
        if (typedConstant is { Kind: TypedConstantKind.Primitive, Value: string discriminator })
        {
            return discriminator;
        }

        if (typedConstant is { Kind: TypedConstantKind.Primitive, Value: int intDiscriminator })
        {
            return intDiscriminator.ToString();
        }

        return null;
    }
}
