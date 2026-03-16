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

    [Theory]
    [MemberData(nameof(BaseJsonContractAData))]
    public void DeserializeGenericBaseContractOfAWorks(string json, Type targetType)
    {
        var baseContract = JsonSerializer.Deserialize<BaseContract<A>>(
            json,
            CreateJsonSerializerOptions()
        );

        Assert.NotNull(baseContract);
        Assert.Equal(2, baseContract.Value.Value);
        Assert.IsType(targetType, baseContract);
    }

    [Theory]
    [MemberData(nameof(LeafAContractData))]
    public void SerializeBaseContractAWorks(BaseContract<A> contract, string expectedJson)
    {
        var json = JsonSerializer.Serialize(
            contract,
            CreateJsonSerializerOptions()
        );

        Assert.Equal(expectedJson, json);
    }

    [Theory]
    [MemberData(nameof(BaseJsonContractBData))]
    public void DeserializeGenericBaseContractOfBWorks(string json, Type targetType)
    {
        var baseContract = JsonSerializer.Deserialize<BaseContract<B>>(
            json,
            CreateJsonSerializerOptions()
        );

        Assert.NotNull(baseContract);
        Assert.Equal("hi", baseContract.Value.Value);
        Assert.IsType(targetType, baseContract);
    }

    [Theory]
    [MemberData(nameof(LeafBContractData))]
    public void SerializeBaseContractBWorks(BaseContract<B> contract, string expectedJson)
    {
        var json = JsonSerializer.Serialize(
            contract,
            CreateJsonSerializerOptions()
        );

        Assert.Equal(expectedJson, json);
    }

    [Theory]
    [MemberData(nameof(LeafContractData))]
    public void SerializeGenericBaseContractWorks(BaseContract contract, string expectedJson)
    {
        var json = JsonSerializer.Serialize(
            contract,
            CreateJsonSerializerOptions()
        );

        Assert.Equal(expectedJson, json);
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

    public static TheoryData<string, Type> BaseJsonContractAData() => new()
    {
        {
            // language=json
            """
            {
                "myCustomDiscriminator": "Leaf1",
                "value": {
                  "value": 2
                }
            }
            """,
            typeof(Leaf1Contract<A>)
        },
        {
            // language=json
            """
            {
                "myCustomDiscriminator": "Leaf2",
                "value": {
                  "value": 2
                }
            }
            """,
            typeof(Leaf2Contract<A>)
        },
    };

    public static TheoryData<string, Type> BaseJsonContractBData() => new()
    {
        {
            // language=json
            """
            {
                "myCustomDiscriminator": "Leaf1",
                "value": {
                  "value": "hi"
                }
            }
            """,
            typeof(Leaf1Contract<B>)
        },
        {
            // language=json
            """
            {
                "myCustomDiscriminator": "Leaf2",
                "value": {
                  "value": "hi"
                }
            }
            """,
            typeof(Leaf2Contract<B>)
        },
    };

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

    public static TheoryData<BaseContract<A>, string> LeafAContractData() => new()
    {
        {
            new Leaf1Contract<A>
            {
                Value = new A
                {
                    Value = 2,
                }
            },
            // language=json
            """
                {
                  "myCustomDiscriminator": "Leaf1",
                  "value": {
                    "value": 2
                  }
                }
                """.Replace("\r\n", Environment.NewLine)
        },
        {
            new Leaf2Contract<A>
            {
                Value = new A
                {
                    Value = 4,
                }
            },
            // language=json
            """
                {
                  "myCustomDiscriminator": "Leaf2",
                  "value": {
                    "value": 4
                  }
                }
                """.Replace("\r\n", Environment.NewLine)
        }
    };

    public static TheoryData<BaseContract<B>, string> LeafBContractData() => new()
    {
        {
            new Leaf1Contract<B>
            {
                Value = new B
                {
                    Value = "hi",
                }
            },
            // language=json
            """
                {
                  "myCustomDiscriminator": "Leaf1",
                  "value": {
                    "value": "hi"
                  }
                }
                """.Replace("\r\n", Environment.NewLine)
        },
        {
            new Leaf2Contract<B>
            {
                Value = new B
                {
                    Value = "there",
                }
            },
            // language=json
            """
                {
                  "myCustomDiscriminator": "Leaf2",
                  "value": {
                    "value": "there"
                  }
                }
                """.Replace("\r\n", Environment.NewLine)
        }
    };
}
