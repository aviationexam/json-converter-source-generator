//HintName: DiscriminatorStruct.g.cs
// ReSharper disable once RedundantNullableDirective

#nullable enable

namespace Aviationexam.JsonConverter.SourceGenerator;

internal readonly struct DiscriminatorStruct<T> : IDiscriminatorStruct
{
    public T Value { get; init; }

    public DiscriminatorStruct(T value)
    {
        Value = value;
    }
}
