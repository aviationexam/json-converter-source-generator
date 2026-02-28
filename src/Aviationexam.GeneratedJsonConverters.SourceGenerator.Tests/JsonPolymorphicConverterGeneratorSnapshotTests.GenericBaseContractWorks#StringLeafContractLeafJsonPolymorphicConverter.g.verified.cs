//HintName: StringLeafContractLeafJsonPolymorphicConverter.g.cs
#nullable enable

namespace ApplicationNamespace;

internal class StringLeafContractLeafJsonPolymorphicConverter : Aviationexam.GeneratedJsonConverters.LeafPolymorphicJsonConvertor<global::ApplicationNamespace.Contracts.StringLeafContract>
{
    protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "myCustomDiscriminator"u8;

    protected override void WriteDiscriminatorValue(System.Text.Json.Utf8JsonWriter writer)
    {
        writer.WriteString(GetDiscriminatorPropertyName(), "StringLeafContract");
    }
}