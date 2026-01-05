using Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.ContractWithCustomDelimiter;
using System;
using System.Text.Json;
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
    public void SerializeBaseContractWorks(BaseContractWithCustomDelimiter contract, string expectedJson)
    {
        var json = JsonSerializer.Serialize(
            contract,
            MyJsonSerializerContext.Default.Options
        );

        Assert.Equal(expectedJson, json);
    }
}
