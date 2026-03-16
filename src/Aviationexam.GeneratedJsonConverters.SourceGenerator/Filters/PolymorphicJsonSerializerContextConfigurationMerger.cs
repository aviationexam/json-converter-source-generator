using H.Generators.Extensions;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using ZLinq;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Filters;

internal static class PolymorphicJsonSerializerContextConfigurationMerger
{
    public static IncrementalValuesProvider<ResultWithDiagnostics<PolymorphicJsonSerializerContextConfiguration>> MergeJsonSerializerContextConfiguration(
        this IncrementalValuesProvider<ResultWithDiagnostics<PolymorphicJsonSerializerContextConfiguration>> valuesProvider
    ) => valuesProvider.CollectAsEquatableArray()
        .SelectMany(MergePolymorphicJsonSerializerContextConfigurations);

    private static IEnumerable<ResultWithDiagnostics<PolymorphicJsonSerializerContextConfiguration>> MergePolymorphicJsonSerializerContextConfigurations(
        EquatableArray<ResultWithDiagnostics<PolymorphicJsonSerializerContextConfiguration>> resultWithDiagnostics,
        CancellationToken cancellationToken
    )
    {
        foreach (
            var grouping in
            resultWithDiagnostics
                .AsValueEnumerable()
                .SelectMany(r => r.Result
                    .JsonSerializableCollection
                    .AsValueEnumerable()
                    .Select(c => (
                        r.Result.JsonSerializerContextClassType,
                        r.Result.Metadata,
                        r.Diagnostics,
                        Serializable: c
                    ))
                )
                .GroupBy(c => c.Serializable.JsonSerializableAttributeTypeArgument.ToDisplayString(
                    JsonPolymorphicConverterIncrementalGenerator.NamespaceFormatWithGenericArguments
                ))
                .Select(r => r.AsValueEnumerable().ToList())
                .ToList()
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var jsonSerializerContextClassTypes = new List<EquatableArray<ISymbol>>();
            PolymorphicJsonSerializerMetadata? jsonPolymorphicMetadata = null;
            var jsonSerializableCollections = new List<JsonSerializableConfiguration>();
            var diagnostics = new List<EquatableArray<Diagnostic>>();
            foreach (var item in grouping)
            {
                jsonSerializerContextClassTypes.Add(item.JsonSerializerContextClassType);
                jsonPolymorphicMetadata ??= item.Metadata;
                jsonSerializableCollections.Add(item.Serializable);
                diagnostics.Add(item.Diagnostics);
            }

            if (
                jsonPolymorphicMetadata is null
            )
            {
                continue;
            }

            yield return new PolymorphicJsonSerializerContextConfiguration(
                jsonSerializerContextClassTypes.AsValueEnumerable().SelectMany(static x => x).ToArray().ToImmutableArray().AsEquatableArray(),
                jsonPolymorphicMetadata,
                jsonSerializableCollections
                    .AsValueEnumerable()
                    .DistinctBy(c => c
                        .JsonSerializableAttributeTypeArgument.ToDisplayString(
                            JsonPolymorphicConverterIncrementalGenerator.NamespaceFormatWithGenericArguments
                        )
                    )
                    .ToArray()
                    .ToImmutableArray()
                    .AsEquatableArray()
            ).ToResultWithDiagnostics(diagnostics.AsValueEnumerable().SelectMany(static x => x).ToArray().ToImmutableArray().AsEquatableArray());
        }
    }
}
