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
                Metadata.JsonPolymorphicAttributeSymbol: { } jsonPolymorphicAttributeSymbol
            }
        )
        {
            var filteredJsonSerializableCollection = new List<JsonSerializableConfiguration>(jsonConverterConfigurations.AsImmutableArray().Length);

            foreach (var jsonConverterConfiguration in jsonConverterConfigurations)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var jsonSerializableTargetAttributes = jsonConverterConfiguration.JsonSerializableAttributeTypeArgument.GetAttributes();

                foreach (var jsonSerializableTargetAttribute in jsonSerializableTargetAttributes)
                {
                    if (SymbolEqualityComparer.Default.Equals(jsonSerializableTargetAttribute.AttributeClass, jsonPolymorphicAttributeSymbol))
                    {
                        filteredJsonSerializableCollection.Add(jsonConverterConfiguration);
                    }
                }
            }

            var filteredConfiguration = x.Result with
            {
                JsonSerializableCollection = filteredJsonSerializableCollection.ToImmutableArray().AsEquatableArray()
            };

            return filteredConfiguration.ToResultWithDiagnostics(x.Diagnostics);
        }

        return x.Result.ToResultWithDiagnostics(x.Diagnostics);
    }
}
