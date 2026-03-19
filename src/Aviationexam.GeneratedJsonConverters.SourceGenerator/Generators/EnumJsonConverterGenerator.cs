using Aviationexam.GeneratedJsonConverters.SourceGenerator.Parsers;
using H.Generators.Extensions;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using ZLinq;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Generators;

internal static class EnumJsonConverterGenerator
{
    private const string MethodPrefix = "    ";
    private const string Indention = "    ";

    public static FileWithName? Generate(
        EnumJsonConverterOptions enumJsonConverterOptions,
        string targetNamespace,
        EnumJsonConverterConfiguration enumJsonConverterConfiguration,
        INamedTypeSymbol enumSymbol,
        INamedTypeSymbol enumMemberAttributeSymbol,
        bool isFlagsEnum,
        out bool hasFlagsArrayOnNonFlagsEnum,
        out string converterName
    )
    {
        converterName = GenerateConverterName(enumSymbol);

        var fullName = enumSymbol.ToDisplayString(EnumJsonConverterIncrementalGenerator.NamespaceFormat);

        var enumTypeCode = BackingTypeToTypeCode(
            enumSymbol.EnumUnderlyingType
            ?? throw new ArgumentNullException(nameof(enumSymbol.EnumUnderlyingType))
        );

        var backingTypeSerialization = new Dictionary<string, object>();
        var backingTypeDeserialization = new Dictionary<object, string>();

        var fieldNameSerialization = new Dictionary<string, string>();
        var fieldNameDeserialization = new Dictionary<string, string>();

        Type? backingType = null;
        foreach (var typeMember in enumSymbol.GetMembers().OfType<IFieldSymbol>())
        {
            var constantValue = typeMember.ConstantValue ?? throw new NullReferenceException(nameof(typeMember.ConstantValue));
            backingType = constantValue.GetType();

            var fieldName = typeMember.Name;
            var enumMember = typeMember.GetAttributes()
                .AsValueEnumerable()
                .Where(x => SymbolEqualityComparer.Default.Equals(x.AttributeClass, enumMemberAttributeSymbol))
                .Select(EnumMemberAttributeParser.Parse)
                .SingleOrDefault();

            if (enumMember is not null)
            {
                fieldName = enumMember;
            }

            if (!fieldNameDeserialization.ContainsKey(fieldName))
            {
                fieldNameDeserialization.Add(fieldName, typeMember.Name);
            }

            if (
                !fieldNameSerialization.ContainsKey(typeMember.Name)
                && !backingTypeDeserialization.ContainsKey(constantValue)
            )
            {
                fieldNameSerialization.Add(typeMember.Name, fieldName);
            }


            if (
                !backingTypeSerialization.ContainsKey(typeMember.Name)
                && !backingTypeDeserialization.ContainsKey(constantValue)
            )
            {
                backingTypeSerialization.Add(typeMember.Name, constantValue);
            }

            if (!backingTypeDeserialization.ContainsKey(constantValue))
            {
                backingTypeDeserialization.Add(constantValue, typeMember.Name);
            }
        }

        if (backingType is null)
        {
            hasFlagsArrayOnNonFlagsEnum = false;
            return null;
        }

        var serializationStrategies = enumJsonConverterConfiguration.SerializationStrategies;
        var isUsingProjectDefaults = serializationStrategies.IsEmpty;
        if (isUsingProjectDefaults)
        {
            serializationStrategies = enumJsonConverterOptions.DefaultEnumSerializationStrategies;
        }

        var hasFlagsArraySerialization = serializationStrategies.AsValueEnumerable().Any(x => x is EnumSerializationStrategy.FlagsArray);

        // When FlagsArray comes from project defaults, silently strip it for non-[Flags] enums
        // Only warn when FlagsArray is explicitly set per-enum on a non-[Flags] enum
        if (hasFlagsArraySerialization && !isFlagsEnum)
        {
            if (isUsingProjectDefaults)
            {
                serializationStrategies = serializationStrategies.RemoveAll(x => x is EnumSerializationStrategy.FlagsArray);
                hasFlagsArraySerialization = false;

                // If stripping FlagsArray left no strategies, fall back to FirstEnumName
                if (serializationStrategies.IsEmpty)
                {
                    serializationStrategies = [EnumSerializationStrategy.FirstEnumName];
                }
            }
        }

        hasFlagsArrayOnNonFlagsEnum = hasFlagsArraySerialization && !isFlagsEnum;
        var hasFirstEnumNameSerialization = serializationStrategies.AsValueEnumerable().Any(x => x is EnumSerializationStrategy.FirstEnumName);
        var hasBackingTypeSerialization = serializationStrategies.AsValueEnumerable().Any(x => x is EnumSerializationStrategy.BackingType);

        var serializationStrategyFormatted = serializationStrategies
            .AsValueEnumerable()
            .Select(x => $"Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy.{x}")
            .JoinToString(" | ");

        var deserializationStrategies = enumJsonConverterConfiguration.DeserializationStrategies;
        if (deserializationStrategies.IsEmpty)
        {
            deserializationStrategies = enumJsonConverterOptions.DefaultEnumDeserializationStrategies;
        }

        var deserializationStrategy = deserializationStrategies
            .AsValueEnumerable()
            .Select(x => $"Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.{x}")
            .JoinToString(" | ");

        var toEnumFromString = GenerateToEnumFromString(deserializationStrategies, fullName, fieldNameDeserialization);
        var toEnumFromBackingType = GenerateToEnumFromBackingType(deserializationStrategies, fullName, backingTypeDeserialization);
        var toStringFromEnum = GenerateToStringFromEnum(hasFirstEnumNameSerialization, fullName, fieldNameSerialization);
        var toBackingTypeFromEnum = GenerateToBackingTypeFromEnum(hasBackingTypeSerialization, fullName, backingTypeSerialization);
        var writeFlagsAsArray = GenerateWriteFlagsAsArray(
            isFlagsEnum,
            hasFlagsArraySerialization,
            hasFirstEnumNameSerialization,
            hasBackingTypeSerialization,
            fullName,
            backingType,
            fieldNameSerialization,
            backingTypeSerialization
        );

        return new FileWithName(
            $"{converterName}.g.cs",
            // language=cs
            $$"""
              #nullable enable

              namespace {{targetNamespace}};

              internal class {{converterName}} : Aviationexam.GeneratedJsonConverters.EnumJsonConvertor<{{fullName}}, {{backingType}}>
              {
                  protected override System.TypeCode BackingTypeTypeCode => System.TypeCode.{{enumTypeCode}};

                  protected override Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy DeserializationStrategy => {{deserializationStrategy}};

                   protected override Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy SerializationStrategy => {{serializationStrategyFormatted}};

                  public override bool TryToEnum(
                      System.ReadOnlySpan<byte> enumName, out {{fullName}} value
                  ){{toEnumFromString}}

                  public override bool TryToEnum(
                      {{backingType}} numericValue, out {{fullName}} value
                  ){{toEnumFromBackingType}}

                  public override {{backingType}} ToBackingType(
                      {{fullName}} value
                  ){{toBackingTypeFromEnum}}

                  public override System.ReadOnlySpan<byte> ToFirstEnumName(
                      {{fullName}} value
                  ){{toStringFromEnum}}

                  protected override void WriteFlagsAsArray(
                      System.Text.Json.Utf8JsonWriter writer,
                      {{fullName}} value,
                      System.Text.Json.JsonSerializerOptions options
                  ){{writeFlagsAsArray}}
              }
              """
        );
    }

