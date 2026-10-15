// ReSharper disable once RedundantNullableDirective

#nullable enable

using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aviationexam.GeneratedJsonConverters;

/// <summary>
/// Base of every generated polymorphic converter. Its members are the same in every target framework asset,
/// so a converter compiled against the netstandard2.0 asset still loads when a net8.0+ application picks the net8.0+ asset.
/// </summary>
public abstract class PolymorphicJsonConvertor<T> : JsonConverter<T>
    where T : class
{
    private readonly Type _polymorphicType = typeof(T);

    protected abstract ReadOnlySpan<byte> Self_GetDiscriminatorPropertyName();

    protected abstract Type Self_GetTypeForDiscriminator(IDiscriminatorStruct discriminator);

    protected abstract IDiscriminatorStruct Self_GetDiscriminatorForInstance<TInstance>(
        TInstance instance, out Type targetType
    ) where TInstance : T;

    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDocument = JsonDocument.ParseValue(ref reader);

        var discriminatorPropertyName =
            Self_GetDiscriminatorPropertyName();

        var discriminatorProperty = jsonDocument.RootElement
            .GetProperty(discriminatorPropertyName);

        IDiscriminatorStruct? typeDiscriminator = null;
        if (discriminatorProperty.ValueKind is JsonValueKind.String)
        {
            typeDiscriminator = new DiscriminatorStruct<string>(discriminatorProperty.GetString()!);
        }
        else if (discriminatorProperty.ValueKind is JsonValueKind.Number)
        {
            typeDiscriminator = new DiscriminatorStruct<int>(discriminatorProperty.GetInt32());
        }

        if (typeDiscriminator is null)
        {
            var discriminatorPropertyNameString = Encoding.UTF8.GetString(discriminatorPropertyName.ToArray());

            throw new JsonException($"Not found discriminator property '{discriminatorPropertyNameString}' for type {_polymorphicType}");
        }

        var type =
            Self_GetTypeForDiscriminator(typeDiscriminator);

        return (T?) jsonDocument.Deserialize(options.GetTypeInfo(type));
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        var discriminatorPropertyName =
            Self_GetDiscriminatorPropertyName();
        var discriminatorValue =
            Self_GetDiscriminatorForInstance(value, out var instanceType);

        var typeInfo = options.GetTypeInfo(instanceType);

        if (
            typeInfo.Properties.Count > 0
            && Encoding.UTF8.GetBytes(typeInfo.Properties[0].Name).SequenceEqual(discriminatorPropertyName)
        )
        {
            JsonSerializer.Serialize(writer, value, typeInfo);

            return;
        }

        writer.WriteStartObject();

        if (discriminatorValue is DiscriminatorStruct<string> discriminatorString)
        {
            writer.WriteString(discriminatorPropertyName, discriminatorString.Value);
        }
        else if (discriminatorValue is DiscriminatorStruct<int> discriminatorInt)
        {
            writer.WriteNumber(discriminatorPropertyName, discriminatorInt.Value);
        }

        foreach (var p in typeInfo.Properties)
        {
            if (p.Get is null)
            {
                continue;
            }

            var realValue = p.Get(value);

            if (
                p.ShouldSerialize is { } shouldSerialize
                && !shouldSerialize(value, realValue)
            )
            {
                continue;
            }

            if (
                options.DefaultIgnoreCondition is JsonIgnoreCondition.Always
#if NET_10_OR_GREATER
                || options.DefaultIgnoreCondition is JsonIgnoreCondition.WhenWriting
#endif
                || (options.DefaultIgnoreCondition is JsonIgnoreCondition.WhenWritingDefault && realValue == null)
                || (options.DefaultIgnoreCondition is JsonIgnoreCondition.WhenWritingNull && realValue is null)
            )
            {
                continue;
            }

            writer.WritePropertyName(p.Name);
            JsonSerializer.Serialize(writer, realValue, options.GetTypeInfo(p.PropertyType));
        }

        writer.WriteEndObject();
    }
}

#if NET8_0_OR_GREATER
/// <summary>
/// Base of polymorphic converters generated for net8.0+, which supply the discriminator mapping through the static abstract members of <see cref="IPolymorphicJsonConvertor{T}"/>.
/// </summary>
public abstract class PolymorphicJsonConvertor<TConverter, T> : PolymorphicJsonConvertor<T>
    where TConverter : PolymorphicJsonConvertor<TConverter, T>, IPolymorphicJsonConvertor<T>
    where T : class
{
    protected sealed override ReadOnlySpan<byte> Self_GetDiscriminatorPropertyName() => TConverter.GetDiscriminatorPropertyName();

    protected sealed override Type Self_GetTypeForDiscriminator(
        IDiscriminatorStruct discriminator
    ) => TConverter.GetTypeForDiscriminator(discriminator);

    protected sealed override IDiscriminatorStruct Self_GetDiscriminatorForInstance<TInstance>(
        TInstance instance, out Type targetType
    ) => TConverter.GetDiscriminatorForInstance(instance, out targetType);
}
#endif
