#if NET7_0_OR_GREATER
using System;
#endif

namespace Aviationexam.GeneratedJsonConverters;

public interface IPolymorphicJsonConvertor
{
#if NET7_0_OR_GREATER
    public static abstract ReadOnlySpan<byte> GetDiscriminatorPropertyName();

    public static abstract Type GetTypeForDiscriminator(IDiscriminatorStruct discriminator);
#endif
}
