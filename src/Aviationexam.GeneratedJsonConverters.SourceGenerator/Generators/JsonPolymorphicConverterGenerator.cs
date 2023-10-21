using H.Generators;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Generators;

internal static class JsonPolymorphicConverterGenerator
{
    private const string DefaultTypeDiscriminatorPropertyName = "$type";

    public static FileWithName Generate(
        string targetNamespace,
        JsonSerializableConfiguration jsonSerializableConfiguration,
        JsonPolymorphicConfiguration? jsonPolymorphicConfiguration,
        IReadOnlyCollection<JsonDerivedTypeConfiguration> derivedTypes,
        out string converterName
    )
    {
        var jsonSerializableAttributeTypeArgument = jsonSerializableConfiguration.JsonSerializableAttributeTypeArgument;
        converterName = GenerateConverterName(jsonSerializableAttributeTypeArgument);

        var fullName = jsonSerializableAttributeTypeArgument.ToDisplayString(JsonPolymorphicConverterIncrementalGenerator.NamespaceFormatWithGenericArguments);

        var discriminatorPropertyName = jsonPolymorphicConfiguration?.DiscriminatorPropertyName ?? DefaultTypeDiscriminatorPropertyName;

        const string prefix = "        ";

        var typeForDiscriminatorStringBuilder = new StringBuilder();
        var discriminatorForTypeStringBuilder = new StringBuilder();
        foreach (var derivedType in derivedTypes)
        {
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
        }

        return new FileWithName(
            $"{converterName}.g.cs",
            // language=cs
            $$"""
              #nullable enable

              namespace {{targetNamespace}};

              internal class {{converterName}} : Aviationexam.GeneratedJsonConverters.PolymorphicJsonConvertor<{{fullName}}>
              {
                  protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "{{discriminatorPropertyName}}"u8;

                  protected override System.Type GetTypeForDiscriminator(
                      Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct discriminator
                  ) => discriminator switch
                  {
              {{typeForDiscriminatorStringBuilder}}
                      _ => throw new System.ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
                  };

                  protected override Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct GetDiscriminatorForType(
                      System.Type type
                  )
                  {
              {{discriminatorForTypeStringBuilder}}
                      throw new System.ArgumentOutOfRangeException(nameof(type), type, null);
                  }
              }
              """
        );
    }

    private static string GenerateConverterName(ITypeSymbol jsonSerializableAttributeTypeArgument)
    {
        var sourceTypeName = jsonSerializableAttributeTypeArgument.Name;

        if (jsonSerializableAttributeTypeArgument is INamedTypeSymbol { TypeArguments: { Length: > 0 } typeArguments })
        {
            var sourceTypeNameStringBuilder = new StringBuilder(sourceTypeName);
            sourceTypeNameStringBuilder.Append("Of");

            for (var i = 0; i < typeArguments.Length; i++)
            {
                sourceTypeNameStringBuilder.Append(typeArguments[i].Name);

                if (i + 1 < typeArguments.Length)
                {
                    sourceTypeNameStringBuilder.Append("And");
                }
            }

            sourceTypeName = sourceTypeNameStringBuilder.ToString();
        }

        return $"{sourceTypeName}JsonPolymorphicConverter";
    }
}
