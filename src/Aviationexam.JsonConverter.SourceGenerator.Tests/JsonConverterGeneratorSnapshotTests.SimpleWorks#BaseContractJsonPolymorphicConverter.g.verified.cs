//HintName: BaseContractJsonPolymorphicConverter.g.cs
using System;

namespace ApplicationNamespace;

internal class BaseContractJsonPolymorphicConverter : PolymorphicJsonConvertor<ApplicationNamespace.BaseContract>
{
    protected override ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "$type"u8;

    protected override Type GetTypeForDiscriminator(string discriminator) => discriminator switch {
        _ => throw new ArgumentOutOfRangeException(nameof(discriminator), discriminator, null);
    };
}
