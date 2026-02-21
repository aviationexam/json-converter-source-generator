//HintName: LeafPolymorphicJsonConvertor.g.cs
// ReSharper disable once RedundantNullableDirective

#nullable enable

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aviationexam.GeneratedJsonConverters;

internal abstract class LeafPolymorphicJsonConvertor<T> : JsonConverter<T> where T : class
{
    protected abstract ReadOnlySpan<byte> GetDiscriminatorPropertyName();

    protected abstract void WriteDiscriminatorValue(Utf8JsonWriter writer);

    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => (T?) JsonSerializer.Deserialize(ref reader, options.GetTypeInfo(typeof(T)));

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        WriteDiscriminatorValue(writer);

        var typeInfo = options.GetTypeInfo(typeof(T));

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