    private static string GenerateToEnumFromString(
        ImmutableArray<EnumDeserializationStrategy> enumDeserializationStrategies,
        string enumFullName,
        IDictionary<string, string> backingTypeDeserialization
    )
    {
        if (enumDeserializationStrategies.AsValueEnumerable().Any(x => x is EnumDeserializationStrategy.UseEnumName))
        {
            const string propertyName = "enumName";

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine();
            stringBuilder.Append(MethodPrefix);
            stringBuilder.AppendLine("{");

            foreach (var mapping in backingTypeDeserialization)
            {
                stringBuilder.Append(MethodPrefix);
                stringBuilder.Append(Indention);
                stringBuilder.Append($"if (System.MemoryExtensions.SequenceEqual({propertyName}, \"");
                stringBuilder.Append(mapping.Key);
                stringBuilder.AppendLine("\"u8))");

                stringBuilder.Append(MethodPrefix);
                stringBuilder.Append(Indention);
                stringBuilder.AppendLine("{");

                stringBuilder.Append(MethodPrefix);
                stringBuilder.Append(Indention);
                stringBuilder.Append(Indention);
                stringBuilder.Append("value = ");
                stringBuilder.Append(enumFullName);
                stringBuilder.Append(".");
                stringBuilder.Append(mapping.Value);
                stringBuilder.AppendLine(";");

                stringBuilder.Append(MethodPrefix);
                stringBuilder.Append(Indention);
                stringBuilder.Append(Indention);
                stringBuilder.AppendLine("return true;");

                stringBuilder.Append(MethodPrefix);
                stringBuilder.Append(Indention);
                stringBuilder.AppendLine("}");
            }

            stringBuilder.AppendLine();

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append(Indention);
            stringBuilder.AppendLine(
                // language=cs
                $"""
                 value = default({enumFullName});
                 """
            );

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append(Indention);
            stringBuilder.AppendLine(
                // language=cs
                """
                return false;
                """
            );

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append("}");

            return stringBuilder.ToString();
        }

        return GenerateToEnumException("enum name");
    }

