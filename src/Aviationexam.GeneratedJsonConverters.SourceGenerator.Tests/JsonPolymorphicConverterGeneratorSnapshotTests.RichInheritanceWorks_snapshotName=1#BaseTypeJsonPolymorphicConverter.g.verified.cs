//HintName: BaseTypeJsonPolymorphicConverter.g.cs
#nullable enable

namespace PolymorphicGlobalNamespace;

internal class BaseTypeJsonPolymorphicConverter :
    Aviationexam.GeneratedJsonConverters.PolymorphicJsonConvertor<BaseTypeJsonPolymorphicConverter, global::BaseType>,
    Aviationexam.GeneratedJsonConverters.IPolymorphicJsonConvertor<global::BaseType>
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
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "SecondLevel1A" } => typeof(SecondLevel1A),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "SecondLevel1B" } => typeof(SecondLevel1B),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "SecondLevel2A" } => typeof(SecondLevel2A),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "SecondLevel2B" } => typeof(SecondLevel2B),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "FirstLevel1" } => typeof(FirstLevel1),
        Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> { Value: "FirstLevel2" } => typeof(FirstLevel2),

        _ => throw new System.ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
    };

    public static Aviationexam.GeneratedJsonConverters.IDiscriminatorStruct GetDiscriminatorForInstance<TInstance>(
        TInstance instance, out System.Type targetType
    ) where TInstance : global::BaseType
    {
        if (instance is SecondLevel1A)
        {
            targetType = typeof(SecondLevel1A);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("SecondLevel1A");
        }
        if (instance is SecondLevel1B)
        {
            targetType = typeof(SecondLevel1B);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("SecondLevel1B");
        }
        if (instance is SecondLevel2A)
        {
            targetType = typeof(SecondLevel2A);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("SecondLevel2A");
        }
        if (instance is SecondLevel2B)
        {
            targetType = typeof(SecondLevel2B);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("SecondLevel2B");
        }
        if (instance is FirstLevel1)
        {
            targetType = typeof(FirstLevel1);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("FirstLevel1");
        }
        if (instance is FirstLevel2)
        {
            targetType = typeof(FirstLevel2);

            return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>("FirstLevel2");
        }

        throw new System.ArgumentOutOfRangeException(nameof(instance), instance, null);
    }

    public static void ConfigureJsonTypeInfo(System.Text.Json.Serialization.Metadata.JsonTypeInfo jsonTypeInfo)
    {
 
        if (jsonTypeInfo.Type == typeof(SecondLevel1A) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Add(System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = true,
                    DeclaringType = typeof(SecondLevel1A),
                    Converter = null,
                    Getter = static _ => "SecondLevel1A",
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

        if (jsonTypeInfo.Type == typeof(SecondLevel1B) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Add(System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = true,
                    DeclaringType = typeof(SecondLevel1B),
                    Converter = null,
                    Getter = static _ => "SecondLevel1B",
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

        if (jsonTypeInfo.Type == typeof(SecondLevel2A) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Add(System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = true,
                    DeclaringType = typeof(SecondLevel2A),
                    Converter = null,
                    Getter = static _ => "SecondLevel2A",
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

        if (jsonTypeInfo.Type == typeof(SecondLevel2B) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Add(System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = true,
                    DeclaringType = typeof(SecondLevel2B),
                    Converter = null,
                    Getter = static _ => "SecondLevel2B",
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

        if (jsonTypeInfo.Type == typeof(FirstLevel1) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Add(System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = true,
                    DeclaringType = typeof(FirstLevel1),
                    Converter = null,
                    Getter = static _ => "FirstLevel1",
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

        if (jsonTypeInfo.Type == typeof(FirstLevel2) && jsonTypeInfo.Kind is System.Text.Json.Serialization.Metadata.JsonTypeInfoKind.Object)
        {
            jsonTypeInfo.Properties.Add(System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo(
                jsonTypeInfo.Options,
                new System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
                {
                    IsProperty = false,
                    IsPublic = true,
                    IsVirtual = true,
                    DeclaringType = typeof(FirstLevel2),
                    Converter = null,
                    Getter = static _ => "FirstLevel2",
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