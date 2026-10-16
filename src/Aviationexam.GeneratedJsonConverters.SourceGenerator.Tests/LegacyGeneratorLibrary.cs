using Microsoft.CodeAnalysis;
using System;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Tests;

/// <summary>
/// A library built with generator 0.6.x or older, which emitted internal copies of the generator attributes into every assembly.
/// </summary>
public static class LegacyGeneratorLibrary
{
    private static readonly Lazy<MetadataReference> LazyReference = new(() => TestHelper.CreateLibraryReference(
        "LegacyLibrary",
        // ReSharper disable once HeapView.ObjectAllocation
        """
        using System;

        namespace Aviationexam.GeneratedJsonConverters.Attributes;

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
        internal sealed class JsonPolymorphicAttribute : System.Text.Json.Serialization.JsonAttribute
        {
            public string? TypeDiscriminatorPropertyName { get; set; }
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
        internal class JsonDerivedTypeAttribute : System.Text.Json.Serialization.JsonAttribute
        {
            public JsonDerivedTypeAttribute(Type derivedType)
            {
                DerivedType = derivedType;
            }

            public JsonDerivedTypeAttribute(Type derivedType, string typeDiscriminator)
            {
                DerivedType = derivedType;
                TypeDiscriminator = typeDiscriminator;
            }

            public JsonDerivedTypeAttribute(Type derivedType, int typeDiscriminator)
            {
                DerivedType = derivedType;
                TypeDiscriminator = typeDiscriminator;
            }

            public Type DerivedType { get; }

            public object? TypeDiscriminator { get; }
        }

        [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
        internal sealed class EnumJsonConverterAttribute : System.Text.Json.Serialization.JsonAttribute
        {
        }

        [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
        internal sealed class DisableEnumJsonConverterAttribute : System.Text.Json.Serialization.JsonAttribute
        {
        }
        """
    ));

    public static MetadataReference Reference => LazyReference.Value;
}
