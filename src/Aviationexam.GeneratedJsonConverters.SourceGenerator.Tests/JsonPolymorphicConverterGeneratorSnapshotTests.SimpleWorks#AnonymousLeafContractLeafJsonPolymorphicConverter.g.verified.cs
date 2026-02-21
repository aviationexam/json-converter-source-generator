//HintName: AnonymousLeafContractLeafJsonPolymorphicConverter.g.cs
#nullable enable

namespace ApplicationNamespace;

internal class AnonymousLeafContractLeafJsonPolymorphicConverter : Aviationexam.GeneratedJsonConverters.LeafPolymorphicJsonConvertor<global::ApplicationNamespace.Contracts.AnonymousLeafContract>
{
    protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "$type"u8;

    protected override void WriteDiscriminatorValue(System.Text.Json.Utf8JsonWriter writer)
    {
        writer.WriteString(GetDiscriminatorPropertyName(), "AnonymousLeafContract");
    }
}