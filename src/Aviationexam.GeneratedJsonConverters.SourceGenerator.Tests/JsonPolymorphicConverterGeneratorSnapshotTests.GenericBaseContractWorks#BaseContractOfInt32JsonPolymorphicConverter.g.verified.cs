//HintName: BaseContractOfInt32JsonPolymorphicConverter.g.cs
#nullable enable

namespace ApplicationNamespace;

internal class BaseContractOfInt32JsonPolymorphicConverter :
    Aviationexam.GeneratedJsonConverters.PolymorphicJsonConvertor<BaseContractOfInt32JsonPolymorphicConverter, global::ApplicationNamespace.Contracts.BaseContract<global::System.Int32>>,
    Aviationexam.GeneratedJsonConverters.IPolymorphicJsonConvertor<global::ApplicationNamespace.Contracts.BaseContract<global::System.Int32>>
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
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "IntLeafContract" } => typeof(ApplicationNamespace.Contracts.IntLeafContract),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "StringLeafContract" } => typeof(ApplicationNamespace.Contracts.StringLeafContract),

        _ => throw new System.ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
    };

    public static Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct GetDiscriminatorForInstance<TInstance>(
        TInstance instance, out System.Type targetType
    ) where TInstance : global::ApplicationNamespace.Contracts.BaseContract<global::System.Int32>
    {
        if (instance is ApplicationNamespace.Contracts.IntLeafContract)
        {
            targetType = typeof(ApplicationNamespace.Contracts.IntLeafContract);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("IntLeafContract");
        }
        if (instance is ApplicationNamespace.Contracts.StringLeafContract)
        {
            targetType = typeof(ApplicationNamespace.Contracts.StringLeafContract);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("StringLeafContract");
        }

        throw new System.ArgumentOutOfRangeException(nameof(instance), instance, null);
    }

    public static void ConfigureJsonTypeInfo(System.Text.Json.Serialization.Metadata.JsonTypeInfo jsonTypeInfo)
    {
 
        if (jsonTypeInfo.Type == typeof(ApplicationNamespace.Contracts.IntLeafContract) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Insert(0, System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = false,
                    DeclaringType = typeof(ApplicationNamespace.Contracts.IntLeafContract),
                    Converter = null,
                    Getter = static _ => "IntLeafContract",
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

        if (jsonTypeInfo.Type == typeof(ApplicationNamespace.Contracts.StringLeafContract) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Insert(0, System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = false,
                    DeclaringType = typeof(ApplicationNamespace.Contracts.StringLeafContract),
                    Converter = null,
                    Getter = static _ => "StringLeafContract",
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