//HintName: BaseTypeOfTJsonPolymorphicConverter.g.cs
#nullable enable

namespace PolymorphicGlobalNamespace;

internal class BaseTypeOfTJsonPolymorphicConverter :
    Aviationexam.GeneratedJsonConverters.PolymorphicJsonConvertor<BaseTypeOfTJsonPolymorphicConverter, global::BaseType<T>>,
    Aviationexam.GeneratedJsonConverters.IPolymorphicJsonConvertor<global::BaseType<T>>
{
    #if !NET7_0_OR_GREATER

    protected override System.ReadOnlySpan<byte> Self_GetDiscriminatorPropertyName() => GetDiscriminatorPropertyName();

    protected override System.Type Self_GetTypeForDiscriminator(
        Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct discriminator
    ) => GetTypeForDiscriminator(discriminator);

    protected override Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct Self_GetDiscriminatorForInstance<TInstance>(
        TInstance instance, out System.Type targetType
    ) => GetDiscriminatorForInstance<TInstance>(instance, out targetType);

    #endif

    public static System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "$type"u8;

    public static System.Type GetTypeForDiscriminator(
        Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct discriminator
    ) => discriminator switch
    {

        _ => throw new System.ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
    };

    public static Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct GetDiscriminatorForInstance<TInstance>(
        TInstance instance, out System.Type targetType
    ) where TInstance : global::BaseType<T>
    {

        throw new System.ArgumentOutOfRangeException(nameof(instance), instance, null);
    }

    public static void ConfigureJsonTypeInfo(System.Text.Json.Serialization.Metadata.JsonTypeInfo jsonTypeInfo)
    {
 
    }
}