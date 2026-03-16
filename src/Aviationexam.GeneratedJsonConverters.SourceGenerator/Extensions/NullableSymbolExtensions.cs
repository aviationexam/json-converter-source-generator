using Microsoft.CodeAnalysis;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Extensions;

/// <summary>
/// Based on <see href="https://github.com/riok/mapperly/blob/main/src/Riok.Mapperly/Helpers/NullableSymbolExtensions.cs"/>
/// </summary>
internal static class NullableSymbolExtensions
{
    internal static bool IsNullable(
        this ITypeSymbol symbol
    ) => symbol.IsNullableReferenceType() || symbol.IsNullableValueType();

    private static bool IsNullableReferenceType(
        this ITypeSymbol symbol
    ) => symbol.NullableAnnotation.IsNullable();

    private static bool IsNullableValueType(
        this ITypeSymbol symbol
    ) => symbol.NonNullableValueType() != null;

    private static ITypeSymbol? NonNullableValueType(
        this ITypeSymbol symbol
    )
    {
        if (
            symbol.IsValueType
            && symbol is INamedTypeSymbol { OriginalDefinition.SpecialType: SpecialType.System_Nullable_T } namedType
        )
        {
            return namedType.TypeArguments[0];
        }

        return null;
    }

    private static bool IsNullable(
        this NullableAnnotation nullable
    ) => nullable is NullableAnnotation.Annotated or NullableAnnotation.None;
}
