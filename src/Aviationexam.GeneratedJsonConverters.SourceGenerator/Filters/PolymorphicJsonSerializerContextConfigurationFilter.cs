using Aviationexam.GeneratedJsonConverters.SourceGenerator.Parsers;
using H.Generators.Extensions;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Filters;

internal static class PolymorphicJsonSerializerContextConfigurationFilter
{
    public static ResultWithDiagnostics<PolymorphicJsonSerializerContextConfiguration> FilterJsonSerializerContextConfiguration(
        ResultWithDiagnostics<PolymorphicJsonSerializerContextConfiguration> x, CancellationToken cancellationToken
    )
    {
        if (
            x.Result is
            {
                JsonSerializableCollection: { IsEmpty: false } jsonConverterConfigurations,
                Metadata.JsonPolymorphicAttributeSymbol: { } jsonPolymorphicAttributeSymbol,
                Metadata.JsonDerivedTypeAttributeSymbol: { } jsonDerivedTypeAttributeSymbol,
            }
        )
        {
            var filteredJsonSerializableCollection = new List<JsonSerializableConfiguration>(jsonConverterConfigurations.AsImmutableArray().Length);
            var leafSerializableCollection = new List<JsonLeafSerializableConfiguration>();

            // First pass: collect all base types (those with [JsonPolymorphic]) and build a
            // map from leaf-type-full-name -> (discriminatorPropertyName, discriminatorValue).
            var derivedTypeMap = new Dictionary<string, (JsonPolymorphicConfiguration? PolymorphicConfig, IDiscriminatorStruct Discriminator)>(
                System.StringComparer.Ordinal
            );

            foreach (var jsonConverterConfiguration in jsonConverterConfigurations)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var typeSymbol = jsonConverterConfiguration.JsonSerializableAttributeTypeArgument;
                var typeAttributes = typeSymbol.GetAttributes();

                JsonPolymorphicConfiguration? polymorphicConfiguration = null;
                var hasJsonPolymorphicAttribute = false;

                foreach (var attr in typeAttributes)
                {
                    if (SymbolEqualityComparer.Default.Equals(attr.AttributeClass, jsonPolymorphicAttributeSymbol))
                    {
                        hasJsonPolymorphicAttribute = true;
                        polymorphicConfiguration = JsonPolymorphicAttributeParser.Parse(attr);
                        filteredJsonSerializableCollection.Add(jsonConverterConfiguration);
                    }
                }

                // If this type has [JsonPolymorphic], collect all its [JsonDerivedType] entries
                if (hasJsonPolymorphicAttribute)
                {
                    foreach (var attr in typeAttributes)
                    {
                        if (
                            SymbolEqualityComparer.Default.Equals(attr.AttributeClass, jsonDerivedTypeAttributeSymbol)
                            || (
                                attr.AttributeClass is { IsGenericType: true } attributeClass
                                && SymbolEqualityComparer.Default.Equals(attributeClass.BaseType, jsonDerivedTypeAttributeSymbol)
                            )
                        )
                        {
                            var derivedTypeConfig = JsonDerivedTypeAttributeParser.Parse(attr);
                            if (derivedTypeConfig is null)
                            {
                                continue;
                            }

                            var discriminator = derivedTypeConfig.Discriminator
                                ?? new DiscriminatorStruct<string>(derivedTypeConfig.TargetType.Name);

                            var leafKey = derivedTypeConfig.TargetType.ToDisplayString(
                                JsonPolymorphicConverterIncrementalGenerator.NamespaceFormatWithGenericArguments
                            );

                            // Last-writer-wins if a leaf appears in multiple hierarchies, but that
                            // would be unusual; in practice each leaf belongs to one base type.
                            derivedTypeMap[leafKey] = (polymorphicConfiguration, discriminator);
                        }
                    }
                }
            }

            // Second pass: for every [JsonSerializable] type that is NOT a base polymorphic type
            // but IS a known derived/leaf type, generate a leaf converter for it.
            foreach (var jsonConverterConfiguration in jsonConverterConfigurations)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var typeSymbol = jsonConverterConfiguration.JsonSerializableAttributeTypeArgument;

                // Skip types that already have [JsonPolymorphic] — they are handled by the base converter.
                var isPolymorphicBase = false;
                foreach (var attr in typeSymbol.GetAttributes())
                {
                    if (SymbolEqualityComparer.Default.Equals(attr.AttributeClass, jsonPolymorphicAttributeSymbol))
                    {
                        isPolymorphicBase = true;
                        break;
                    }
                }

                if (isPolymorphicBase)
                {
                    continue;
                }

                var leafKey = typeSymbol.ToDisplayString(
                    JsonPolymorphicConverterIncrementalGenerator.NamespaceFormatWithGenericArguments
                );

                if (derivedTypeMap.TryGetValue(leafKey, out var leafInfo))
                {
                    leafSerializableCollection.Add(new JsonLeafSerializableConfiguration(
                        typeSymbol,
                        leafInfo.PolymorphicConfig,
                        leafInfo.Discriminator
                    ));
                }
            }

            var filteredConfiguration = x.Result with
            {
                JsonSerializableCollection = filteredJsonSerializableCollection.ToImmutableArray().AsEquatableArray(),
                JsonLeafSerializableCollection = leafSerializableCollection.ToImmutableArray().AsEquatableArray(),
            };

            return filteredConfiguration.ToResultWithDiagnostics(x.Diagnostics);
        }

        return x.Result.ToResultWithDiagnostics(x.Diagnostics);
    }
}
