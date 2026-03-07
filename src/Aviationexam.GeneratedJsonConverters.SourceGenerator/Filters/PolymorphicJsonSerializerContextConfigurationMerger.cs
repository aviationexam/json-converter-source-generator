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
                .GroupBy(r => r.Result
                    .JsonSerializableCollection
                    .AsValueEnumerable()
                    .Select(c => c.JsonSerializableAttributeTypeArgument.ToDisplayString(
                        JsonPolymorphicConverterIncrementalGenerator.NamespaceFormatWithGenericArguments
                    ))
                    .JoinToString(',')
                )
                .Select(r => r.AsValueEnumerable().ToList())
                .ToList()
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var jsonSerializerContextClassTypes = new List<EquatableArray<ISymbol>>();
            PolymorphicJsonSerializerMetadata? jsonPolymorphicMetadata = null;
            var jsonSerializableCollections = new List<EquatableArray<JsonSerializableConfiguration>>();
            var jsonLeafSerializableCollections = new List<EquatableArray<JsonLeafSerializableConfiguration>>();
            var diagnostics = new List<EquatableArray<Diagnostic>>();
            foreach (var item in grouping)
            {
                jsonSerializerContextClassTypes.Add(item.Result.JsonSerializerContextClassType);
                jsonPolymorphicMetadata ??= item.Result.Metadata;
                jsonSerializableCollections.Add(item.Result.JsonSerializableCollection);
                jsonLeafSerializableCollections.Add(item.Result.JsonLeafSerializableCollection);
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
                    .SelectMany(static x => x)
                    .DistinctBy(c => c
                        .JsonSerializableAttributeTypeArgument.ToDisplayString(
                            JsonPolymorphicConverterIncrementalGenerator.NamespaceFormatWithGenericArguments
                        )
                    )
                    .ToArray()
                    .ToImmutableArray()
                    .AsEquatableArray(),
                jsonLeafSerializableCollections
                    .AsValueEnumerable()
                    .SelectMany(static x => x)
                    .DistinctBy(c => c
                        .LeafType.ToDisplayString(
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
