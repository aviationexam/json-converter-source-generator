using H.Generators.Extensions;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

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
                .GroupBy(r => string.Join(
                    ",",
                    r.Result
                        .JsonSerializableCollection
                        .Select(c => c.JsonSerializableAttributeTypeArgument.ToDisplayString(
                            JsonPolymorphicConverterIncrementalGenerator.NamespaceFormatWithGenericArguments
                        ))
                ))
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var jsonSerializerContextClassTypes = new List<EquatableArray<ISymbol>>();
            PolymorphicJsonSerializerMetadata? jsonPolymorphicMetadata = null;
            var jsonSerializableCollections = new List<EquatableArray<JsonSerializableConfiguration>>();
            var diagnostics = new List<EquatableArray<Diagnostic>>();
            foreach (var item in grouping)
            {
                jsonSerializerContextClassTypes.Add(item.Result.JsonSerializerContextClassType);
                jsonPolymorphicMetadata ??= item.Result.Metadata;
                jsonSerializableCollections.Add(item.Result.JsonSerializableCollection);
                diagnostics.Add(item.Diagnostics);
            }

            if (
                jsonPolymorphicMetadata is null
            )
            {
                continue;
            }

            yield return new PolymorphicJsonSerializerContextConfiguration(
                jsonSerializerContextClassTypes.SelectMany(static x => x).ToArray().ToImmutableArray().AsEquatableArray(),
                jsonPolymorphicMetadata,
                jsonSerializableCollections
                    .SelectMany(static x => x)
                    .GroupBy(c => c
                        .JsonSerializableAttributeTypeArgument.ToDisplayString(
                            JsonPolymorphicConverterIncrementalGenerator.NamespaceFormatWithGenericArguments
                        )
                    )
                    .Select(static x => x.First())
                    .ToArray()
                    .ToImmutableArray()
                    .AsEquatableArray()
            ).ToResultWithDiagnostics(diagnostics.SelectMany(static x => x).ToArray().ToImmutableArray().AsEquatableArray());
        }
    }
}
