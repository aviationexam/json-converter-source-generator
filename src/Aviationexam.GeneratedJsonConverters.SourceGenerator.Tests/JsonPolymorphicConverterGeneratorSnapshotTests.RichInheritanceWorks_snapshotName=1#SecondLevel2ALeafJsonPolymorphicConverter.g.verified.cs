//HintName: SecondLevel2ALeafJsonPolymorphicConverter.g.cs
#nullable enable

namespace PolymorphicGlobalNamespace;

internal class SecondLevel2ALeafJsonPolymorphicConverter : Aviationexam.GeneratedJsonConverters.LeafPolymorphicJsonConvertor<global::SecondLevel2A>
{
    protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "$type"u8;

    protected override void WriteDiscriminatorValue(System.Text.Json.Utf8JsonWriter writer)
    {
        writer.WriteString(GetDiscriminatorPropertyName(), "SecondLevel2A");
    }
}