    private static string GenerateToEnumFromBackingType(
        ImmutableArray<EnumDeserializationStrategy> enumDeserializationStrategies,
        string enumFullName,
        IDictionary<object, string> backingTypeDeserialization
    )
    {
        if (enumDeserializationStrategies.AsValueEnumerable().Any(x => x is EnumDeserializationStrategy.UseBackingType))
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine();
            stringBuilder.Append(MethodPrefix);
            stringBuilder.AppendLine("{");

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append(Indention);
            stringBuilder.AppendLine("(var tryValue, value) = numericValue switch");

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append(Indention);
            stringBuilder.AppendLine("{");

            foreach (var mapping in backingTypeDeserialization)
            {
                stringBuilder.Append(MethodPrefix);
                stringBuilder.Append(Indention);
                stringBuilder.Append(Indention);
                stringBuilder.Append(mapping.Key);
                stringBuilder.Append(" => (true, ");
                stringBuilder.Append(enumFullName);
                stringBuilder.Append(".");
                stringBuilder.Append(mapping.Value);
                stringBuilder.AppendLine("),");
            }

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append(Indention);
            stringBuilder.Append(Indention);
            stringBuilder.AppendLine($"_ => (false, default({enumFullName})),");

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append(Indention);
            stringBuilder.AppendLine("};");

            stringBuilder.AppendLine();

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append(Indention);
            stringBuilder.AppendLine("return tryValue;");

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append("}");

            return stringBuilder.ToString();
        }

