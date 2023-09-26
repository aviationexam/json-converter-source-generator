//HintName: MyJsonSerializerContext.g.cs
using System.Collections.Generic;

namespace ApplicationNamespace;

public partial class MyJsonSerializerContext
{
    protected static IReadOnlyCollection<System.Text.Json.Serialization.JsonConverter> GetPolymorphicConverters() => new System.Text.Json.Serialization.JsonConverter[]
    {
        new BaseContractJsonPolymorphicConverter(),
    };

    protected static void UsePolymorphicConverters(
        ICollection<System.Text.Json.Serialization.JsonConverter> optionsConverters
    )
    {
        foreach (var converter in GetPolymorphicConverters())
        {
            optionsConverters.Add(converter);
        }
    }
}
