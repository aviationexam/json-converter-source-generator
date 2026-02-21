//HintName: ProjectJsonSerializerContext.g.cs
#nullable enable

public partial class ProjectJsonSerializerContext
{
    public static System.Collections.Generic.IReadOnlyCollection<System.Text.Json.Serialization.JsonConverter> GetPolymorphicConverters() => new System.Text.Json.Serialization.JsonConverter[]
    {
        new PolymorphicGlobalNamespace.BaseTypeJsonPolymorphicConverter(),
        new PolymorphicGlobalNamespace.FirstLevel1LeafJsonPolymorphicConverter(),
        new PolymorphicGlobalNamespace.FirstLevel2LeafJsonPolymorphicConverter(),
        new PolymorphicGlobalNamespace.SecondLevel1ALeafJsonPolymorphicConverter(),
        new PolymorphicGlobalNamespace.SecondLevel1BLeafJsonPolymorphicConverter(),
        new PolymorphicGlobalNamespace.SecondLevel2ALeafJsonPolymorphicConverter(),
        new PolymorphicGlobalNamespace.SecondLevel2BLeafJsonPolymorphicConverter(),
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