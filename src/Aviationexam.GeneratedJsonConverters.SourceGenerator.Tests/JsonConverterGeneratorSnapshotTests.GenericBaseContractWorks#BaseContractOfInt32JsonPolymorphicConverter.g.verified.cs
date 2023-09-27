﻿//HintName: BaseContractOfInt32JsonPolymorphicConverter.g.cs
#nullable enable

namespace ApplicationNamespace;

internal class BaseContractOfInt32JsonPolymorphicConverter : Aviationexam.GeneratedJsonConverters.PolymorphicJsonConvertor<ApplicationNamespace.Contracts.BaseContract<System.Int32>>
{
    protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "myCustomDiscriminator"u8;

    protected override System.Type GetTypeForDiscriminator(
        Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct discriminator
    ) => discriminator switch
    {
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "LeafContract" } => typeof(LeafContract),

        _ => throw new System.ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
    };

    protected override Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct GetDiscriminatorForType(
        System.Type type
    )
    {
        if (type == typeof(LeafContract))
        {
            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("LeafContract");
        }

        throw new System.ArgumentOutOfRangeException(nameof(type), type, null);
    }
}
