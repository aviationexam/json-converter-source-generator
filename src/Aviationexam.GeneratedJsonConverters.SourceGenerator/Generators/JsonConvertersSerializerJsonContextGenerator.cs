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
        string convertersTargetNamespace,
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

        var targetNamespace = jsonSerializerContextClassType.ContainingNamespace.ToDisplayString(JsonPolymorphicConverterIncrementalGenerator.NamespaceFormat);
        var converterTypeString = converterType.ToString();

        return new FileWithName(
            $"{jsonSerializerContextClassType.Name}.g.cs",
            // language=cs
            $$"""
              #nullable enable

              namespace {{targetNamespace}};

              {{classAccessibility}} partial class {{jsonSerializerContextClassType.Name}}
              {
                  public static System.Collections.Generic.IReadOnlyCollection<System.Text.Json.Serialization.JsonConverter> Get{{converterTypeString}}Converters() => new System.Text.Json.Serialization.JsonConverter[]
                  {
                      {{string.Join("\n        ", converters.Select(x => $"new {convertersTargetNamespace}.{x}(),"))}}
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
