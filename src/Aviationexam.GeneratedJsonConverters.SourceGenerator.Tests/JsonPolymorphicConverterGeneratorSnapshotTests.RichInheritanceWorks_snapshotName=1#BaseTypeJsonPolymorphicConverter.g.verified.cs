//HintName: BaseTypeJsonPolymorphicConverter.g.cs
#nullable enable

namespace PolymorphicGlobalNamespace;

internal class BaseTypeJsonPolymorphicConverter : Aviationexam.GeneratedJsonConverters.PolymorphicJsonConvertor<global::BaseType>
{
    protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "$type"u8;

    protected override System.Type GetTypeForDiscriminator(
        Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct discriminator
    ) => discriminator switch
    {
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "SecondLevel1A" } => typeof(SecondLevel1A),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "SecondLevel1B" } => typeof(SecondLevel1B),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "SecondLevel2A" } => typeof(SecondLevel2A),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "SecondLevel2B" } => typeof(SecondLevel2B),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "FirstLevel1" } => typeof(FirstLevel1),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "FirstLevel2" } => typeof(FirstLevel2),

        _ => throw new System.ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
    };

    protected override Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct GetDiscriminatorForType(
        System.Type type
    )
    {
        if (type == typeof(SecondLevel1A))
        {
            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("SecondLevel1A");
        }
        if (type == typeof(SecondLevel1B))
        {
            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("SecondLevel1B");
        }
        if (type == typeof(SecondLevel2A))
        {
            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("SecondLevel2A");
        }
        if (type == typeof(SecondLevel2B))
        {
            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("SecondLevel2B");
        }
        if (type == typeof(FirstLevel1))
        {
            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("FirstLevel1");
        }
        if (type == typeof(FirstLevel2))
        {
            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("FirstLevel2");
        }

        throw new System.ArgumentOutOfRangeException(nameof(type), type, null);
    }
}
