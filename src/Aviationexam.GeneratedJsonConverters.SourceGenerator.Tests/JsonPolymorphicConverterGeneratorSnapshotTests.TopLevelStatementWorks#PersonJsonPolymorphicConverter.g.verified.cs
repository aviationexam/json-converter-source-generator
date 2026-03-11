//HintName: PersonJsonPolymorphicConverter.g.cs
#nullable enable

namespace PolymorphicGlobalNamespace;

internal class PersonJsonPolymorphicConverter :
    Aviationexam.GeneratedJsonConverters.PolymorphicJsonConvertor<PersonJsonPolymorphicConverter, global::Person>,
    Aviationexam.GeneratedJsonConverters.IPolymorphicJsonConvertor<global::Person>
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
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "Student" } => typeof(Student),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "Teacher" } => typeof(Teacher),

        _ => throw new System.ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
    };

    public static Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct GetDiscriminatorForInstance<TInstance>(
        TInstance instance, out System.Type targetType
    ) where TInstance : global::Person
    {
        if (instance is Student)
        {
            targetType = typeof(Student);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("Student");
        }
        if (instance is Teacher)
        {
            targetType = typeof(Teacher);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("Teacher");
        }

        throw new System.ArgumentOutOfRangeException(nameof(instance), instance, null);
    }
}