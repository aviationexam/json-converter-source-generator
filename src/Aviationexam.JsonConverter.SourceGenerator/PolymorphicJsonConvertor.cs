// ReSharper disable once RedundantNullableDirective

#nullable enable

using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aviationexam.JsonConverter.SourceGenerator;

internal abstract class PolymorphicJsonConvertor<T> : JsonConverter<T> where T : class
{
    private readonly Type _polymorphicType = typeof(T);

    protected abstract ReadOnlySpan<byte> GetDiscriminatorPropertyName();

    protected abstract Type GetTypeForDiscriminator(IDiscriminatorStruct discriminator);

    protected abstract IDiscriminatorStruct GetDiscriminatorForType(Type type);

    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDocument = JsonDocument.ParseValue(ref reader);

        var discriminatorPropertyName = GetDiscriminatorPropertyName();

        var discriminatorProperty = jsonDocument.RootElement
            .GetProperty(discriminatorPropertyName);

        string? typeDiscriminator = null;
        if (discriminatorProperty.ValueKind is JsonValueKind.String)
        {
            typeDiscriminator = discriminatorProperty.GetString();
        }
        else if (discriminatorProperty.ValueKind is JsonValueKind.Number)
        {
            typeDiscriminator = discriminatorProperty.GetInt32().ToString();
        }

        if (typeDiscriminator is null)
        {
            var discriminatorPropertyNameString = Encoding.UTF8.GetString(discriminatorPropertyName.ToArray());

            throw new JsonException($"Not found discriminator property '{discriminatorPropertyNameString}' for type {_polymorphicType}");
        }

        var type = GetTypeForDiscriminator(typeDiscriminator);

        return (T?) jsonDocument.Deserialize(type, options);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (RemoveThisFromOptions(options).GetConverter(_polymorphicType) is not JsonConverter<T> converter)
        {
            throw new JsonException($"Missing default converter for type {_polymorphicType}");
        }

        converter.Write(writer, value, options);
    }

    private JsonSerializerOptions RemoveThisFromOptions(JsonSerializerOptions options)
    {
        JsonSerializerOptions newOptions = new(options);
        newOptions.Converters.Remove(this);
        return newOptions;
    }
}
