using Aviationexam.GeneratedJsonConverters.SourceGenerator.Parsers;
using H.Generators;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Generators;

internal static class EnumJsonConverterGenerator
{
    private const string MethodPrefix = "    ";
    private const string Prefix = "    ";

    public static FileWithName? Generate(
        EnumJsonConverterOptions enumJsonConverterOptions,
        string targetNamespace,
        EnumJsonConverterConfiguration enumJsonConverterConfiguration,
        INamedTypeSymbol enumSymbol,
        INamedTypeSymbol enumMemberAttributeSymbol,
        out string converterName
    )
    {
        converterName = GenerateConverterName(enumSymbol);

        var fullName = enumSymbol.ToDisplayString(JsonPolymorphicConverterIncrementalGenerator.NamespaceFormatWithGenericArguments);

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

            if (!backingTypeDeserialization.ContainsKey(constantValue))
            {
                backingTypeDeserialization.Add(constantValue, typeMember.Name);
            }

            if (!backingTypeSerialization.ContainsKey(typeMember.Name))
            {
                backingTypeSerialization.Add(typeMember.Name, constantValue);
            }

            var fieldName = typeMember.Name;
            var enumMember = typeMember.GetAttributes().Where(
                    x => SymbolEqualityComparer.Default.Equals(x.AttributeClass, enumMemberAttributeSymbol)
                )
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

            if (!fieldNameSerialization.ContainsKey(typeMember.Name))
            {
                fieldNameSerialization.Add(typeMember.Name, fieldName);
            }

            /*
            typeForDiscriminatorStringBuilder.Append(prefix);

            var fullTargetType = derivedType.TargetType.ToDisplayString(JsonPolymorphicConverterIncrementalGenerator.NamespaceFormat);

            var discriminator = derivedType.Discriminator;
            if (discriminator is null)
            {
                discriminator = new DiscriminatorStruct<string> { Value = derivedType.TargetType.Name };
            }

            var (typeForDiscriminatorCase, discriminatorForTypeCase) = discriminator switch
            {
                DiscriminatorStruct<string> discriminatorString => (
                    $"Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string> {{ Value: \"{discriminatorString.Value}\" }}",
                    $"    return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<string>(\"{discriminatorString.Value}\");"
                ),
                DiscriminatorStruct<int> discriminatorInt => (
                    $"Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<int> {{ Value: {discriminatorInt.Value} }}",
                    $"    return new Aviationexam.GeneratedJsonConverters.DiscriminatorStruct<int>({discriminatorInt.Value});"
                ),
                _ => throw new ArgumentOutOfRangeException(nameof(discriminator), discriminator, null),
            };

            typeForDiscriminatorStringBuilder.Append(
                $"{typeForDiscriminatorCase} => typeof({fullTargetType}),"
            );

            typeForDiscriminatorStringBuilder.AppendLine();

            discriminatorForTypeStringBuilder.Append(prefix);
            discriminatorForTypeStringBuilder.AppendLine($"if (type == typeof({fullTargetType}))");
            discriminatorForTypeStringBuilder.Append(prefix);
            discriminatorForTypeStringBuilder.AppendLine("{");
            discriminatorForTypeStringBuilder.Append(prefix);
            discriminatorForTypeStringBuilder.AppendLine(discriminatorForTypeCase);
            discriminatorForTypeStringBuilder.Append(prefix);
            discriminatorForTypeStringBuilder.AppendLine("}");
            */
        }

        if (backingType is null)
        {
            return null;
        }

        var serializationStrategy = enumJsonConverterConfiguration.SerializationStrategy;
        if (serializationStrategy == EnumSerializationStrategy.ProjectDefault)
        {
            serializationStrategy = enumJsonConverterOptions.DefaultEnumSerializationStrategy;
        }

        var deserializationStrategies = enumJsonConverterConfiguration.DeserializationStrategies;
        if (deserializationStrategies.IsEmpty)
        {
            deserializationStrategies = enumJsonConverterOptions.DefaultEnumDeserializationStrategies;
        }

