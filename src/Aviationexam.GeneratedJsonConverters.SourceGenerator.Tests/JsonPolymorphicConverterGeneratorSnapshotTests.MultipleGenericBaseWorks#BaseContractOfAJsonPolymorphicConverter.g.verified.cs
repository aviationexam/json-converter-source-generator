//HintName: BaseContractOfAJsonPolymorphicConverter.g.cs
#nullable enable

namespace ApplicationNamespace;

internal class BaseContractOfAJsonPolymorphicConverter :
    Aviationexam.GeneratedJsonConverters.PolymorphicJsonConvertor<BaseContractOfAJsonPolymorphicConverter, global::ApplicationNamespace.Contracts.BaseContract<global::ApplicationNamespace.Contracts.A>>,
    Aviationexam.GeneratedJsonConverters.IPolymorphicJsonConvertor<global::ApplicationNamespace.Contracts.BaseContract<global::ApplicationNamespace.Contracts.A>>
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

    public static System.ReadOnlySpan<byte> GetDiscriminatorPropertyName() => "myCustomDiscriminator"u8;

    public static System.Type GetTypeForDiscriminator(
        Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct discriminator
    ) => discriminator switch
    {
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "Leaf1_A" } => typeof(ApplicationNamespace.Contracts.Leaf1Contract<ApplicationNamespace.Contracts.A>),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "Leaf1_B" } => typeof(ApplicationNamespace.Contracts.Leaf1Contract<ApplicationNamespace.Contracts.B>),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "Leaf2_A" } => typeof(ApplicationNamespace.Contracts.Leaf2Contract<ApplicationNamespace.Contracts.A>),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "Leaf2_B" } => typeof(ApplicationNamespace.Contracts.Leaf2Contract<ApplicationNamespace.Contracts.B>),

        _ => throw new System.ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
    };

    public static Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct GetDiscriminatorForInstance<TInstance>(
        TInstance instance, out System.Type targetType
    ) where TInstance : global::ApplicationNamespace.Contracts.BaseContract<global::ApplicationNamespace.Contracts.A>
    {
        if (instance is ApplicationNamespace.Contracts.Leaf1Contract<ApplicationNamespace.Contracts.A>)
        {
            targetType = typeof(ApplicationNamespace.Contracts.Leaf1Contract<ApplicationNamespace.Contracts.A>);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("Leaf1_A");
        }
        if (instance is ApplicationNamespace.Contracts.Leaf1Contract<ApplicationNamespace.Contracts.B>)
        {
            targetType = typeof(ApplicationNamespace.Contracts.Leaf1Contract<ApplicationNamespace.Contracts.B>);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("Leaf1_B");
        }
        if (instance is ApplicationNamespace.Contracts.Leaf2Contract<ApplicationNamespace.Contracts.A>)
        {
            targetType = typeof(ApplicationNamespace.Contracts.Leaf2Contract<ApplicationNamespace.Contracts.A>);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("Leaf2_A");
        }
        if (instance is ApplicationNamespace.Contracts.Leaf2Contract<ApplicationNamespace.Contracts.B>)
        {
            targetType = typeof(ApplicationNamespace.Contracts.Leaf2Contract<ApplicationNamespace.Contracts.B>);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("Leaf2_B");
        }

        throw new System.ArgumentOutOfRangeException(nameof(instance), instance, null);
    }

    public static void ConfigureJsonTypeInfo(System.Text.Json.Serialization.Metadata.JsonTypeInfo jsonTypeInfo)
    {
 
        if (jsonTypeInfo.Type == typeof(ApplicationNamespace.Contracts.Leaf1Contract<ApplicationNamespace.Contracts.A>) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Insert(0, System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = false,
                    DeclaringType = typeof(ApplicationNamespace.Contracts.Leaf1Contract<ApplicationNamespace.Contracts.A>),
                    Converter = null,
                    Getter = static _ => "Leaf1_A",
                    Setter = null,
                    IgnoreCondition = null,
                    HasJsonInclude = false,
                    IsExtensionData = false,
                    NumberHandling = null,
                    PropertyName = "__jsonTypeDiscriminator",
                    JsonPropertyName = "myCustomDiscriminator"
                }
            ));
        }

        if (jsonTypeInfo.Type == typeof(ApplicationNamespace.Contracts.Leaf1Contract<ApplicationNamespace.Contracts.B>) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Insert(0, System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = false,
                    DeclaringType = typeof(ApplicationNamespace.Contracts.Leaf1Contract<ApplicationNamespace.Contracts.B>),
                    Converter = null,
                    Getter = static _ => "Leaf1_B",
                    Setter = null,
                    IgnoreCondition = null,
                    HasJsonInclude = false,
                    IsExtensionData = false,
                    NumberHandling = null,
                    PropertyName = "__jsonTypeDiscriminator",
                    JsonPropertyName = "myCustomDiscriminator"
                }
            ));
        }

        if (jsonTypeInfo.Type == typeof(ApplicationNamespace.Contracts.Leaf2Contract<ApplicationNamespace.Contracts.A>) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Insert(0, System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = false,
                    DeclaringType = typeof(ApplicationNamespace.Contracts.Leaf2Contract<ApplicationNamespace.Contracts.A>),
                    Converter = null,
                    Getter = static _ => "Leaf2_A",
                    Setter = null,
                    IgnoreCondition = null,
                    HasJsonInclude = false,
                    IsExtensionData = false,
                    NumberHandling = null,
                    PropertyName = "__jsonTypeDiscriminator",
                    JsonPropertyName = "myCustomDiscriminator"
                }
            ));
        }

        if (jsonTypeInfo.Type == typeof(ApplicationNamespace.Contracts.Leaf2Contract<ApplicationNamespace.Contracts.B>) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Insert(0, System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = false,
                    DeclaringType = typeof(ApplicationNamespace.Contracts.Leaf2Contract<ApplicationNamespace.Contracts.B>),
                    Converter = null,
                    Getter = static _ => "Leaf2_B",
                    Setter = null,
                    IgnoreCondition = null,
                    HasJsonInclude = false,
                    IsExtensionData = false,
                    NumberHandling = null,
                    PropertyName = "__jsonTypeDiscriminator",
                    JsonPropertyName = "myCustomDiscriminator"
                }
            ));
        }

    }
}