//HintName: ProjectJsonSerializerContext.g.cs
#nullable enable

public partial class ProjectJsonSerializerContext
{
    public static System.Collections.Generic.IReadOnlyCollection<System.Text.Json.Serialization.JsonConverter> GetPolymorphicConverters() => new System.Text.Json.Serialization.JsonConverter[]
    {
        new PolymorphicGlobalNamespace.BaseTypeOfTJsonPolymorphicConverter(),
        new PolymorphicGlobalNamespace.LeafAOfInt32LeafJsonPolymorphicConverter(),
        new PolymorphicGlobalNamespace.LeafBOfInt32LeafJsonPolymorphicConverter(),
        new PolymorphicGlobalNamespace.LeafAOfStringLeafJsonPolymorphicConverter(),
        new PolymorphicGlobalNamespace.LeafBOfStringLeafJsonPolymorphicConverter(),
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
}