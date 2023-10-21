using Microsoft.CodeAnalysis;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Generators;

internal static class DisableEnumJsonConverterAttributeGenerator
{
    public static void GenerateDisableEnumJsonConverterAttribute(
        this IncrementalGeneratorPostInitializationContext initializationContext
    ) => initializationContext.AddSource(
        "DisableEnumJsonConverterAttribute.g.cs",
        // language=cs
        """
        #nullable enable

        namespace Aviationexam.GeneratedJsonConverters.Attributes;

        /// <summary>
        /// When placed on an enum, indicates that generator should not report missing <see cref="EnumJsonConverterAttribute" />
        /// </summary>
        [System.AttributeUsage(System.AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
        internal sealed class DisableEnumJsonConverterAttribute : System.Text.Json.Serialization.JsonAttribute
        {
        }
        """
    );
}
