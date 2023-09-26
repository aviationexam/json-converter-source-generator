//HintName: BaseContractJsonPolymorphicConverter.g.cs
#nullable enable
using System;

namespace ApplicationNamespace;

internal class BaseContractJsonPolymorphicConverter : PolymorphicJsonConvertor<ApplicationNamespace.BaseContract>
{
    protected override ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "$type"u8;

    protected override Type GetTypeForDiscriminator(
        string discriminator
    ) => discriminator switch
    {
        "LeafContract" => typeof(ApplicationNamespace.LeafContract),
        "AnotherLeafContract" => typeof(ApplicationNamespace.AnotherLeafContract),

        _ => throw new ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
    };
}
