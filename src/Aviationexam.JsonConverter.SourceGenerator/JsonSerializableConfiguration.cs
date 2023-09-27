using Microsoft.CodeAnalysis;

namespace Aviationexam.JsonConverter.SourceGenerator;

public sealed record JsonSerializableConfiguration(
    ITypeSymbol JsonSerializableAttributeTypeArgument
);
