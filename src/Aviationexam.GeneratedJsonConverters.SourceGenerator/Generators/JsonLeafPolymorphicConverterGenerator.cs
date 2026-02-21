using H.Generators.Extensions;
using Microsoft.CodeAnalysis;
using System;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Generators;

internal static class JsonLeafPolymorphicConverterGenerator
{
    private const string DefaultTypeDiscriminatorPropertyName = "$type";

    public static FileWithName Generate(
        string targetNamespace,
        JsonLeafSerializableConfiguration leafConfiguration,
        out string converterName
    )
    {
        var leafType = leafConfiguration.LeafType;
        converterName = GenerateConverterName(leafType);

        var fullName = leafType.ToDisplayString(JsonPolymorphicConverterIncrementalGenerator.NamespaceFormatWithGenericArguments);

        var discriminatorPropertyName = leafConfiguration.PolymorphicConfiguration?.DiscriminatorPropertyName
            ?? DefaultTypeDiscriminatorPropertyName;

        var writeDiscriminatorBody = leafConfiguration.Discriminator switch
        {
            DiscriminatorStruct<string> discriminatorString =>
                $"writer.WriteString(GetDiscriminatorPropertyName(), \"{discriminatorString.Value}\");",
            DiscriminatorStruct<int> discriminatorInt =>
                $"writer.WriteNumber(GetDiscriminatorPropertyName(), {discriminatorInt.Value});",
            _ => throw new ArgumentOutOfRangeException(
                nameof(leafConfiguration.Discriminator),
                leafConfiguration.Discriminator,
                null
            ),
        };

        return new FileWithName(
            $"{converterName}.g.cs",
            // language=cs
            $$"""
              #nullable enable

              namespace {{targetNamespace}};

              internal class {{converterName}} : Aviationexam.GeneratedJsonConverters.LeafPolymorphicJsonConvertor<{{fullName}}>
              {
                  protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "{{discriminatorPropertyName}}"u8;

                  protected override void WriteDiscriminatorValue(System.Text.Json.Utf8JsonWriter writer)
                  {
                      {{writeDiscriminatorBody}}
                  }
              }
              """
        );
    }

    private static string GenerateConverterName(ITypeSymbol leafType)
    {
        var sourceTypeName = leafType.Name;

        if (leafType is INamedTypeSymbol { TypeArguments: { Length: > 0 } typeArguments })
        {
            var sb = new System.Text.StringBuilder(sourceTypeName);
            sb.Append("Of");

            for (var i = 0; i < typeArguments.Length; i++)
            {
                sb.Append(typeArguments[i].Name);

                if (i + 1 < typeArguments.Length)
                {
                    sb.Append("And");
                }
            }

            sourceTypeName = sb.ToString();
        }

        return $"{sourceTypeName}LeafJsonPolymorphicConverter";
    }
}
