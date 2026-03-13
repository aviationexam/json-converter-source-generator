using Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.ContractWithCustomDelimiter;
using System;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using VerifyTests;
using VerifyXunit;
using Xunit;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Tests;

public partial class BaseContractWithCustomDelimiterSerializationTests
{
    private JsonSerializerOptions CreateMySecondJsonSerializerOptions()
    {
        IJsonTypeInfoResolver jsonTypeInfoResolver = MySecondJsonSerializerContext.Default;
        foreach (var jsonTypeInfoConfiguration in MySecondJsonSerializerContext.GetPolymorphicJsonTypeInfoConfigurations())
        {
            jsonTypeInfoResolver = jsonTypeInfoResolver.WithAddedModifier(jsonTypeInfoConfiguration);
        }

        var jsonOptions = new JsonSerializerOptions(MySecondJsonSerializerContext.Default.Options)
        {
            TypeInfoResolver = jsonTypeInfoResolver,
        };

        return jsonOptions;
    }

    [Theory]
    [MemberData(nameof(BaseJsonContractData))]
    public void DeserializeBaseContractWorks_withMySecondJsonSerializerContext(string json, Type targetType)
    {
        var baseContract = JsonSerializer.Deserialize<BaseContractWithCustomDelimiter>(
            json,
            CreateMySecondJsonSerializerOptions()
        );

        Assert.NotNull(baseContract);
        Assert.Equal(1, baseContract.BaseProperty);
        Assert.IsType(targetType, baseContract);

        if (baseContract is LeafContractWithCustomDelimiter leafContract)
        {
            Assert.Equal(2, leafContract.LeafProperty);
        }
    }

    [Theory]
    [MemberData(nameof(LeafContractData))]
    public Task SerializeBaseContractWorks_withMySecondJsonSerializerContext(int testId, BaseContractWithCustomDelimiter contract)
    {
        var json = JsonSerializer.Serialize(
            contract,
            CreateMySecondJsonSerializerOptions()
        );

        var settings = new VerifySettings();
        settings.UseParameters(testId);

        return Verifier.VerifyJson(json, settings);
    }
}
