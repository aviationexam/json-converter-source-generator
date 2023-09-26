using System.Text.Json.Serialization;

namespace Aviationexam.JsonConverter.SourceGenerator.Target;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Default
)]
[JsonSerializable(typeof(BaseContract))]
public partial class MyJsonSerializerContext : JsonSerializerContext
{
    static MyJsonSerializerContext()
    {
        UsePolymorphicConverters(s_defaultOptions.Converters);
    }
}
