using H.Generators;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Text;

namespace Aviationexam.JsonConverter.SourceGenerator.Generators;

public static class JsonPolymorphicConverterGenerator
{
    private const string DefaultTypeDiscriminatorPropertyName = "$type";

    private static readonly SymbolDisplayFormat NamespaceFormat = new(
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces
    );

    public static FileWithName Generate(
        JsonSerializableConfiguration jsonSerializableConfiguration,
        JsonPolymorphicConfiguration? jsonPolymorphicConfiguration,
        IReadOnlyCollection<JsonDerivedTypeConfiguration> derivedTypes
    )
    {
        var jsonSerializableAttributeTypeArgument = jsonSerializableConfiguration.JsonSerializableAttributeTypeArgument;
        var generateTame = $"{jsonSerializableAttributeTypeArgument.Name}JsonPolymorphicConverter";

        var fullName = jsonSerializableAttributeTypeArgument.ToDisplayString(NamespaceFormat);

        var targetNamespace = jsonSerializableAttributeTypeArgument.ContainingNamespace.ToDisplayString(NamespaceFormat);

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

            derivedTypeStringBuilder.Append($"\"{discriminator}\" => typeof({derivedType.TargetType.ToDisplayString(NamespaceFormat)}),");

            derivedTypeStringBuilder.AppendLine();
        }

        return new FileWithName(
            $"{generateTame}.g.cs",
            // language=cs
            $$"""
              using System;

              namespace {{targetNamespace}};

              internal class {{generateTame}} : PolymorphicJsonConvertor<{{fullName}}>
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
