﻿//HintName: BaseContractOfStringJsonPolymorphicConverter.g.cs
#nullable enable

namespace ApplicationNamespace;

internal class BaseContractOfStringJsonPolymorphicConverter : Aviationexam.GeneratedJsonConverters.PolymorphicJsonConvertor<global::ApplicationNamespace.Contracts.BaseContract<global::System.String>>
{
    protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "myCustomDiscriminator"u8;

    protected override System.Type GetTypeForDiscriminator(
        Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct discriminator
    ) => discriminator switch
    {
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "IntLeafContract" } => typeof(ApplicationNamespace.Contracts.IntLeafContract),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "StringLeafContract" } => typeof(ApplicationNamespace.Contracts.StringLeafContract),

        _ => throw new System.ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
    };

    protected override Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct GetDiscriminatorForType(
        System.Type type
    )
    {
        if (type == typeof(ApplicationNamespace.Contracts.IntLeafContract))
        {
            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("IntLeafContract");
        }
        if (type == typeof(ApplicationNamespace.Contracts.StringLeafContract))
        {
            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("StringLeafContract");
        }

        throw new System.ArgumentOutOfRangeException(nameof(type), type, null);
    }
}
