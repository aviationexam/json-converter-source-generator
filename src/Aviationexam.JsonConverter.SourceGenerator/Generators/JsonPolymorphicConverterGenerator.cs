using H.Generators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aviationexam.JsonConverter.SourceGenerator.Generators;

public static class JsonPolymorphicConverterGenerator
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
        converterName = $"{jsonSerializableAttributeTypeArgument.Name}JsonPolymorphicConverter";

        var fullName = jsonSerializableAttributeTypeArgument.ToDisplayString(JsonConverterGenerator.NamespaceFormat);

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
                    $"Aviationexam.JsonConverter.SourceGenerator.DiscriminatorStruct<string> {{ Value: \"{discriminatorString.Value}\" }}",
                    $"    return new Aviationexam.JsonConverter.SourceGenerator.DiscriminatorStruct<string>(\"{discriminatorString.Value}\");"
                ),
                DiscriminatorStruct<int> discriminatorInt => (
                    $"Aviationexam.JsonConverter.SourceGenerator.DiscriminatorStruct<int> {{ Value: {discriminatorInt.Value} }}",
                    $"    return new Aviationexam.JsonConverter.SourceGenerator.DiscriminatorStruct<int>({discriminatorInt.Value});"
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

              internal class {{converterName}} : Aviationexam.JsonConverter.SourceGenerator.PolymorphicJsonConvertor<{{fullName}}>
              {
                  protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "{{discriminatorPropertyName}}"u8;

                  protected override System.Type GetTypeForDiscriminator(
                      Aviationexam.JsonConverter.SourceGenerator.IDiscriminatorStruct discriminator
                  ) => discriminator switch
                  {
              {{typeForDiscriminatorStringBuilder}}
                      _ => throw new System.ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
                  };

                  protected override Aviationexam.JsonConverter.SourceGenerator.IDiscriminatorStruct GetDiscriminatorForType(
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
}
