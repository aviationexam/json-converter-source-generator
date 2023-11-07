using Microsoft.CodeAnalysis;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Parsers;

internal static class JsonDerivedTypeAttributeParser
{
    public static JsonDerivedTypeConfiguration? Parse(
        AttributeData attributeData
    )
    {
        var arguments = attributeData.ConstructorArguments;

        ITypeSymbol? targetType;
        IDiscriminatorStruct? discriminator = null;
        if (
            attributeData.AttributeClass is { IsGenericType: true, TypeArguments: [var genericTypeArgument] }
            && arguments is [var discriminatorArgument]
        )
        {
            targetType = genericTypeArgument;
            discriminator = GetDiscriminatorSymbol(discriminatorArgument);
        }
        else if (arguments is [var typeArgument])
        {
            targetType = GetTargetTypeSymbol(typeArgument);
        }
        else if (arguments is [var typeArgument2, var discriminatorArgument2])
        {
            targetType = GetTargetTypeSymbol(typeArgument2);
            discriminator = GetDiscriminatorSymbol(discriminatorArgument2);
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

    private static IDiscriminatorStruct? GetDiscriminatorSymbol(TypedConstant typedConstant)
    {
        if (typedConstant is { Kind: TypedConstantKind.Primitive, Value: string discriminator })
        {
            return new DiscriminatorStruct<string>(discriminator);
        }

        if (typedConstant is { Kind: TypedConstantKind.Primitive, Value: int intDiscriminator })
        {
            return new DiscriminatorStruct<int>(intDiscriminator);
        }

        return null;
    }
}
