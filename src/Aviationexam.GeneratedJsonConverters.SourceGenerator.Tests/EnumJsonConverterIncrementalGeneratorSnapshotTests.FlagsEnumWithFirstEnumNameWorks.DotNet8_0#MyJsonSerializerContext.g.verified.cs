//HintName: MyJsonSerializerContext.g.cs
#nullable enable

namespace ApplicationNamespace;

public partial class MyJsonSerializerContext
{
    public static System.Collections.Generic.IReadOnlyCollection<System.Text.Json.Serialization.JsonConverter> GetEnumConverters() => new System.Text.Json.Serialization.JsonConverter[]
    {
        new ApplicationNamespace.Contracts.EFlagsEnumEnumJsonConverter(),
    };

    public static void UseEnumConverters(
        System.Collections.Generic.ICollection<System.Text.Json.Serialization.JsonConverter> optionsConverters
    )
    {
        foreach (var converter in GetEnumConverters())
        {
            optionsConverters.Add(converter);
        }
    }

    public static System.Collections.Generic.IReadOnlyCollection<System.Action<System.Text.Json.Serialization.Metadata.JsonTypeInfo>> GetEnumJsonTypeInfoConfigurations() => new System.Action<System.Text.Json.Serialization.Metadata.JsonTypeInfo>[]
    {
        
    };
}