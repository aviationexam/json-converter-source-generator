using H.Generators;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

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
            ? JsonPolymorphicConverterIncrementalGenerator.EmptyPolymorphicNamespace
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

        return new FileWithName(
            $"{jsonSerializerContext.ClassName}.g.cs",
            // language=cs
            $$"""
              #nullable enable

              namespace {{jsonSerializerContext.Namespace}};

              {{jsonSerializerContext.ClassAccessibility}} partial class {{jsonSerializerContext.ClassName}}
              {
                  public static System.Collections.Generic.IReadOnlyCollection<System.Text.Json.Serialization.JsonConverter> Get{{converterTypeString}}Converters() => new System.Text.Json.Serialization.JsonConverter[]
                  {
                      {{string.Join("\n        ", converters.Select(x => $"new {x.Namespace}.{x.ClassName}(),"))}}
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
