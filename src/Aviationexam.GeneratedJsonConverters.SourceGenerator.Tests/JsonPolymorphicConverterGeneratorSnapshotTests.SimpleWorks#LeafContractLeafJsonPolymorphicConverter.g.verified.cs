//HintName: LeafContractLeafJsonPolymorphicConverter.g.cs
#nullable enable

namespace ApplicationNamespace;

internal class LeafContractLeafJsonPolymorphicConverter : Aviationexam.GeneratedJsonConverters.LeafPolymorphicJsonConvertor<global::ApplicationNamespace.Contracts.LeafContract>
{
    protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "$type"u8;

    protected override void WriteDiscriminatorValue(System.Text.Json.Utf8JsonWriter writer)
    {
        writer.WriteString(GetDiscriminatorPropertyName(), "LeafContract");
    }
}