// ReSharper disable once RedundantNullableDirective

#nullable enable

namespace Aviationexam.GeneratedJsonConverters;

public readonly struct DiscriminatorStruct<T>(
    T value
) : IDiscriminatorStruct
{
    public T Value { get; init; } = value;

    public override string ToString() => Value switch
    {
        System.IFormattable formattable => formattable.ToString(null, System.Globalization.CultureInfo.InvariantCulture),
        null => string.Empty,
        _ => Value.ToString() ?? string.Empty,
    };
}
