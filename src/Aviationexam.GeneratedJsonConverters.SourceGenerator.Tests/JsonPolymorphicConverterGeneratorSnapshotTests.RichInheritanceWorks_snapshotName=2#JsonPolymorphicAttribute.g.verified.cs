﻿//HintName: JsonPolymorphicAttribute.g.cs
#nullable enable

namespace Aviationexam.GeneratedJsonConverters.Attributes;

/// <summary>
/// This is a copy of System.Text.Json.Serialization.JsonPolymorphicAttribute.
/// It's purpose is to replace this attribute to silence System.Text.Json.Serialization.Metadata.PolymorphicTypeResolver{ThrowHelper.ThrowNotSupportedException_BaseConverterDoesNotSupportMetadata}
///
/// When placed on a type, indicates that the type should be serialized polymorphically.
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
internal sealed class JsonPolymorphicAttribute : System.Text.Json.Serialization.JsonAttribute
{
    /// <summary>
    /// Gets or sets a custom type discriminator property name for the polymorhic type.
    /// Uses the default '$type' property name if left unset.
    /// </summary>
    public string? TypeDiscriminatorPropertyName { get; set; }

    /// <summary>
    /// Gets or sets the behavior when serializing an undeclared derived runtime type.
    /// </summary>
    public System.Text.Json.Serialization.JsonUnknownDerivedTypeHandling UnknownDerivedTypeHandling { get; set; }

    /// <summary>
    /// When set to <see langword="true"/>, instructs the deserializer to ignore any
    /// unrecognized type discriminator id's and reverts to the contract of the base type.
    /// Otherwise, it will fail the deserialization.
    /// </summary>
    public bool IgnoreUnrecognizedTypeDiscriminators { get; set; }
}