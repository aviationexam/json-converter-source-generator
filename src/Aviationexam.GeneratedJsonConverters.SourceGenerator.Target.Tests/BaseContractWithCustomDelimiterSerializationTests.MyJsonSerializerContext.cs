using Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.ContractWithCustomDelimiter;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using VerifyTests;
using VerifyXunit;
using Xunit;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Tests;

public partial class BaseContractWithCustomDelimiterSerializationTests
{
    [Theory]
    [MemberData(nameof(BaseJsonContractData))]
    public void DeserializeBaseContractWorks(string json, Type targetType)
    {
        var baseContract = JsonSerializer.Deserialize<BaseContractWithCustomDelimiter>(
            json,
            MyJsonSerializerContext.Default.Options
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
            MyJsonSerializerContext.Default.Options
        );

        var settings = new VerifySettings();
        settings.UseParameters(testId);

        return Verifier.VerifyJson(json, settings);
    }
}
