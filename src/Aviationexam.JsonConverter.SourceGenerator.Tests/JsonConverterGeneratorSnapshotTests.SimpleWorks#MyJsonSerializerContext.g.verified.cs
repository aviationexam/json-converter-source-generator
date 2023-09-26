//HintName: MyJsonSerializerContext.g.cs
#nullable enable
using System.Collections.Generic;

namespace ApplicationNamespace;

public partial class MyJsonSerializerContext
{
    public static IReadOnlyCollection<System.Text.Json.Serialization.JsonConverter> GetPolymorphicConverters() => new System.Text.Json.Serialization.JsonConverter[]
    {
        new BaseContractJsonPolymorphicConverter(),
    };

    public static void UsePolymorphicConverters(
        ICollection<System.Text.Json.Serialization.JsonConverter> optionsConverters
    )
    {
        foreach (var converter in GetPolymorphicConverters())
        {
            optionsConverters.Add(converter);
        }
    }
}
