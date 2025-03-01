//HintName: BaseTypeOfTJsonPolymorphicConverter.g.cs
#nullable enable

namespace PolymorphicGlobalNamespace;

internal class BaseTypeOfTJsonPolymorphicConverter : Aviationexam.GeneratedJsonConverters.PolymorphicJsonConvertor<global::BaseType<T>>
{
    protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "$type"u8;

    protected override System.Type GetTypeForDiscriminator(
        Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct discriminator
    ) => discriminator switch
    {
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "int_LeafA" } => typeof(LeafA<System.Int32>),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "int_LeafB" } => typeof(LeafB<System.Int32>),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "string_LeafA" } => typeof(LeafA<System.String>),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "string_LeafB" } => typeof(LeafB<System.String>),

        _ => throw new System.ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
    };

    protected override Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct GetDiscriminatorForInstance<TInstance>(
        TInstance instance, out System.Type targetType
    )
    {
        if (instance is LeafA<System.Int32>)
        {
            targetType = typeof(LeafA<System.Int32>);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("int_LeafA");
        }
        if (instance is LeafB<System.Int32>)
        {
            targetType = typeof(LeafB<System.Int32>);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("int_LeafB");
        }
        if (instance is LeafA<System.String>)
        {
            targetType = typeof(LeafA<System.String>);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("string_LeafA");
        }
        if (instance is LeafB<System.String>)
        {
            targetType = typeof(LeafB<System.String>);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("string_LeafB");
        }

        throw new System.ArgumentOutOfRangeException(nameof(instance), instance, null);
    }
}