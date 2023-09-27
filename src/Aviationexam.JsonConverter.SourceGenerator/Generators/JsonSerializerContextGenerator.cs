using H.Generators;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aviationexam.JsonConverter.SourceGenerator.Generators;

public static class JsonSerializerContextGenerator
{
    public static FileWithName Generate(
        ISymbol jsonSerializerContextClassType,
        IReadOnlyCollection<string> converters
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

        var targetNamespace = jsonSerializerContextClassType.ContainingNamespace.ToDisplayString(JsonConverterGenerator.NamespaceFormat);

        return new FileWithName(
            $"{jsonSerializerContextClassType.Name}.g.cs",
            // language=cs
            $$"""
              #nullable enable
              using System.Collections.Generic;

              namespace {{targetNamespace}};

              {{classAccessibility}} partial class {{jsonSerializerContextClassType.Name}}
              {
                  public static IReadOnlyCollection<System.Text.Json.Serialization.JsonConverter> GetPolymorphicConverters() => new System.Text.Json.Serialization.JsonConverter[]
                  {
                      {{string.Join("\n        ", converters.Select(x => $"new {x}(),"))}}
                  };

                  public static void UsePolymorphicConverters(
                      ICollection<System.Text.Json.Serialization.JsonConverter> optionsConverters
                  )
                  {
                      foreach (var converter in GetPolymorphicConverters())
                      {
                          optionsConverters.Add(converter);
                      }
                  }
              }
              """
        );
    }
}
