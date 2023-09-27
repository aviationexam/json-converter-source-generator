using Microsoft.CodeAnalysis;

namespace Aviationexam.JsonConverter.SourceGenerator;

internal record JsonDerivedTypeConfiguration(
    ITypeSymbol TargetType,
    IDiscriminatorStruct? Discriminator
);
