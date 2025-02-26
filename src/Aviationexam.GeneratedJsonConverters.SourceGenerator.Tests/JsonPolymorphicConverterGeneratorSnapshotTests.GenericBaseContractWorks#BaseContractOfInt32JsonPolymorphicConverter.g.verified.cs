//HintName: BaseContractOfInt32JsonPolymorphicConverter.g.cs
#nullable enable

namespace ApplicationNamespace;

internal class BaseContractOfInt32JsonPolymorphicConverter : Aviationexam.GeneratedJsonConverters.PolymorphicJsonConvertor<global::ApplicationNamespace.Contracts.BaseContract<global::System.Int32>>
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

    protected override Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct GetDiscriminatorForInstance<TInstance>(
        TInstance instance, out System.Type targetType
    )
    {
        if (instance is ApplicationNamespace.Contracts.IntLeafContract)
        {
            targetType = typeof(ApplicationNamespace.Contracts.IntLeafContract);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("IntLeafContract");
        }
        if (instance is ApplicationNamespace.Contracts.StringLeafContract)
        {
            targetType = typeof(ApplicationNamespace.Contracts.StringLeafContract);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("StringLeafContract");
        }

        throw new System.ArgumentOutOfRangeException(nameof(instance), instance, null);
    }
}