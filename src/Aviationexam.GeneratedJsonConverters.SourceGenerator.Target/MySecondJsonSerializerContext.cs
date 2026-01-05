using Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Contracts;
using Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.ContractWithCustomDelimiter;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Default,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
)]
[JsonSerializable(typeof(BaseContract))]
[JsonSerializable(typeof(LeafContract))]
[JsonSerializable(typeof(AnotherLeafContract))]
[JsonSerializable(typeof(GenericLeafContract))]
[JsonSerializable(typeof(BaseContractWithCustomDelimiter))]
[JsonSerializable(typeof(LeafContractWithCustomDelimiter))]
[JsonSerializable(typeof(NullableLeafContractWithCustomDelimiter))]
[JsonSerializable(typeof(EBackingEnum))]
[JsonSerializable(typeof(EConfiguredPropertyEnum))]
[JsonSerializable(typeof(EDuplicatedValueUsingBackingTypeEnum))]
[JsonSerializable(typeof(EDuplicatedValueUsingFirstEnumNameEnum))]
[JsonSerializable(typeof(EMyEnum))]
[JsonSerializable(typeof(EPropertyEnum))]
[JsonSerializable(typeof(EPropertyWithBackingEnum))]
[JsonSerializable(typeof(IReadOnlyDictionary<EBackingEnum, int>))]
[JsonSerializable(typeof(IReadOnlyDictionary<EConfiguredPropertyEnum, int>))]
public partial class MySecondJsonSerializerContext : JsonSerializerContext
{
    static MySecondJsonSerializerContext()
    {
        UsePolymorphicConverters(s_defaultOptions.Converters);
        MyJsonSerializerContext.UseEnumConverters(s_defaultOptions.Converters);

        Default = new MySecondJsonSerializerContext(new JsonSerializerOptions(s_defaultOptions));
    }
}
