using Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Contracts;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Tests;

public class BaseContractSerializationTests
{
    private JsonSerializerOptions CreateJsonSerializerOptions()
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
        var baseContract = JsonSerializer.Deserialize<BaseContract>(
            json,
            CreateJsonSerializerOptions()
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

        if (baseContract is GenericLeafContract genericLeafContract)
        {
            Assert.Equal(2, genericLeafContract.Property);
        }
    }

    [Theory]
    [MemberData(nameof(LeafContractData))]
    public void SerializeBaseContractWorks(BaseContract contract, string expectedJson)
    {
        var json = JsonSerializer.Serialize(
            contract,
            CreateJsonSerializerOptions()
        );

        Assert.Equal(expectedJson, json);
    }


    [Fact]
    public Task SerializeLeafContractWorks()
    {
        var json = JsonSerializer.Serialize(
            new LeafContract
            {
                BaseProperty = 1,
                LeafProperty = 2
            },
            CreateJsonSerializerOptions()
        );

        return Verifier.VerifyJson(json);
    }

    [Fact]
    public Task SerializeAnotherLeafContractWorks()
    {
        var json = JsonSerializer.Serialize(
            new AnotherLeafContract
            {
                BaseProperty = 1,
                AnotherLeafProperty = 2
            },
            CreateJsonSerializerOptions()
        );

        return Verifier.VerifyJson(json);
    }

    public static IEnumerable<object[]> BaseJsonContractData()
    {
        yield return
        [
            // language=json
            """
            {
                "$type": "LeafContract",
                "baseProperty": 1,
                "leafProperty": 2
            }
            """,
            typeof(LeafContract),
        ];
        yield return
        [
            // language=json
            """
            {
                "baseProperty": 1,
                "$type": "LeafContract",
                "leafProperty": 2
            }
            """,
            typeof(LeafContract),
        ];
        yield return
        [
            // language=json
            """
            {
                "baseProperty": 1,
                "leafProperty": 2,
                "$type": "LeafContract"
            }
            """,
            typeof(LeafContract),
        ];
        yield return
        [
            // language=json
            """
            {
                "$type": 2,
                "baseProperty": 1,
                "anotherLeafProperty": 2
            }
            """,
            typeof(AnotherLeafContract),
        ];
        yield return
        [
            // language=json
            """
            {
                "baseProperty": 1,
                "$type": 2,
                "anotherLeafProperty": 2
            }
            """,
            typeof(AnotherLeafContract),
        ];
        yield return
        [
            // language=json
            """
            {
                "baseProperty": 1,
                "anotherLeafProperty": 2,
                "$type": 2
            }
            """,
            typeof(AnotherLeafContract),
        ];
        yield return
        [
            // language=json
            """
            {
                "baseProperty": 1,
                "property": 2,
                "$type": "GenericLeafContract"
            }
            """,
            typeof(GenericLeafContract),
        ];
    }

    public static TheoryData<BaseContract, string> LeafContractData() => new()
    {
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
        },
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
        },
        {
            new GenericLeafContract
            {
                BaseProperty = 1,
                Property = 2
            },
            // language=json
            """
                {
                  "$type": "GenericLeafContract",
                  "property": 2,
                  "baseProperty": 1
                }
                """.Replace("\r\n", Environment.NewLine)
        }
    };
}
