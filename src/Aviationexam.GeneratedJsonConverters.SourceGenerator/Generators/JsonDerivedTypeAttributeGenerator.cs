using Microsoft.CodeAnalysis;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Generators;

internal static class JsonDerivedTypeAttributeGenerator
{
    public static void GenerateJsonDerivedTypeAttribute(
        this IncrementalGeneratorPostInitializationContext initializationContext
    ) => initializationContext.AddSource(
        "JsonDerivedTypeAttribute.g.cs",
        // language=cs
        """
        #nullable enable

        namespace Aviationexam.GeneratedJsonConverters.Attributes;

        /// <summary>
        /// This is a copy of System.Text.Json.Serialization.JsonDerivedTypeAttribute.
        /// It's purpose is to replace this attribute to silence System.Text.Json.Serialization.Metadata.PolymorphicTypeResolver{ThrowHelper.ThrowNotSupportedException_BaseConverterDoesNotSupportMetadata}
        ///
        /// When placed on a type declaration, indicates that the specified subtype should be opted into polymorphic serialization.
        /// </summary>
        [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
        public class JsonDerivedTypeAttribute : System.Text.Json.Serialization.JsonAttribute
        {
            /// <summary>
            /// Initializes a new attribute with specified parameters.
            /// </summary>
            /// <param name="derivedType">A derived type that should be supported in polymorphic serialization of the declared based type.</param>
            public JsonDerivedTypeAttribute(System.Type derivedType)
            {
                DerivedType = derivedType;
            }

            /// <summary>
            /// Initializes a new attribute with specified parameters.
            /// </summary>
            /// <param name="derivedType">A derived type that should be supported in polymorphic serialization of the declared base type.</param>
            /// <param name="typeDiscriminator">The type discriminator identifier to be used for the serialization of the subtype.</param>
            public JsonDerivedTypeAttribute(System.Type derivedType, string typeDiscriminator)
            {
                DerivedType = derivedType;
                TypeDiscriminator = typeDiscriminator;
            }

            /// <summary>
            /// Initializes a new attribute with specified parameters.
            /// </summary>
            /// <param name="derivedType">A derived type that should be supported in polymorphic serialization of the declared base type.</param>
            /// <param name="typeDiscriminator">The type discriminator identifier to be used for the serialization of the subtype.</param>
            public JsonDerivedTypeAttribute(System.Type derivedType, int typeDiscriminator)
            {
                DerivedType = derivedType;
                TypeDiscriminator = typeDiscriminator;
            }

            /// <summary>
            /// A derived type that should be supported in polymorphic serialization of the declared base type.
            /// </summary>
            public System.Type DerivedType { get; }

            /// <summary>
            /// The type discriminator identifier to be used for the serialization of the subtype.
            /// </summary>
            public object? TypeDiscriminator { get; }
        }
        """
    );
}
