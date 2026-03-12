using H.Generators.Extensions;
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

        const string halfPrefix = "    ";
        const string prefix = $"{halfPrefix}{halfPrefix}";

        var typeForDiscriminatorStringBuilder = new StringBuilder();
        var discriminatorForInstanceStringBuilder = new StringBuilder();
        var configureJsonTypeInfoStringBuilder = new StringBuilder();
        foreach (var derivedType in derivedTypes)
        {
            typeForDiscriminatorStringBuilder.Append(prefix);

            var fullTargetType = derivedType.TargetType.ToDisplayString(JsonPolymorphicConverterIncrementalGenerator.NamespaceFormat);

            var discriminator = derivedType.Discriminator;
            if (discriminator is null)
            {
                discriminator = new DiscriminatorStruct<string> { Value = derivedType.TargetType.Name };
            }

            var (typeForDiscriminatorCase, discriminatorForTypeCase, discriminatorType, discriminatorValue) = discriminator switch
            {
                DiscriminatorStruct<string> discriminatorString => (
                    $"Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> {{ Value: \"{discriminatorString.Value}\" }}",
                    $"    return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>(\"{discriminatorString.Value}\");",
                    "string",
                    $"\"{discriminatorString.Value}\""
                ),
                DiscriminatorStruct<int> discriminatorInt => (
                    $"Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<int> {{ Value: {discriminatorInt.Value} }}",
                    $"{halfPrefix}return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<int>({discriminatorInt.Value});",
                    "int",
                    $"{discriminatorInt.Value}"
                ),
                _ => throw new ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
            };

            typeForDiscriminatorStringBuilder.Append(
                $"{typeForDiscriminatorCase} => typeof({fullTargetType}),"
            );

            typeForDiscriminatorStringBuilder.AppendLine();

            discriminatorForInstanceStringBuilder.Append(prefix);
            discriminatorForInstanceStringBuilder.AppendLine($"if (instance is {fullTargetType})");
            discriminatorForInstanceStringBuilder.Append(prefix);
            discriminatorForInstanceStringBuilder.AppendLine("{");
            discriminatorForInstanceStringBuilder.Append(prefix);
            discriminatorForInstanceStringBuilder.AppendLine($"{halfPrefix}targetType = typeof({fullTargetType});");
            discriminatorForInstanceStringBuilder.AppendLine();
            discriminatorForInstanceStringBuilder.Append(prefix);
            discriminatorForInstanceStringBuilder.AppendLine(discriminatorForTypeCase);
            discriminatorForInstanceStringBuilder.Append(prefix);
            discriminatorForInstanceStringBuilder.AppendLine("}");

            configureJsonTypeInfoStringBuilder.AppendLine(
                // language=cs
                $$"""

                if (jsonTypeInfo.Type == typeof({{fullTargetType}}) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
                {
                    jsonTypeInfo.Properties.Add(System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                        jsonTypeInfo.Options,
                        new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<{{discriminatorType}}>
                        {
                            IsProperty = false,
                            IsPublic = true,
                            IsVirtual = true,
                            DeclaringType = typeof({{fullTargetType}}),
                            Converter = null,
                            Getter = static _ => {{discriminatorValue}},
                            Setter = null,
                            IgnoreCondition = null,
                            HasJsonInclude = false,
                            IsExtensionData = false,
                            NumberHandling = null,
                            PropertyName = "__jsonTypeDiscriminator",
                            JsonPropertyName = "{{discriminatorPropertyName}}"
                        }
                    ));
                }
                """.Replace("\n", $"\n{prefix}")
            );
        }

        return new FileWithName(
            $"{converterName}.g.cs",
            // language=cs
            $$"""
              #nullable enable

              namespace {{targetNamespace}};

              internal class {{converterName}} :
                  Aviationexam.GeneratedJsonConverters.PolymorphicJsonConvertor<{{converterName}}, {{fullName}}>,
                  Aviationexam.GeneratedJsonConverters.IPolymorphicJsonConvertor<{{fullName}}>
              {
                  #if !NET7_0_OR_GREATER

                  protected override System.ReadOnlySpan<byte> Self_GetDiscriminatorPropertyName() => GetDiscriminatorPropertyName();

                  protected override System.Type Self_GetTypeForDiscriminator(
                      Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct discriminator
                  ) => GetTypeForDiscriminator(discriminator);

                  protected override Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct Self_GetDiscriminatorForInstance<TInstance>(
                      TInstance instance, out System.Type targetType
                  ) => GetDiscriminatorForInstance<TInstance>(instance, out targetType);

                  #endif

                  public static System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "{{discriminatorPropertyName}}"u8;

                  public static System.Type GetTypeForDiscriminator(
                      Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct discriminator
                  ) => discriminator switch
                  {
              {{typeForDiscriminatorStringBuilder}}
                      _ => throw new System.ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
                  };

                  public static Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct GetDiscriminatorForInstance<TInstance>(
                      TInstance instance, out System.Type targetType
                  ) where TInstance : {{fullName}}
                  {
              {{discriminatorForInstanceStringBuilder}}
                      throw new System.ArgumentOutOfRangeException(nameof(instance), instance, null);
                  }

                  public static void ConfigureJsonTypeInfo(System.Text.Json.Serialization.Metadata.JsonTypeInfo jsonTypeInfo)
                  {
               {{configureJsonTypeInfoStringBuilder}}
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
