using System;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;

namespace Aviationexam.JsonConverter.SourceGenerator.Target.Tests;

public class BaseContractSerializationTests
{
    [Theory]
    [MemberData(nameof(BaseContractData))]
    public void DeserializeBaseContractWorks(string json, Type targetType)
    {
        var baseContract = JsonSerializer.Deserialize(json, MyJsonSerializerContext.Default.BaseContract);

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

    public static IEnumerable<object[]> BaseContractData()
    {
        yield return new object[]
        {
            """
            {
                "$type": "LeafContract",
                "BaseProperty": 1,
                "LeafProperty": 2
            }
            """,
            typeof(LeafContract),
        };
        yield return new object[]
        {
            """
            {
                "BaseProperty": 1,
                "$type": "LeafContract",
                "LeafProperty": 2
            }
            """,
            typeof(LeafContract),
        };
        yield return new object[]
        {
            """
            {
                "BaseProperty": 1,
                "LeafProperty": 2
                "$type": "LeafContract",
            }
            """,
            typeof(LeafContract),
        };
        yield return new object[]
        {
            """
            {
                "$type": "AnotherLeafContract",
                "BaseProperty": 1,
                "AnotherLeafProperty": 2
            }
            """,
            typeof(AnotherLeafContract),
        };
        yield return new object[]
        {
            """
            {
                "BaseProperty": 1,
                "$type": "AnotherLeafContract",
                "AnotherLeafProperty": 2
            }
            """,
            typeof(AnotherLeafContract),
        };
        yield return new object[]
        {
            """
            {
                "BaseProperty": 1,
                "AnotherLeafProperty": 2
                "$type": "AnotherLeafContract",
            }
            """,
            typeof(AnotherLeafContract),
        };
    }
}
