using H.Generators.Extensions;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using ZLinq;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Generators;

internal static class JsonConvertersSerializerJsonContextGenerator
{
    public static FileWithName Generate(
        EJsonConverterType converterType,
        ISymbol jsonSerializerContextClassType,
        IReadOnlyCollection<JsonConverter> converters
    )
    {
        var classAccessibility = jsonSerializerContextClassType.DeclaredAccessibility switch
        {
            Accessibility.Public => "public",
            Accessibility.Protected => "protected",
            Accessibility.Private => "private",
            Accessibility.Internal => "internal",
            _ => throw new ArgumentOutOfRangeException(
                nameof(jsonSerializerContextClassType.DeclaredAccessibility),
                jsonSerializerContextClassType.DeclaredAccessibility,
                null
            ),
        };

        if (jsonSerializerContextClassType.IsSealed)
        {
            classAccessibility = $"{classAccessibility} sealed";
        }

        var targetNamespace = jsonSerializerContextClassType.ContainingNamespace.IsGlobalNamespace
            ? null
            : jsonSerializerContextClassType.ContainingNamespace.ToDisplayString(JsonPolymorphicConverterIncrementalGenerator.NamespaceFormat);

        return Generate(
            converterType,
            new JsonSerializerContext(
                classAccessibility,
                targetNamespace,
                jsonSerializerContextClassType.Name
            ),
            converters
        );
    }

    public static FileWithName Generate(
        EJsonConverterType converterType,
        JsonSerializerContext jsonSerializerContext,
        IReadOnlyCollection<JsonConverter> converters
    )
    {
        var converterTypeString = converterType.ToString();

        var namespaceLine = string.Empty;
        if (jsonSerializerContext.Namespace is { } jsonConverterNamespace)
        {
            namespaceLine =
                // language=cs
                $"""

                 namespace {jsonConverterNamespace};

                 """;
        }

        var jsonConverters = converters.AsValueEnumerable()
            .Select(x => $"new {x.Namespace}.{x.ClassName}(),")
            .JoinToString("\n        ");

        return new FileWithName(
            $"{jsonSerializerContext.ClassName}.g.cs",
            // language=cs
            $$"""
              #nullable enable
              {{namespaceLine}}
              {{jsonSerializerContext.ClassAccessibility}} partial class {{jsonSerializerContext.ClassName}}
              {
                  public static System.Collections.Generic.IReadOnlyCollection<System.Text.Json.Serialization.JsonConverter> Get{{converterTypeString}}Converters() => new System.Text.Json.Serialization.JsonConverter[]
                  {
                      {{jsonConverters}}
                  };

                  public static void Use{{converterTypeString}}Converters(
                      System.Collections.Generic.ICollection<System.Text.Json.Serialization.JsonConverter> optionsConverters
                  )
                  {
                      foreach (var converter in Get{{converterTypeString}}Converters())
                      {
                          optionsConverters.Add(converter);
                      }
                  }
              }
              """
        );
    }
}
