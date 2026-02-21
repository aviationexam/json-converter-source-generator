//HintName: IntLeafContractLeafJsonPolymorphicConverter.g.cs
#nullable enable

namespace ApplicationNamespace;

internal class IntLeafContractLeafJsonPolymorphicConverter : Aviationexam.GeneratedJsonConverters.LeafPolymorphicJsonConvertor<global::ApplicationNamespace.Contracts.IntLeafContract>
{
    protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "myCustomDiscriminator"u8;

    protected override void WriteDiscriminatorValue(System.Text.Json.Utf8JsonWriter writer)
    {
        writer.WriteString(GetDiscriminatorPropertyName(), "IntLeafContract");
    }
}