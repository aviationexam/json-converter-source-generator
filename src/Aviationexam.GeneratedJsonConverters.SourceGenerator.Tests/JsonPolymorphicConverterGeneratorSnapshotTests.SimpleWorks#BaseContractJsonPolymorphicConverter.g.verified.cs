//HintName: BaseContractJsonPolymorphicConverter.g.cs
#nullable enable

namespace ApplicationNamespace;

internal class BaseContractJsonPolymorphicConverter :
    Aviationexam.GeneratedJsonConverters.PolymorphicJsonConvertor<BaseContractJsonPolymorphicConverter, global::ApplicationNamespace.Contracts.BaseContract>,
    Aviationexam.GeneratedJsonConverters.IPolymorphicJsonConvertor<global::ApplicationNamespace.Contracts.BaseContract>
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
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "LeafContract" } => typeof(ApplicationNamespace.Contracts.LeafContract),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<int> { Value: 2 } => typeof(ApplicationNamespace.Contracts.AnotherLeafContract),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "AnonymousLeafContract" } => typeof(ApplicationNamespace.Contracts.AnonymousLeafContract),

        _ => throw new System.ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
    };

    public static Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct GetDiscriminatorForInstance<TInstance>(
        TInstance instance, out System.Type targetType
    ) where TInstance : global::ApplicationNamespace.Contracts.BaseContract
    {
        if (instance is ApplicationNamespace.Contracts.LeafContract)
        {
            targetType = typeof(ApplicationNamespace.Contracts.LeafContract);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("LeafContract");
        }
        if (instance is ApplicationNamespace.Contracts.AnotherLeafContract)
        {
            targetType = typeof(ApplicationNamespace.Contracts.AnotherLeafContract);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<int>(2);
        }
        if (instance is ApplicationNamespace.Contracts.AnonymousLeafContract)
        {
            targetType = typeof(ApplicationNamespace.Contracts.AnonymousLeafContract);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("AnonymousLeafContract");
        }

        throw new System.ArgumentOutOfRangeException(nameof(instance), instance, null);
    }

    public static void ConfigureJsonTypeInfo(System.Text.Json.Serialization.Metadata.JsonTypeInfo jsonTypeInfo)
    {
 
        if (jsonTypeInfo.Type == typeof(ApplicationNamespace.Contracts.LeafContract) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Insert(0, System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = false,
                    DeclaringType = typeof(ApplicationNamespace.Contracts.LeafContract),
                    Converter = null,
                    Getter = static _ => "LeafContract",
                    Setter = null,
                    IgnoreCondition = null,
                    HasJsonInclude = false,
                    IsExtensionData = false,
                    NumberHandling = null,
                    PropertyName = "__jsonTypeDiscriminator",
                    JsonPropertyName = "$type"
                }
            ));
        }

        if (jsonTypeInfo.Type == typeof(ApplicationNamespace.Contracts.AnotherLeafContract) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Insert(0, System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<int>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = false,
                    DeclaringType = typeof(ApplicationNamespace.Contracts.AnotherLeafContract),
                    Converter = null,
                    Getter = static _ => 2,
                    Setter = null,
                    IgnoreCondition = null,
                    HasJsonInclude = false,
                    IsExtensionData = false,
                    NumberHandling = null,
                    PropertyName = "__jsonTypeDiscriminator",
                    JsonPropertyName = "$type"
                }
            ));
        }

        if (jsonTypeInfo.Type == typeof(ApplicationNamespace.Contracts.AnonymousLeafContract) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Insert(0, System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = false,
                    DeclaringType = typeof(ApplicationNamespace.Contracts.AnonymousLeafContract),
                    Converter = null,
                    Getter = static _ => "AnonymousLeafContract",
                    Setter = null,
                    IgnoreCondition = null,
                    HasJsonInclude = false,
                    IsExtensionData = false,
                    NumberHandling = null,
                    PropertyName = "__jsonTypeDiscriminator",
                    JsonPropertyName = "$type"
                }
            ));
        }

    }
}