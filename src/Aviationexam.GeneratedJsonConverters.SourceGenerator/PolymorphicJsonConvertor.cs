// ReSharper disable once RedundantNullableDirective

#nullable enable

using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aviationexam.GeneratedJsonConverters;

internal abstract class PolymorphicJsonConvertor<T> : JsonConverter<T> where T : class
{
    private readonly Type _polymorphicType = typeof(T);

    protected abstract ReadOnlySpan<byte> GetDiscriminatorPropertyName();

    protected abstract Type GetTypeForDiscriminator(IDiscriminatorStruct discriminator);

    protected abstract IDiscriminatorStruct GetDiscriminatorForInstance<TInstance>(
        TInstance instance, out Type targetType
    ) where TInstance : T;

    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDocument = JsonDocument.ParseValue(ref reader);

        var discriminatorPropertyName = GetDiscriminatorPropertyName();

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

        var type = GetTypeForDiscriminator(typeDiscriminator);

        return (T?) jsonDocument.Deserialize(options.GetTypeInfo(type));
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        var discriminatorPropertyName = GetDiscriminatorPropertyName();
        var discriminatorValue = GetDiscriminatorForInstance(value, out var instanceType);

        if (discriminatorValue is DiscriminatorStruct<string> discriminatorString)
        {
            writer.WriteString(discriminatorPropertyName, discriminatorString.Value);
        }
        else if (discriminatorValue is DiscriminatorStruct<int> discriminatorInt)
        {
            writer.WriteNumber(discriminatorPropertyName, discriminatorInt.Value);
        }

        var typeInfo = options.GetTypeInfo(instanceType);

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
