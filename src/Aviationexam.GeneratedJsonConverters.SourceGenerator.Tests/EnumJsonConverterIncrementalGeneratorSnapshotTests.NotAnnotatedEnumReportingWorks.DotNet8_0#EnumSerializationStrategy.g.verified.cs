//HintName: EnumSerializationStrategy.g.cs
// ReSharper disable once RedundantNullableDirective

#nullable enable

using Aviationexam.GeneratedJsonConverters.Attributes;
using System;

namespace Aviationexam.GeneratedJsonConverters;

[Flags]
[DisableEnumJsonConverter]
internal enum EnumSerializationStrategy : byte
{
    ProjectDefault = 0,
    BackingType = 1 << 0,
    FirstEnumName = 1 << 1,
    FlagsArray = 1 << 2,
}
