using Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Contracts;
using System.Text.Json;
using Xunit;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Tests;

public class EnumSerializationTests
{
    [Theory]
    [InlineData(EBackingEnum.A, "0")]
    [InlineData(EBackingEnum.B, "1")]
    [InlineData(EConfiguredPropertyEnum.A, "\"C\"")]
    [InlineData(EConfiguredPropertyEnum.B, "\"D\"")]
    [InlineData(EMyEnum.A, "\"A\"")]
    [InlineData(EMyEnum.B, "\"B\"")]
    [InlineData(EPropertyEnum.C, "\"C\"")]
    [InlineData(EPropertyEnum.D, "\"D\"")]
    [InlineData(EPropertyWithBackingEnum.E, "\"E\"")]
    [InlineData(EPropertyWithBackingEnum.F, "\"F\"")]
    public void SerializeEnumWorks(object enumValue, string expectedValue)
    {
        var serializedValue = JsonSerializer.Serialize(
            enumValue,
            enumValue.GetType(),
            MyJsonSerializerContext.Default.Options
        );

        Assert.Equal(expectedValue, serializedValue);
    }
}
