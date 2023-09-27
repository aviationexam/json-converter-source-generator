using Aviationexam.JsonConverter.SourceGenerator.Target.Contracts;
using Aviationexam.JsonConverter.SourceGenerator.Target.ContractWithCustomDelimiter;
using System.Text.Json.Serialization;

namespace Aviationexam.JsonConverter.SourceGenerator.Target;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Default
)]
[JsonSerializable(typeof(BaseContract))]
[JsonSerializable(typeof(LeafContract))]
[JsonSerializable(typeof(AnotherLeafContract))]
[JsonSerializable(typeof(BaseContractWithCustomDelimiter))]
[JsonSerializable(typeof(LeafContractWithCustomDelimiter))]
public partial class MyJsonSerializerContext : JsonSerializerContext
{
    static MyJsonSerializerContext()
    {
        UsePolymorphicConverters(s_defaultOptions.Converters);
    }
}
