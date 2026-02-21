//HintName: LeafPolymorphicJsonConvertor.g.cs
// ReSharper disable once RedundantNullableDirective

#nullable enable

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Aviationexam.GeneratedJsonConverters;

internal abstract class LeafPolymorphicJsonConvertor<T> : JsonConverter<T> where T : class
{
    private JsonSerializerOptions? _optionsWithoutSelf;

    protected abstract ReadOnlySpan<byte> GetDiscriminatorPropertyName();

    protected abstract void WriteDiscriminatorValue(Utf8JsonWriter writer);

    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => (T?) JsonSerializer.Deserialize(ref reader, GetTypeInfoWithoutSelf(options));

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        WriteDiscriminatorValue(writer);

        var typeInfo = GetTypeInfoWithoutSelf(options);

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

    private JsonTypeInfo<T> GetTypeInfoWithoutSelf(JsonSerializerOptions options)
    {
        // options.GetTypeInfo(typeof(T)) returns a converter-backed typeinfo with no property
        // metadata when this converter is registered in options.Converters. Build a once-per-
        // options-instance copy that omits this converter so the source-generated property
        // metadata is used instead.
        if (_optionsWithoutSelf is null)
        {
            var newOptions = new JsonSerializerOptions(options);

            for (var i = newOptions.Converters.Count - 1; i >= 0; i--)
            {
                if (newOptions.Converters[i] == this)
                {
                    newOptions.Converters.RemoveAt(i);
                }
            }

            newOptions.MakeReadOnly();
            _optionsWithoutSelf = newOptions;
        }

        return (JsonTypeInfo<T>) _optionsWithoutSelf.GetTypeInfo(typeof(T));
    }
}
