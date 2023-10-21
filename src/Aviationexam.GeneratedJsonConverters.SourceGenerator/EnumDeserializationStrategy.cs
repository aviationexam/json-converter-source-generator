using System;

namespace Aviationexam.GeneratedJsonConverters;

[Flags]
internal enum EnumDeserializationStrategy : byte
{
    ProjectDefault = 0,
    UseBackingType = 1 << 0,
    UseEnumName = 1 << 1,
}
