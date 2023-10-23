using Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Contracts;
using System;
using System.Text.Json;
using Xunit;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Tests;

public class EnumSerializationTests
{
    [Theory]
    [InlineData(typeof(EBackingEnum), EBackingEnum.A, "0")]
    [InlineData(typeof(EBackingEnum), EBackingEnum.B, "1")]
    [InlineData(typeof(EConfiguredPropertyEnum), EConfiguredPropertyEnum.A, "\"C\"")]
    [InlineData(typeof(EConfiguredPropertyEnum), EConfiguredPropertyEnum.B, "\"D\"")]
    [InlineData(typeof(EMyEnum), EMyEnum.A, "\"A\"")]
    [InlineData(typeof(EMyEnum), EMyEnum.B, "\"B\"")]
    [InlineData(typeof(EPropertyEnum), EPropertyEnum.C, "\"C\"")]
    [InlineData(typeof(EPropertyEnum), EPropertyEnum.D, "\"D\"")]
    [InlineData(typeof(EPropertyWithBackingEnum), EPropertyWithBackingEnum.E, "\"E\"")]
    [InlineData(typeof(EPropertyWithBackingEnum), EPropertyWithBackingEnum.F, "\"F\"")]
    [InlineData(typeof(EDuplicatedValueUsingBackingTypeEnum), EDuplicatedValueUsingBackingTypeEnum.A, "0")]
    [InlineData(typeof(EDuplicatedValueUsingBackingTypeEnum), EDuplicatedValueUsingBackingTypeEnum.B, "1")]
#pragma warning disable xUnit1025
    [InlineData(typeof(EDuplicatedValueUsingBackingTypeEnum), EDuplicatedValueUsingBackingTypeEnum.C, "1")]
#pragma warning restore xUnit1025
    [InlineData(typeof(EDuplicatedValueUsingFirstEnumNameEnum), EDuplicatedValueUsingFirstEnumNameEnum.A, "\"C\"")]
    [InlineData(typeof(EDuplicatedValueUsingFirstEnumNameEnum), EDuplicatedValueUsingFirstEnumNameEnum.B, "\"D\"")]
#pragma warning disable xUnit1025
    [InlineData(typeof(EDuplicatedValueUsingFirstEnumNameEnum), EDuplicatedValueUsingFirstEnumNameEnum.C, "\"D\"")]
#pragma warning restore xUnit1025
    public void SerializeEnumWorks(
        Type type,
        object enumValue,
        string expectedValue
    )
    {
        var serializedValue = JsonSerializer.Serialize(
            enumValue,
            type,
            MyJsonSerializerContext.Default.Options
        );

        Assert.Equal(expectedValue, serializedValue);
    }

    [Theory]
    [InlineData(typeof(EBackingEnum), EBackingEnum.A, "0")]
    [InlineData(typeof(EBackingEnum), EBackingEnum.B, "1")]
    [InlineData(typeof(EConfiguredPropertyEnum), EConfiguredPropertyEnum.A, "\"C\"")]
    [InlineData(typeof(EConfiguredPropertyEnum), EConfiguredPropertyEnum.B, "\"D\"")]
    [InlineData(typeof(EMyEnum), EMyEnum.A, "\"A\"")]
    [InlineData(typeof(EMyEnum), EMyEnum.B, "\"B\"")]
    [InlineData(typeof(EPropertyEnum), EPropertyEnum.C, "\"C\"")]
    [InlineData(typeof(EPropertyEnum), EPropertyEnum.D, "\"D\"")]
    [InlineData(typeof(EPropertyWithBackingEnum), EPropertyWithBackingEnum.E, "\"E\"")]
    [InlineData(typeof(EPropertyWithBackingEnum), EPropertyWithBackingEnum.F, "\"F\"")]
    [InlineData(typeof(EPropertyWithBackingEnum), EPropertyWithBackingEnum.E, "0")]
    [InlineData(typeof(EPropertyWithBackingEnum), EPropertyWithBackingEnum.F, "1")]
    [InlineData(typeof(EDuplicatedValueUsingBackingTypeEnum), EDuplicatedValueUsingBackingTypeEnum.A, "0")]
    [InlineData(typeof(EDuplicatedValueUsingBackingTypeEnum), EDuplicatedValueUsingBackingTypeEnum.B, "1")]
#pragma warning disable xUnit1025
    [InlineData(typeof(EDuplicatedValueUsingBackingTypeEnum), EDuplicatedValueUsingBackingTypeEnum.C, "1")]
#pragma warning restore xUnit1025
    [InlineData(typeof(EDuplicatedValueUsingFirstEnumNameEnum), EDuplicatedValueUsingFirstEnumNameEnum.A, "\"C\"")]
    [InlineData(typeof(EDuplicatedValueUsingFirstEnumNameEnum), EDuplicatedValueUsingFirstEnumNameEnum.B, "\"D\"")]
#pragma warning disable xUnit1025
    [InlineData(typeof(EDuplicatedValueUsingFirstEnumNameEnum), EDuplicatedValueUsingFirstEnumNameEnum.C, "\"E\"")]
#pragma warning restore xUnit1025
    public void DeserializeEnumWorks(
        Type type,
        object expectedValue,
        string sourceJson
    )
    {
        var serializedValue = JsonSerializer.Deserialize(
            sourceJson,
            type,
            MyJsonSerializerContext.Default.Options
        );

        Assert.Equal(expectedValue, serializedValue);
    }
}