        return GenerateToEnumException("backing type");
    }

    private static string GenerateToEnumException(
        string source
    ) => // language=cs
        $"""
          => throw new System.Text.Json.JsonException("Enum is not configured to support deserialization from {source}");
         """;

    private static string GenerateToStringFromEnum(
        bool supportsFirstEnumNameSerialization,
        string enumFullName,
        IDictionary<string, string> backingTypeSerialization
    )
    {
        if (supportsFirstEnumNameSerialization)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(" => value switch");
            stringBuilder.Append(MethodPrefix);
            stringBuilder.AppendLine("{");

            foreach (var mapping in backingTypeSerialization)
            {
                stringBuilder.Append(MethodPrefix);
                stringBuilder.Append(Indention);
                stringBuilder.Append(enumFullName);
                stringBuilder.Append(".");
                stringBuilder.Append(mapping.Key);
                stringBuilder.Append(" => \"");
                stringBuilder.Append(mapping.Value);
                stringBuilder.AppendLine("\"u8,");
            }

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append(Indention);
            stringBuilder.Append("_ => ");
            stringBuilder.AppendLine(
                // language=cs
                $$"""
                  throw new System.Text.Json.JsonException($"Undefined mapping of '{value}' from enum '{{enumFullName}}'"),
                  """
            );

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append("};");

            return stringBuilder.ToString();
        }

        return GenerateFromEnumException("enum type");
    }

    private static string GenerateToBackingTypeFromEnum(
        bool supportsBackingTypeSerialization,
        string enumFullName,
        IDictionary<string, object> backingTypeSerialization
    )
    {
        if (supportsBackingTypeSerialization)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(" => value switch");
            stringBuilder.Append(MethodPrefix);
            stringBuilder.AppendLine("{");

            foreach (var mapping in backingTypeSerialization)
            {
                stringBuilder.Append(MethodPrefix);
                stringBuilder.Append(Indention);
                stringBuilder.Append(enumFullName);
                stringBuilder.Append(".");
                stringBuilder.Append(mapping.Key);
                stringBuilder.Append(" => ");
                stringBuilder.Append(mapping.Value);
                stringBuilder.AppendLine(",");
            }

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append(Indention);
            stringBuilder.Append("_ => ");
            stringBuilder.AppendLine(
                // language=cs
                $$"""
                  throw new System.Text.Json.JsonException($"Undefined mapping of '{value}' from enum '{{enumFullName}}'"),
                  """
            );

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append("};");

            return stringBuilder.ToString();
        }

        return GenerateFromEnumException("backing type");
    }

    private static string GenerateWriteFlagsAsArray(
        bool isFlagsEnum,
        bool hasFlagsArraySerialization,
        bool supportsFirstEnumNameSerialization,
        bool supportsBackingTypeSerialization,
        string enumFullName,
        Type backingType,
        IDictionary<string, string> fieldNameSerialization,
        IDictionary<string, object> backingTypeSerialization
    )
    {
        var supportsFlagsArray = isFlagsEnum
            && hasFlagsArraySerialization
            && (supportsBackingTypeSerialization || supportsFirstEnumNameSerialization);

        if (!supportsFlagsArray)
        {
            return " => throw new System.Text.Json.JsonException(\"Enum is not configured to support serialization to flags array\");";
        }

        var writeAsBackingType = supportsBackingTypeSerialization;

        var powerOfTwoMembers = new List<(string MemberName, string FieldName, object ConstantValue)>();
        string? zeroFieldName = null;
        foreach (var mapping in backingTypeSerialization)
        {
            var numericValue = GetFlagUInt64(mapping.Value);

            if (numericValue == 0)
            {
                zeroFieldName = fieldNameSerialization[mapping.Key];
                continue;
            }

            if ((numericValue & (numericValue - 1)) == 0)
            {
                powerOfTwoMembers.Add((mapping.Key, fieldNameSerialization[mapping.Key], mapping.Value));
            }
        }

        var backingTypeFullName = backingType.ToString() ?? throw new ArgumentNullException(nameof(backingType));

        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine();
        stringBuilder.Append(MethodPrefix);
        stringBuilder.AppendLine("{");

        stringBuilder.Append(MethodPrefix);
        stringBuilder.Append(Indention);
        stringBuilder.AppendLine("if (value == default)");

        stringBuilder.Append(MethodPrefix);
        stringBuilder.Append(Indention);
        stringBuilder.AppendLine("{");

        stringBuilder.Append(MethodPrefix);
        stringBuilder.Append(Indention);
        stringBuilder.Append(Indention);
        stringBuilder.AppendLine("writer.WriteStartArray();");

        if (writeAsBackingType)
        {
            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append(Indention);
            stringBuilder.Append(Indention);
            stringBuilder.Append("writer.WriteNumberValue((");
            stringBuilder.Append(backingTypeFullName);
            stringBuilder.Append(")");
            stringBuilder.Append(GetNumericLiteral(0, backingType));
            stringBuilder.AppendLine(");");
        }
        else if (zeroFieldName is not null)
        {
            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append(Indention);
            stringBuilder.Append(Indention);
            stringBuilder.Append("writer.WriteStringValue(\"");
            stringBuilder.Append(zeroFieldName);
            stringBuilder.AppendLine("\"u8);");
        }

        stringBuilder.Append(MethodPrefix);
        stringBuilder.Append(Indention);
        stringBuilder.Append(Indention);
        stringBuilder.AppendLine("writer.WriteEndArray();");

        stringBuilder.Append(MethodPrefix);
        stringBuilder.Append(Indention);
        stringBuilder.Append(Indention);
        stringBuilder.AppendLine("return;");

        stringBuilder.Append(MethodPrefix);
        stringBuilder.Append(Indention);
        stringBuilder.AppendLine("}");

        stringBuilder.Append(MethodPrefix);
        stringBuilder.Append(Indention);
        stringBuilder.AppendLine("writer.WriteStartArray();");

        foreach (var mapping in powerOfTwoMembers)
        {
            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append(Indention);
            stringBuilder.Append("if (value.HasFlag(");
            stringBuilder.Append(enumFullName);
            stringBuilder.Append(".");
            stringBuilder.Append(mapping.MemberName);
            stringBuilder.AppendLine("))");

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append(Indention);
            stringBuilder.AppendLine("{");

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append(Indention);
            stringBuilder.Append(Indention);

            if (writeAsBackingType)
            {
                stringBuilder.Append("writer.WriteNumberValue((");
                stringBuilder.Append(backingTypeFullName);
                stringBuilder.Append(")");
                stringBuilder.Append(GetNumericLiteral(mapping.ConstantValue, backingType));
                stringBuilder.AppendLine(");");
            }
            else
            {
                stringBuilder.Append("writer.WriteStringValue(\"");
                stringBuilder.Append(mapping.FieldName);
                stringBuilder.AppendLine("\"u8);");
            }

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append(Indention);
            stringBuilder.AppendLine("}");
        }

        stringBuilder.Append(MethodPrefix);
        stringBuilder.Append(Indention);
        stringBuilder.AppendLine("writer.WriteEndArray();");

        stringBuilder.Append(MethodPrefix);
        stringBuilder.Append("}");

        return stringBuilder.ToString();
    }

    private static string GetNumericLiteral(object value, Type backingType)
    {
        if (backingType == typeof(sbyte))
        {
            return ((sbyte) value).ToString();
        }

        if (backingType == typeof(byte))
        {
            return ((byte) value).ToString();
        }

        if (backingType == typeof(short))
        {
            return ((short) value).ToString();
        }

        if (backingType == typeof(ushort))
        {
            return ((ushort) value).ToString();
        }

        if (backingType == typeof(int))
        {
            return ((int) value).ToString();
        }

        if (backingType == typeof(uint))
        {
            return ((uint) value).ToString();
        }

        if (backingType == typeof(long))
        {
            return ((long) value).ToString();
        }

        if (backingType == typeof(ulong))
        {
            return ((ulong) value).ToString();
        }

        throw new ArgumentOutOfRangeException(nameof(backingType), backingType, "Unsupported enum backing type");
    }

    private static ulong GetFlagUInt64(object value) => value switch
    {
        sbyte signedValue => unchecked((ulong) signedValue),
        byte byteValue => byteValue,
        short shortValue => unchecked((ulong) shortValue),
        ushort ushortValue => ushortValue,
        int intValue => unchecked((ulong) intValue),
        uint uintValue => uintValue,
        long longValue => unchecked((ulong) longValue),
        ulong ulongValue => ulongValue,
        _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unsupported enum constant type")
    };

    private static string GenerateFromEnumException(
        string source
    ) => // language=cs
        $"""
          => throw new System.Text.Json.JsonException("Enum is not configured to support serialization to {source}");
         """;

    private static TypeCode BackingTypeToTypeCode(
        INamedTypeSymbol namedTypeSymbol
    ) => namedTypeSymbol.SpecialType switch
    {
        SpecialType.System_SByte => TypeCode.SByte,
        SpecialType.System_Byte => TypeCode.Byte,
        SpecialType.System_Int16 => TypeCode.Int16,
        SpecialType.System_UInt16 => TypeCode.UInt16,
        SpecialType.System_Int32 => TypeCode.Int32,
        SpecialType.System_UInt32 => TypeCode.UInt32,
        SpecialType.System_Int64 => TypeCode.Int64,
        SpecialType.System_UInt64 => TypeCode.UInt64,
        _ => throw new ArgumentOutOfRangeException(nameof(namedTypeSymbol.SpecialType), namedTypeSymbol.SpecialType, "Not expected special type"),
    };

    private static string GenerateConverterName(
        ISymbol enumSymbol
    ) => $"{enumSymbol.Name}EnumJsonConverter";
}
