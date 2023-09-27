using Aviationexam.JsonConverter.SourceGenerator.Target.ContractWithCustomDelimiter;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;

namespace Aviationexam.JsonConverter.SourceGenerator.Target.Tests;

public class BaseContractWithCustomDelimiterSerializationTests
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

    public static IEnumerable<object[]> BaseJsonContractData()
    {
        yield return new object[]
        {
            // language=json
            """
            {
                "myCustomDelimiter": "LeafContractWithCustomDelimiter",
                "baseProperty": 1,
                "leafProperty": 2
            }
            """,
            typeof(LeafContractWithCustomDelimiter),
        };
        yield return new object[]
        {
            // language=json
            """
            {
                "baseProperty": 1,
                "myCustomDelimiter": "LeafContractWithCustomDelimiter",
                "leafProperty": 2
            }
            """,
            typeof(LeafContractWithCustomDelimiter),
        };
        yield return new object[]
        {
            // language=json
            """
            {
                "baseProperty": 1,
                "leafProperty": 2,
                "myCustomDelimiter": "LeafContractWithCustomDelimiter"
            }
            """,
            typeof(LeafContractWithCustomDelimiter),
        };
    }

    public static IEnumerable<object[]> LeafContractData()
    {
        yield return new object[]
        {
            new LeafContractWithCustomDelimiter
            {
                BaseProperty = 1,
                LeafProperty = 2
            },
            // language=json
            """
            {
              "myCustomDelimiter": "LeafContractWithCustomDelimiter",
              "leafProperty": 2,
              "baseProperty": 1
            }
            """.Replace("\r\n", Environment.NewLine)
        };
    }
}
