//HintName: MyJsonSerializerContext.g.cs
#nullable enable

namespace ApplicationNamespace;

public partial class MyJsonSerializerContext
{
    public static System.Collections.Generic.IReadOnlyCollection<System.Text.Json.Serialization.JsonConverter> GetPolymorphicConverters() => new System.Text.Json.Serialization.JsonConverter[]
    {
        new ApplicationNamespace.BaseContractJsonPolymorphicConverter(),
        new ApplicationNamespace.BaseContractOfAJsonPolymorphicConverter(),
        new ApplicationNamespace.BaseContractOfBJsonPolymorphicConverter(),
    };

    public static void UsePolymorphicConverters(
        System.Collections.Generic.ICollection<System.Text.Json.Serialization.JsonConverter> optionsConverters
    )
    {
        foreach (var converter in GetPolymorphicConverters())
        {
            optionsConverters.Add(converter);
        }
    }

    public static System.Collections.Generic.IReadOnlyCollection<System.Action<System.Text.Json.Serialization.Metadata.JsonTypeInfo>> GetPolymorphicJsonTypeInfoConfigurations() => new System.Action<System.Text.Json.Serialization.Metadata.JsonTypeInfo>[]
    {
        ApplicationNamespace.BaseContractJsonPolymorphicConverter.ConfigureJsonTypeInfo,
        ApplicationNamespace.BaseContractOfAJsonPolymorphicConverter.ConfigureJsonTypeInfo,
        ApplicationNamespace.BaseContractOfBJsonPolymorphicConverter.ConfigureJsonTypeInfo,
    };
}