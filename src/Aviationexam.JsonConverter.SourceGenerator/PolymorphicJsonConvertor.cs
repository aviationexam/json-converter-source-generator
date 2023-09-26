using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aviationexam.JsonConverter.SourceGenerator;

internal abstract class PolymorphicJsonConvertor<T> : JsonConverter<T>
{
    protected abstract string GetDiscriminatorProperty();

    protected abstract Type GetTypeForDiscriminator(string discriminator);

    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);

        string typeDiscriminator = jsonDoc.RootElement
            .GetProperty(GetDiscriminatorProperty())
            .GetString()!;

        var type = GetTypeForDiscriminator(typeDiscriminator);

        return (T?) jsonDoc.Deserialize(type, RemoveThisFromOptions(options));
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    private JsonSerializerOptions RemoveThisFromOptions(JsonSerializerOptions options)
    {
        JsonSerializerOptions newOptions = new(options);
        newOptions.Converters.Remove(this); // NOTE: We'll get an infinite loop if we don't do this
        return newOptions;
    }
}
