using Microsoft.CodeAnalysis;

namespace Aviationexam.JsonConverter.SourceGenerator;

internal sealed record JsonSerializableConfiguration(
    ITypeSymbol JsonSerializableAttributeTypeArgument
);
