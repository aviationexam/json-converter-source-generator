//HintName: PersonJsonPolymorphicConverter.g.cs
#nullable enable

namespace PolymorphicGlobalNamespace;

internal class PersonJsonPolymorphicConverter : Aviationexam.GeneratedJsonConverters.PolymorphicJsonConvertor<global::Person>
{
    protected override System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "$type"u8;

    protected override System.Type GetTypeForDiscriminator(
        Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct discriminator
    ) => discriminator switch
    {
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "Student" } => typeof(Student),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "Teacher" } => typeof(Teacher),

        _ => throw new System.ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
    };

    protected override Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct GetDiscriminatorForType(
        System.Type type
    )
    {
        if (type == typeof(Student))
        {
            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("Student");
        }
        if (type == typeof(Teacher))
        {
            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("Teacher");
        }

        throw new System.ArgumentOutOfRangeException(nameof(type), type, null);
    }

    protected override Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct GetDiscriminatorForInstance<TInstance>(
        TInstance instance, out System.Type targetType
    )
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