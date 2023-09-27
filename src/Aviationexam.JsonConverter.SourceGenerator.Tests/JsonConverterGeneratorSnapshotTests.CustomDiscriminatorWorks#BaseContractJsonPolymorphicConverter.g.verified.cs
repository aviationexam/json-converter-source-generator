//HintName: BaseContractJsonPolymorphicConverter.g.cs
#nullable enable

namespace ApplicationNamespace;

internal class BaseContractJsonPolymorphicConverter : Aviationexam.JsonConverter.SourceGenerator.PolymorphicJsonConvertor<ApplicationNamespace.Contracts.BaseContract>
{
    protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "myCustomDiscriminator"u8;

    protected override System.Type GetTypeForDiscriminator(
        Aviationexam.JsonConverter.SourceGenerator.IDiscriminatorStruct discriminator
    ) => discriminator switch
    {
        Aviationexam.JsonConverter.SourceGenerator.DiscriminatorStruct<string> { Value: "LeafContract" } => typeof(ApplicationNamespace.Contracts.LeafContract),

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

        throw new System.ArgumentOutOfRangeException(nameof(type), type, null);
    }
}