        var deserializationStrategy = string.Join(
            " | ",
            deserializationStrategies.Select(
                x => $"Aviationexam.GeneratedJsonConverters.EnumDeserializationStrategy.{x}"
            )
        );

        var toEnumFromString = GenerateToEnumFromString(deserializationStrategies, fullName, fieldNameDeserialization);
        var toEnumFromBackingType = GenerateToEnumFromBackingType(deserializationStrategies, fullName, backingTypeDeserialization);

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

                  protected override Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy SerializationStrategy => Aviationexam.GeneratedJsonConverters.EnumSerializationStrategy.{{serializationStrategy}};

                  protected override T ToEnum(
                      ReadOnlySpan<byte> enumName
                  ){{toEnumFromString}}

                  protected override T ToEnum(
                      TBackingType numericValue
                  ){{toEnumFromBackingType}}
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
        if (enumDeserializationStrategies.Any(x => x == EnumDeserializationStrategy.UseEnumName))
        {
            const string propertyName = "enumName";

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine();
            stringBuilder.Append(MethodPrefix);
            stringBuilder.AppendLine("{");

            foreach (var mapping in backingTypeDeserialization)
            {
                stringBuilder.Append(MethodPrefix);
                stringBuilder.Append(Prefix);
                stringBuilder.Append($"if ({propertyName}.SequenceEqual(\"");
                stringBuilder.Append(mapping.Key);
                stringBuilder.AppendLine("\"u8))");

                stringBuilder.Append(MethodPrefix);
                stringBuilder.Append(Prefix);
                stringBuilder.AppendLine("{");

                stringBuilder.Append(MethodPrefix);
                stringBuilder.Append(Prefix);
                stringBuilder.Append(Prefix);
                stringBuilder.Append("return ");
                stringBuilder.Append(enumFullName);
                stringBuilder.Append(".");
                stringBuilder.Append(mapping.Value);
                stringBuilder.AppendLine(";");

                stringBuilder.Append(MethodPrefix);
                stringBuilder.Append(Prefix);
                stringBuilder.AppendLine("}");
            }

            stringBuilder.AppendLine();
            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append(Prefix);
            stringBuilder.AppendLine(
                // language=cs
                $"""
                var stringValue = System.Text.Encoding.UTF8.GetString({propertyName}.ToArray());
                """
            );
            stringBuilder.AppendLine();

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append(Prefix);
            stringBuilder.AppendLine(
                // language=cs
                $$"""
                throw new System.Text.Json.JsonException($"Undefined mapping of '{stringValue}' to enum '{{enumFullName}}'");
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
        if (enumDeserializationStrategies.Any(x => x == EnumDeserializationStrategy.UseBackingType))
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(" => numericValue switch");
            stringBuilder.Append(MethodPrefix);
            stringBuilder.AppendLine("{");

            foreach (var mapping in backingTypeDeserialization)
            {
                stringBuilder.Append(MethodPrefix);
                stringBuilder.Append(Prefix);
                stringBuilder.Append(mapping.Key);
                stringBuilder.Append(" => ");
                stringBuilder.Append(enumFullName);
                stringBuilder.Append(".");
                stringBuilder.Append(mapping.Value);
                stringBuilder.AppendLine(",");
            }

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append(Prefix);
            stringBuilder.Append("_ => ");
            stringBuilder.AppendLine(
                // language=cs
                $$"""
                  throw new System.Text.Json.JsonException($"Undefined mapping of '{numericValue}' to enum '{{enumFullName}}'"),
                  """
            );

            stringBuilder.Append(MethodPrefix);
            stringBuilder.Append("};");

            return stringBuilder.ToString();
        }

        return GenerateToEnumException("backing type");
    }

    private static string GenerateToEnumException(
        string source
        // language=cs
    ) => $"""
           => throw new System.Text.Json.JsonException("Enum is not configured to support deserialization from {source}");
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
