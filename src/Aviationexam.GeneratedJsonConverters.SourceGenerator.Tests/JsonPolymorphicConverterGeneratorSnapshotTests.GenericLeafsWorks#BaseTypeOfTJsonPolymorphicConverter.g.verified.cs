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
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "int_LeafA" } => typeof(LeafA<System.Int32>),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "int_LeafB" } => typeof(LeafB<System.Int32>),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "string_LeafA" } => typeof(LeafA<System.String>),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "string_LeafB" } => typeof(LeafB<System.String>),

        _ => throw new System.ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
    };

    public static Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct GetDiscriminatorForInstance<TInstance>(
        TInstance instance, out System.Type targetType
    ) where TInstance : global::BaseType<T>
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

    public static void ConfigureJsonTypeInfo(System.Text.Json.Serialization.Metadata.JsonTypeInfo jsonTypeInfo)
    {
 
        if (jsonTypeInfo.Type == typeof(LeafA<System.Int32>) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Insert(0, System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = false,
                    DeclaringType = typeof(LeafA<System.Int32>),
                    Converter = null,
                    Getter = static _ => "int_LeafA",
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

        if (jsonTypeInfo.Type == typeof(LeafB<System.Int32>) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Insert(0, System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = false,
                    DeclaringType = typeof(LeafB<System.Int32>),
                    Converter = null,
                    Getter = static _ => "int_LeafB",
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

        if (jsonTypeInfo.Type == typeof(LeafA<System.String>) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Insert(0, System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = false,
                    DeclaringType = typeof(LeafA<System.String>),
                    Converter = null,
                    Getter = static _ => "string_LeafA",
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

        if (jsonTypeInfo.Type == typeof(LeafB<System.String>) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Insert(0, System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = false,
                    DeclaringType = typeof(LeafB<System.String>),
                    Converter = null,
                    Getter = static _ => "string_LeafB",
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