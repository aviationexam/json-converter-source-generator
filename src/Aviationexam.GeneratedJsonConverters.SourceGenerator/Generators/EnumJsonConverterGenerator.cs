using H.Generators;
using Microsoft.CodeAnalysis;
using System;
using System.Text;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Generators;

internal static class EnumJsonConverterGenerator
{
    public static FileWithName? Generate(
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

        Type? backingType = null;
        foreach (var typeMember in enumSymbol.GetMembers().OfType<IFieldSymbol>())
        {
            var stringEnumValue = typeMember.Name;
            var constantValue = typeMember.ConstantValue ?? throw new NullReferenceException(nameof(typeMember.ConstantValue));
            backingType = constantValue.GetType();


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

        if (backingType is null)
        {
            return null;
        }

        return new FileWithName(
            $"{converterName}.g.cs",
            // language=cs
            $$"""
              #nullable enable

              namespace {{targetNamespace}};

              internal class {{converterName}} : Aviationexam.GeneratedJsonConverters.EnumJsonConvertor<{{fullName}}, {{backingType}}>
              {
                  protected override EnumDeserializationStrategy DeserializationStrategy { get; }

                  protected override EnumSerializationStrategy SerializationStrategy { get; }
              }
              """
        );
    }

    private static string GenerateConverterName(
        ISymbol enumSymbol
    ) => $"{enumSymbol.Name}EnumJsonConverter";
}
