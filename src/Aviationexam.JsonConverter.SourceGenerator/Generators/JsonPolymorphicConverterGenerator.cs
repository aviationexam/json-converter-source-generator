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

        var derivedTypeStringBuilder = new StringBuilder();
        foreach (var derivedType in derivedTypes)
        {
            derivedTypeStringBuilder.Append(prefix);

            var discriminator = derivedType.Discriminator;
            if (discriminator is null)
            {
                discriminator = new DiscriminatorStruct<string> { Value = derivedType.TargetType.Name };
            }

            var discriminatorCase = discriminator switch
            {
                DiscriminatorStruct<string> discriminatorString => $"DiscriminatorStruct<string> {{ Value: \"{discriminatorString.Value}\" }}",
                DiscriminatorStruct<int> discriminatorInt => $"DiscriminatorStruct<int> {{ Value: {discriminatorInt.Value} }}",
                _ => throw new ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
            };

            derivedTypeStringBuilder.Append(
                $"{discriminatorCase} => typeof({derivedType.TargetType.ToDisplayString(JsonConverterGenerator.NamespaceFormat)}),"
            );

            derivedTypeStringBuilder.AppendLine();
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
              {{derivedTypeStringBuilder}}
                      _ => throw new ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
                  };
              }
              """
        );
    }
}
