﻿//HintName: BaseContractJsonPolymorphicConverter.g.cs
#nullable enable

namespace ApplicationNamespace;

internal class BaseContractJsonPolymorphicConverter : Aviationexam.GeneratedJsonConverters.PolymorphicJsonConvertor<global::ApplicationNamespace.Contracts.BaseContract>
{
    protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "myCustomDiscriminator"u8;

    protected override System.Type GetTypeForDiscriminator(
        Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct discriminator
    ) => discriminator switch
    {
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "LeafContract" } => typeof(ApplicationNamespace.Contracts.LeafContract),

        _ => throw new System.ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
    };

    protected override Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct GetDiscriminatorForInstance<TInstance>(
        TInstance instance, out System.Type targetType
    )
    {
        if (instance is ApplicationNamespace.Contracts.LeafContract)
        {
            targetType = typeof(ApplicationNamespace.Contracts.LeafContract);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("LeafContract");
        }

        throw new System.ArgumentOutOfRangeException(nameof(instance), instance, null);
    }
}