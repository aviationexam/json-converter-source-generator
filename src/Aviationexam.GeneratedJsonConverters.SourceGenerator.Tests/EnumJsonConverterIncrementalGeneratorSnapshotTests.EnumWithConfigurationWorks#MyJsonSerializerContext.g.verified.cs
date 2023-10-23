//HintName: MyJsonSerializerContext.g.cs
#nullable enable

namespace ApplicationNamespace;

public partial class MyJsonSerializerContext
{
    public static System.Collections.Generic.IReadOnlyCollection<System.Text.Json.Serialization.JsonConverter> GetEnumConverters() => new System.Text.Json.Serialization.JsonConverter[]
    {
        new ApplicationNamespace.Contracts.EBackingEnumEnumJsonConverter(),
        new ApplicationNamespace.Contracts.EPropertyEnumEnumJsonConverter(),
        new ApplicationNamespace.Contracts.EPropertyWithBackingEnumEnumJsonConverter(),
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
}
