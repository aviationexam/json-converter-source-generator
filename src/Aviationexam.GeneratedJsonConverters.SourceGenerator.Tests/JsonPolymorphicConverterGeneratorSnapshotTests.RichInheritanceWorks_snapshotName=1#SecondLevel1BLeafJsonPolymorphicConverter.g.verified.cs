//HintName: SecondLevel1BLeafJsonPolymorphicConverter.g.cs
#nullable enable

namespace PolymorphicGlobalNamespace;

internal class SecondLevel1BLeafJsonPolymorphicConverter : Aviationexam.GeneratedJsonConverters.LeafPolymorphicJsonConvertor<global::SecondLevel1B>
{
    protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "$type"u8;

    protected override void WriteDiscriminatorValue(System.Text.Json.Utf8JsonWriter writer)
    {
        writer.WriteString(GetDiscriminatorPropertyName(), "SecondLevel1B");
    }
}