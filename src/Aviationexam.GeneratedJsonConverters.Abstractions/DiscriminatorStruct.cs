// ReSharper disable once RedundantNullableDirective

#nullable enable

namespace Aviationexam.GeneratedJsonConverters;

public readonly struct DiscriminatorStruct<T>(
    T value
) : IDiscriminatorStruct
{
    public T Value { get; init; } = value;
}
