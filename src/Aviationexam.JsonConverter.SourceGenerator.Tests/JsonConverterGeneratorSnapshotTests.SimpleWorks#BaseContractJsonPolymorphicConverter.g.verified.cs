//HintName: BaseContractJsonPolymorphicConverter.g.cs
#nullable enable
using System;

namespace ApplicationNamespace;

internal class BaseContractJsonPolymorphicConverter : PolymorphicJsonConvertor<ApplicationNamespace.BaseContract>
{
    protected override ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "$type"u8;

    protected override Type GetTypeForDiscriminator(
        IDiscriminatorStruct discriminator
    ) => discriminator switch
    {
        DiscriminatorStruct<string> { Value: "LeafContract" } => typeof(ApplicationNamespace.LeafContract),
        DiscriminatorStruct<string> { Value: "AnotherLeafContract" } => typeof(ApplicationNamespace.AnotherLeafContract),

        _ => throw new ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
    };

    protected override IDiscriminatorStruct GetDiscriminatorForType(Type type)
    {
        if (type == typeof(ApplicationNamespace.LeafContract))
        {
            return new DiscriminatorStruct<string>("LeafContract");
        }
        if (type == typeof(ApplicationNamespace.AnotherLeafContract))
        {
            return new DiscriminatorStruct<string>("AnotherLeafContract");
        }

        throw new ArgumentOutOfRangeException(nameof(type), type, null);
    }
}
