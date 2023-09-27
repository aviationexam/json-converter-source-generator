[![Build Status](https://github.com/aviationexam/json-converter-source-generator/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/aviationexam/json-converter-source-generator/actions/workflows/build.yml)
[![NuGet](https://img.shields.io/nuget/v/Aviationexam.JsonConverter.SourceGenerator.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/Aviationexam.JsonConverter.SourceGenerator/)
[![MyGet](https://img.shields.io/myget/json-converter-source-generator/vpre/Aviationexam.JsonConverter.SourceGenerator?label=MyGet)](https://www.myget.org/feed/json-converter-source-generator/package/nuget/Aviationexam.JsonConverter.SourceGenerator)
[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https%3A%2F%2Ff.feedz.io%2Faviationexam%2Fjson-converter-source-generator%2Fshield%2FAviationexam.JsonConverter.SourceGenerator%2Flatest&label=Aviationexam.JsonConverter.SourceGenerator)](https://f.feedz.io/aviationexam/json-converter-source-generator/packages/Aviationexam.JsonConverter.SourceGenerator/latest/download)

# Aviationexam.JsonConverter.SourceGenerator

## Install
```xml
<ItemGroup>
    <PackageReference Include="Aviationexam.JsonConverter.SourceGenerator" Version="0.1.0" PrivateAssets="all" />
</ItemGroup>
```

## How to use library

```cs
// file=contracts.cs
using Aviationexam.JsonConverter.Attributes;

[JsonPolymorphic]
[JsonDerivedType(typeof(LeafContract), typeDiscriminator: nameof(LeafContract))]
[JsonDerivedType(typeof(AnotherLeafContract), typeDiscriminator: 2)]
public abstract class BaseContract
{
    public int BaseProperty { get; set; }
}
public sealed class LeafContract : BaseContract
{
    public int LeafProperty { get; set; }
}
public sealed class AnotherLeafContract : BaseContract
{
    public int AnotherLeafProperty { get; set; }
}

// file=MyJsonSerializerContext.cs
using System.Text.Json.Serialization;

[JsonSerializable(typeof(BaseContract))] // this line is neccesary, generator searches for JsonSerializableAttribute with argument type decorated by JsonPolymorphicAttribute
[JsonSerializable(typeof(LeafContract))] // notice, it's necessary to specify leaf types
[JsonSerializable(typeof(AnotherLeafContract))]
public partial class MyJsonSerializerContext : JsonSerializerContext
{
    static MyJsonSerializerContext()
    {
        // register generated converters to options
        UsePolymorphicConverters(s_defaultOptions.Converters);
    }
}
```
