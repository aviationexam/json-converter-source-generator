//HintName: LeafAOfInt32LeafJsonPolymorphicConverter.g.cs
#nullable enable

namespace PolymorphicGlobalNamespace;

internal class LeafAOfInt32LeafJsonPolymorphicConverter : Aviationexam.GeneratedJsonConverters.LeafPolymorphicJsonConvertor<global::LeafA<global::System.Int32>>
{
    protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "$type"u8;

    protected override void WriteDiscriminatorValue(System.Text.Json.Utf8JsonWriter writer)
    {
        writer.WriteString(GetDiscriminatorPropertyName(), "int_LeafA");
    }
}