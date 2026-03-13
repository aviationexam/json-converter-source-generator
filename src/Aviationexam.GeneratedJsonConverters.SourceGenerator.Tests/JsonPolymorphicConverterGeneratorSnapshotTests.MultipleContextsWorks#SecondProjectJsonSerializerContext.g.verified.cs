//HintName: SecondProjectJsonSerializerContext.g.cs
#nullable enable

public partial class SecondProjectJsonSerializerContext
{
    public static System.Collections.Generic.IReadOnlyCollection<System.Text.Json.Serialization.JsonConverter> GetPolymorphicConverters() => new System.Text.Json.Serialization.JsonConverter[]
    {
        new PolymorphicGlobalNamespace.PersonJsonPolymorphicConverter(),
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
        PolymorphicGlobalNamespace.PersonJsonPolymorphicConverter.ConfigureJsonTypeInfo,
    };
}