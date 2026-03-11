#if NET7_0_OR_GREATER
using System;
#endif

namespace Aviationexam.GeneratedJsonConverters;

internal interface IPolymorphicJsonConvertor<T> where T : class
{
#if NET7_0_OR_GREATER
    public static abstract ReadOnlySpan<byte> GetDiscriminatorPropertyName();

    public static abstract Type GetTypeForDiscriminator(IDiscriminatorStruct discriminator);

    public static abstract IDiscriminatorStruct GetDiscriminatorForInstance<TInstance>(
        TInstance instance, out Type targetType
    ) where TInstance : T;
#endif
}
