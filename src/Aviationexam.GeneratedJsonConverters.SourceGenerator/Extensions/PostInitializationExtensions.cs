using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Extensions;

internal static class PostInitializationExtensions
{
    public static void AddEmbeddedResources<TGenerator>(
        this IncrementalGeneratorPostInitializationContext initializationContext,
        IReadOnlyCollection<string> filenames
    ) where TGenerator : IIncrementalGenerator
    {
        var type = typeof(TGenerator);
        var generatorNamespace = type.Namespace!;

        var manifestResourceNames = type.Assembly.GetManifestResourceNames();

        foreach (var manifestResourceName in manifestResourceNames)
        {
            initializationContext.CancellationToken.ThrowIfCancellationRequested();

            var fileName = manifestResourceName[(generatorNamespace.Length + 1)..^3];

            if (!filenames.Contains(fileName))
            {
                continue;
            }

            var manifestResourceInfo = type.Assembly.GetManifestResourceStream(manifestResourceName);

            if (manifestResourceInfo is null)
            {
                continue;
            }

            initializationContext.AddSource(
                $"{fileName}.g.cs",
                SourceText.From(manifestResourceInfo, Encoding.UTF8, canBeEmbedded: true)
            );
        }
    }
}
