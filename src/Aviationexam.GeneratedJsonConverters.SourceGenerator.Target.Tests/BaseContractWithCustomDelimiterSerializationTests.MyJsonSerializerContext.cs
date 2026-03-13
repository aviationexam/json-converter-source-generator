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
    private JsonSerializerOptions CreateMyJsonSerializerOptions()
    {
        IJsonTypeInfoResolver jsonTypeInfoResolver = MyJsonSerializerContext.Default;
        foreach (var jsonTypeInfoConfiguration in MyJsonSerializerContext.GetPolymorphicJsonTypeInfoConfigurations())
        {
            jsonTypeInfoResolver = jsonTypeInfoResolver.WithAddedModifier(jsonTypeInfoConfiguration);
        }

        var jsonOptions = new JsonSerializerOptions(MyJsonSerializerContext.Default.Options)
        {
            TypeInfoResolver = jsonTypeInfoResolver,
        };

        return jsonOptions;
    }

    [Theory]
    [MemberData(nameof(BaseJsonContractData))]
    public void DeserializeBaseContractWorks(string json, Type targetType)
    {
        var baseContract = JsonSerializer.Deserialize<BaseContractWithCustomDelimiter>(
            json,
            CreateMyJsonSerializerOptions()
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
    public Task SerializeBaseContractWorks(int testId, BaseContractWithCustomDelimiter contract)
    {
        var json = JsonSerializer.Serialize(
            contract,
            CreateMyJsonSerializerOptions()
        );

        var settings = new VerifySettings();
        settings.UseParameters(testId);

        return Verifier.VerifyJson(json, settings);
    }

    [Fact]
    public Task SerializeLeafContractWithCustomDelimiterWorks()
    {
        var json = JsonSerializer.Serialize(
            new LeafContractWithCustomDelimiter
            {
                BaseProperty = 1,
                LeafProperty = 2
            },
            CreateMyJsonSerializerOptions()
        );

        return Verifier.VerifyJson(json);
    }
}
