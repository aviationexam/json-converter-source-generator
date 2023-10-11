using Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Contracts;
using Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.ContractWithCustomDelimiter;
using System.Text.Json.Serialization;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target;

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