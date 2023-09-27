//HintName: BaseContractJsonPolymorphicConverter.g.cs
#nullable enable

namespace ApplicationNamespace;

internal class BaseContractJsonPolymorphicConverter : Aviationexam.JsonConverter.SourceGenerator.PolymorphicJsonConvertor<ApplicationNamespace.Contracts.BaseContract>
{
    protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "$type"u8;

    protected override System.Type GetTypeForDiscriminator(
        Aviationexam.JsonConverter.SourceGenerator.IDiscriminatorStruct discriminator
    ) => discriminator switch
    {
        Aviationexam.JsonConverter.SourceGenerator.DiscriminatorStruct<string> { Value: "LeafContract" } => typeof(ApplicationNamespace.Contracts.LeafContract),
        Aviationexam.JsonConverter.SourceGenerator.DiscriminatorStruct<int> { Value: 2 } => typeof(ApplicationNamespace.Contracts.AnotherLeafContract),
        Aviationexam.JsonConverter.SourceGenerator.DiscriminatorStruct<string> { Value: "AnonymousLeafContract" } => typeof(ApplicationNamespace.Contracts.AnonymousLeafContract),

        _ => throw new System.ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
    };

    protected override Aviationexam.JsonConverter.SourceGenerator.IDiscriminatorStruct GetDiscriminatorForType(
        System.Type type
    )
    {
        if (type == typeof(ApplicationNamespace.Contracts.LeafContract))
        {
            return new Aviationexam.JsonConverter.SourceGenerator.DiscriminatorStruct<string>("LeafContract");
        }
        if (type == typeof(ApplicationNamespace.Contracts.AnotherLeafContract))
        {
            return new Aviationexam.JsonConverter.SourceGenerator.DiscriminatorStruct<int>(2);
        }
        if (type == typeof(ApplicationNamespace.Contracts.AnonymousLeafContract))
        {
            return new Aviationexam.JsonConverter.SourceGenerator.DiscriminatorStruct<string>("AnonymousLeafContract");
        }

        throw new System.ArgumentOutOfRangeException(nameof(type), type, null);
    }
}
