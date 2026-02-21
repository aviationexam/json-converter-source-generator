//HintName: AnotherLeafContractLeafJsonPolymorphicConverter.g.cs
#nullable enable

namespace ApplicationNamespace;

internal class AnotherLeafContractLeafJsonPolymorphicConverter : Aviationexam.GeneratedJsonConverters.LeafPolymorphicJsonConvertor<global::ApplicationNamespace.Contracts.AnotherLeafContract>
{
    protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "$type"u8;

    protected override void WriteDiscriminatorValue(System.Text.Json.Utf8JsonWriter writer)
    {
        writer.WriteNumber(GetDiscriminatorPropertyName(), 2);
    }
}