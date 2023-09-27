using H.Generators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aviationexam.JsonConverter.SourceGenerator.Generators;

public static class JsonPolymorphicConverterGenerator
{
    private const string DefaultTypeDiscriminatorPropertyName = "$type";

    public static FileWithName Generate(
        JsonSerializableConfiguration jsonSerializableConfiguration,
        JsonPolymorphicConfiguration? jsonPolymorphicConfiguration,
        IReadOnlyCollection<JsonDerivedTypeConfiguration> derivedTypes,
        out string converterName
    )
    {
        var jsonSerializableAttributeTypeArgument = jsonSerializableConfiguration.JsonSerializableAttributeTypeArgument;
        converterName = $"{jsonSerializableAttributeTypeArgument.Name}JsonPolymorphicConverter";

        var fullName = jsonSerializableAttributeTypeArgument.ToDisplayString(JsonConverterGenerator.NamespaceFormat);

        var targetNamespace = jsonSerializableAttributeTypeArgument.ContainingNamespace.ToDisplayString(JsonConverterGenerator.NamespaceFormat);

        var discriminatorPropertyName = jsonPolymorphicConfiguration?.DiscriminatorPropertyName ?? DefaultTypeDiscriminatorPropertyName;

        const string prefix = "        ";

        var typeForDiscriminatorStringBuilder = new StringBuilder();
        var discriminatorForTypeStringBuilder = new StringBuilder();
        foreach (var derivedType in derivedTypes)
        {
            typeForDiscriminatorStringBuilder.Append(prefix);

            var fullTargetType = derivedType.TargetType.ToDisplayString(JsonConverterGenerator.NamespaceFormat);

            var discriminator = derivedType.Discriminator;
            if (discriminator is null)
            {
                discriminator = new DiscriminatorStruct<string> { Value = derivedType.TargetType.Name };
            }

            var (typeForDiscriminatorCase, discriminatorForTypeCase) = discriminator switch
            {
                DiscriminatorStruct<string> discriminatorString => (
                    $"DiscriminatorStruct<string> {{ Value: \"{discriminatorString.Value}\" }}",
                    $"    return new DiscriminatorStruct<string>(\"{discriminatorString.Value}\");"
                ),
                DiscriminatorStruct<int> discriminatorInt => (
                    $"DiscriminatorStruct<int> {{ Value: {discriminatorInt.Value} }}",
                    $"    return new DiscriminatorStruct<int>({discriminatorInt.Value});"
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
              using System;

              namespace {{targetNamespace}};

              internal class {{converterName}} : PolymorphicJsonConvertor<{{fullName}}>
              {
                  protected override ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "{{discriminatorPropertyName}}"u8;

                  protected override Type GetTypeForDiscriminator(
                      IDiscriminatorStruct discriminator
                  ) => discriminator switch
                  {
              {{typeForDiscriminatorStringBuilder}}
                      _ => throw new ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
                  };

                  protected override IDiscriminatorStruct GetDiscriminatorForType(Type type)
                  {
              {{discriminatorForTypeStringBuilder}}
                      throw new ArgumentOutOfRangeException(nameof(type), type, null);
                  }
              }
              """
        );
    }
}
