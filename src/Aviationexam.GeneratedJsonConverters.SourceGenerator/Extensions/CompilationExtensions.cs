using Microsoft.CodeAnalysis;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Extensions;

internal static class CompilationExtensions
{
    /// <summary>
    /// Resolves a type like <see cref="Compilation.GetTypeByMetadataName"/>, but tolerates the same name being declared in several assemblies.
    /// Libraries built with generator 0.6.x and older carry their own internal copies of the generator attributes,
    /// which makes <see cref="Compilation.GetTypeByMetadataName"/> return null once such a library is referenced.
    /// In that case the single candidate accessible from the current assembly wins.
    /// </summary>
    public static INamedTypeSymbol? GetAccessibleTypeByMetadataName(
        this Compilation compilation, string fullyQualifiedMetadataName
    )
    {
        if (compilation.GetTypeByMetadataName(fullyQualifiedMetadataName) is { } typeSymbol)
        {
            return typeSymbol;
        }

        INamedTypeSymbol? accessibleCandidate = null;
        foreach (var candidate in compilation.GetTypesByMetadataName(fullyQualifiedMetadataName))
        {
            if (!compilation.IsSymbolAccessibleWithin(candidate, compilation.Assembly))
            {
                continue;
            }

            if (accessibleCandidate is not null)
            {
                return null;
            }

            accessibleCandidate = candidate;
        }

        return accessibleCandidate;
    }
}
