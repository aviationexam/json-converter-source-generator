namespace Aviationexam.GeneratedJsonConverters;

public interface IPolymorphicJsonConvertor<T> where T : class
{
#if NET8_0_OR_GREATER
    public static abstract System.ReadOnlySpan<byte> GetDiscriminatorPropertyName();

    public static abstract System.Type GetTypeForDiscriminator(IDiscriminatorStruct discriminator);

    public static abstract IDiscriminatorStruct GetDiscriminatorForInstance<TInstance>(
        TInstance instance, out System.Type targetType
    ) where TInstance : T;

    public static abstract void ConfigureJsonTypeInfo(System.Text.Json.Serialization.Metadata.JsonTypeInfo jsonTypeInfo);
#endif
}
