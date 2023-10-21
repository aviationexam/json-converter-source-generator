using H.Generators;
using Microsoft.CodeAnalysis;
using System.Text;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Generators;

internal static class EnumJsonConverterGenerator
{
    public static FileWithName Generate(
        EnumJsonConverterOptions enumJsonConverterOptions,
        string targetNamespace,
        EnumJsonConverterConfiguration enumJsonConverterConfiguration,
        INamedTypeSymbol enumSymbol,
        out string converterName
    )
    {
        converterName = GenerateConverterName(enumSymbol);

        var fullName = enumSymbol.ToDisplayString(JsonPolymorphicConverterIncrementalGenerator.NamespaceFormatWithGenericArguments);

        const string prefix = "        ";

        var typeForDiscriminatorStringBuilder = new StringBuilder();
        var discriminatorForTypeStringBuilder = new StringBuilder();

        foreach (var typeMember in enumSymbol.GetMembers().OfType<IFieldSymbol>())
        {
            var stringEnumValue = typeMember.Name;
            var constantValue = typeMember.ConstantValue;


            /*
            typeForDiscriminatorStringBuilder.Append(prefix);

            var fullTargetType = derivedType.TargetType.ToDisplayString(JsonPolymorphicConverterIncrementalGenerator.NamespaceFormat);

            var discriminator = derivedType.Discriminator;
            if (discriminator is null)
            {
                discriminator = new DiscriminatorStruct<string> { Value = derivedType.TargetType.Name };
            }

            var (typeForDiscriminatorCase, discriminatorForTypeCase) = discriminator switch
            {
                DiscriminatorStruct<string> discriminatorString => (
                    $"Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> {{ Value: \"{discriminatorString.Value}\" }}",
                    $"    return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>(\"{discriminatorString.Value}\");"
                ),
                DiscriminatorStruct<int> discriminatorInt => (
                    $"Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<int> {{ Value: {discriminatorInt.Value} }}",
                    $"    return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<int>({discriminatorInt.Value});"
                ),
                _ => throw new ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
            };

            typeForDiscriminatorStringBuilder.Append(
                $"{typeForDiscriminatorCase} => typeof({fullTargetType}),"
            );

            typeForDiscriminatorStringBuilder.AppendLine();

            discriminatorForTypeStringBuilder.Append(prefix);
            discriminatorForTypeStringBuilder.AppendLine($"if (type == typeof({fullTargetType}))");
            discriminatorForTypeStringBuilder.Append(prefix);
            discriminatorForTypeStringBuilder.AppendLine("{");
            discriminatorForTypeStringBuilder.Append(prefix);
            discriminatorForTypeStringBuilder.AppendLine(discriminatorForTypeCase);
            discriminatorForTypeStringBuilder.Append(prefix);
            discriminatorForTypeStringBuilder.AppendLine("}");
            */
        }

        return new FileWithName(
            $"{converterName}.g.cs",
            // language=cs
            $$"""
              #nullable enable

              namespace {{targetNamespace}};

              internal class {{converterName}} : Aviationexam.GeneratedJsonConverters.EnumJsonConvertor<{{fullName}}>
              {
              }
              """
        );
    }

    private static string GenerateConverterName(
        ISymbol enumSymbol
    ) => $"{enumSymbol.Name}EnumJsonConverter";
}
