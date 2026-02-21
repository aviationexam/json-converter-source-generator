using Microsoft.CodeAnalysis;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

/// <summary>
/// Represents a leaf (concrete derived) type that should have a dedicated converter
/// which writes the type discriminator even when the type is serialized directly.
/// </summary>
internal sealed record JsonLeafSerializableConfiguration(
    ITypeSymbol LeafType,
    JsonPolymorphicConfiguration? PolymorphicConfiguration,
    IDiscriminatorStruct Discriminator
);
