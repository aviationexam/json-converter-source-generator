using Microsoft.CodeAnalysis;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Generators;

internal static class EnumJsonConverterAttributeGenerator
{
    public const string SerializationStrategyPropertyName = "SerializationStrategy";
    public const string DeserializationStrategyPropertyName = "DeserializationStrategy";

    public static void GenerateEnumJsonConverterAttribute(
        this IncrementalGeneratorPostInitializationContext initializationContext
    ) => initializationContext.AddSource(
        "EnumJsonConverterAttribute.g.cs",
        // language=cs
        $$"""
          #nullable enable

          namespace Aviationexam.GeneratedJsonConverters.Attributes;

          /// <summary>
          /// When placed on an enum, indicates that the type should be serialized using generated enum convertor.
          /// </summary>
          [System.AttributeUsage(System.AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
          internal sealed class EnumJsonConverterAttribute : System.Text.Json.Serialization.JsonAttribute
          {
              /// <summary>
              /// Configure serialization strategy
              /// </summary>
              public EnumSerializationStrategy {{SerializationStrategyPropertyName}} { get; set; } = EnumSerializationStrategy.ProjectDefault;

              /// <summary>
              /// Configure deserialization strategy
              /// </summary>
              public EnumDeserializationStrategy {{DeserializationStrategyPropertyName}} { get; set; } = EnumDeserializationStrategy.ProjectDefault;
          }
          """
    );
}
