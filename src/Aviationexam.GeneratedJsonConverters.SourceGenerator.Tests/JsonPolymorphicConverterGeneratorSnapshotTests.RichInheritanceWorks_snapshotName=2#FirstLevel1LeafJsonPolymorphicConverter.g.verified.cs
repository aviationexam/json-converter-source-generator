//HintName: FirstLevel1LeafJsonPolymorphicConverter.g.cs
#nullable enable

namespace PolymorphicGlobalNamespace;

internal class FirstLevel1LeafJsonPolymorphicConverter : Aviationexam.GeneratedJsonConverters.LeafPolymorphicJsonConvertor<global::FirstLevel1>
{
    protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "$type"u8;

    protected override void WriteDiscriminatorValue(System.Text.Json.Utf8JsonWriter writer)
    {
        writer.WriteString(GetDiscriminatorPropertyName(), "FirstLevel1");
    }
}