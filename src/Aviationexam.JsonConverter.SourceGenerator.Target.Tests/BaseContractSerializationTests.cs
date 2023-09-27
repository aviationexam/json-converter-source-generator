using System;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;

namespace Aviationexam.JsonConverter.SourceGenerator.Target.Tests;

public class BaseContractSerializationTests
{
    [Theory]
    [MemberData(nameof(BaseJsonContractData))]
    public void DeserializeBaseContractWorks(string json, Type targetType)
    {
        var baseContract = JsonSerializer.Deserialize<BaseContract>(
            json,
            MyJsonSerializerContext.Default.Options
        );

        Assert.NotNull(baseContract);
        Assert.Equal(1, baseContract.BaseProperty);
        Assert.IsType(targetType, baseContract);

        if (baseContract is LeafContract leafContract)
        {
            Assert.Equal(2, leafContract.LeafProperty);
        }

        if (baseContract is AnotherLeafContract anotherLeafContract)
        {
            Assert.Equal(2, anotherLeafContract.AnotherLeafProperty);
        }
    }

    [Theory]
    [MemberData(nameof(LeafContractData))]
    public void SerializeBaseContractWorks(BaseContract contract, string expectedJson)
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
                "$type": "LeafContract",
                "baseProperty": 1,
                "leafProperty": 2
            }
            """,
            typeof(LeafContract),
        };
        yield return new object[]
        {
            // language=json
            """
            {
                "baseProperty": 1,
                "$type": "LeafContract",
                "leafProperty": 2
            }
            """,
            typeof(LeafContract),
        };
        yield return new object[]
        {
            // language=json
            """
            {
                "baseProperty": 1,
                "leafProperty": 2,
                "$type": "LeafContract"
            }
            """,
            typeof(LeafContract),
        };
        yield return new object[]
        {
            // language=json
            """
            {
                "$type": 2,
                "baseProperty": 1,
                "anotherLeafProperty": 2
            }
            """,
            typeof(AnotherLeafContract),
        };
        yield return new object[]
        {
            // language=json
            """
            {
                "baseProperty": 1,
                "$type": 2,
                "anotherLeafProperty": 2
            }
            """,
            typeof(AnotherLeafContract),
        };
        yield return new object[]
        {
            // language=json
            """
            {
                "baseProperty": 1,
                "anotherLeafProperty": 2,
                "$type": 2
            }
            """,
            typeof(AnotherLeafContract),
        };
    }

    public static IEnumerable<object[]> LeafContractData()
    {
        yield return new object[]
        {
            new LeafContract
            {
                BaseProperty = 1,
                LeafProperty = 2
            },
            // language=json
            """
            {
              "$type": "LeafContract",
              "leafProperty": 2,
              "baseProperty": 1
            }
            """.Replace("\r\n", Environment.NewLine)
        };
        yield return new object[]
        {
            new AnotherLeafContract
            {
                BaseProperty = 1,
                AnotherLeafProperty = 2
            },
            // language=json
            """
            {
              "$type": 2,
              "anotherLeafProperty": 2,
              "baseProperty": 1
            }
            """.Replace("\r\n", Environment.NewLine)
        };
    }
}
