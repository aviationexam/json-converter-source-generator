﻿//HintName: BaseContractJsonPolymorphicConverter.g.cs
#nullable enable

namespace ApplicationNamespace;

internal class BaseContractJsonPolymorphicConverter : Aviationexam.GeneratedJsonConverters.PolymorphicJsonConvertor<global::ApplicationNamespace.Contracts.BaseContract>
{
    protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "$type"u8;

    protected override System.Type GetTypeForDiscriminator(
        Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct discriminator
    ) => discriminator switch
    {
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "LeafContract" } => typeof(ApplicationNamespace.Contracts.LeafContract),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<int> { Value: 2 } => typeof(ApplicationNamespace.Contracts.AnotherLeafContract),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "AnonymousLeafContract" } => typeof(ApplicationNamespace.Contracts.AnonymousLeafContract),

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
        if (instance is ApplicationNamespace.Contracts.AnotherLeafContract)
        {
            targetType = typeof(ApplicationNamespace.Contracts.AnotherLeafContract);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<int>(2);
        }
        if (instance is ApplicationNamespace.Contracts.AnonymousLeafContract)
        {
            targetType = typeof(ApplicationNamespace.Contracts.AnonymousLeafContract);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("AnonymousLeafContract");
        }

        throw new System.ArgumentOutOfRangeException(nameof(instance), instance, null);
    }
}