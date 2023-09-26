using H.Generators;
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
                discriminator = derivedType.TargetType.Name;
            }

            derivedTypeStringBuilder.Append(
                $"\"{discriminator}\" => typeof({derivedType.TargetType.ToDisplayString(JsonConverterGenerator.NamespaceFormat)}),"
            );

            derivedTypeStringBuilder.AppendLine();
        }

        return new FileWithName(
            $"{converterName}.g.cs",
            // language=cs
            $$"""
              using System;

              namespace {{targetNamespace}};

              internal class {{converterName}} : PolymorphicJsonConvertor<{{fullName}}>
              {
                  protected override ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "{{discriminatorPropertyName}}"u8;

                  protected override Type GetTypeForDiscriminator(
                      string discriminator
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
