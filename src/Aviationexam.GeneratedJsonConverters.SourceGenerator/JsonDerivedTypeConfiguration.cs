using Microsoft.CodeAnalysis;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

internal record JsonDerivedTypeConfiguration(
    ITypeSymbol TargetType,
    IDiscriminatorStruct? Discriminator
);
