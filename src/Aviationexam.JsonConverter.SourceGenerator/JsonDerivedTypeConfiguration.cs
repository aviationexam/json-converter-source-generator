using Microsoft.CodeAnalysis;

namespace Aviationexam.JsonConverter.SourceGenerator;

public record JsonDerivedTypeConfiguration(
    ITypeSymbol TargetType,
    string? Discriminator
);
