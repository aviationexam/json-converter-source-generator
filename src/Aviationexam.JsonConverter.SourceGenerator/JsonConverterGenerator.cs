using Aviationexam.JsonConverter.SourceGenerator.Filters;
using Aviationexam.JsonConverter.SourceGenerator.Generators;
using Aviationexam.JsonConverter.SourceGenerator.Parsers;
using Aviationexam.JsonConverter.SourceGenerator.Transformers;
using H.Generators;
using H.Generators.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;

namespace Aviationexam.JsonConverter.SourceGenerator;

[Generator]
public class JsonConverterGenerator : IIncrementalGenerator
{
    public const string Id = "AVI_JC";

    internal static readonly SymbolDisplayFormat NamespaceFormat = new(
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces
    );

    public void Initialize(
        IncrementalGeneratorInitializationContext context
    )
    {
        context.RegisterPostInitializationOutput(i =>
        {
            var type = GetType();
            var generatorNamespace = type.Namespace!;

            var manifestResourceNames = type.Assembly.GetManifestResourceNames();

            foreach (var manifestResourceName in manifestResourceNames)
            {
                var fileName = manifestResourceName[(generatorNamespace.Length + 1)..^3];

                var manifestResourceInfo = type.Assembly.GetManifestResourceStream(manifestResourceName);

                if (manifestResourceInfo is null)
                {
                    continue;
                }

                i.AddSource(
                    $"{fileName}.g.cs",
                    SourceText.From(manifestResourceInfo, Encoding.UTF8, canBeEmbedded: true)
                );
            }

            i.AddSource(
                "JsonPolymorphicAttribute.g.cs",
                """
                #nullable enable

                /// <summary>
                /// This is a copy of System.Text.Json.Serialization.JsonPolymorphicAttribute.
                /// It's purpose is to hijack this attribute to silence System.Text.Json.Serialization.Metadata.PolymorphicTypeResolver{ThrowHelper.ThrowNotSupportedException_BaseConverterDoesNotSupportMetadata}
                ///
                /// When placed on a type, indicates that the type should be serialized polymorphically.
                /// </summary>
                [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
                internal sealed class JsonPolymorphicAttribute : System.Text.Json.Serialization.JsonAttribute
                {
                    /// <summary>
                    /// Gets or sets a custom type discriminator property name for the polymorhic type.
                    /// Uses the default '$type' property name if left unset.
                    /// </summary>
                    public string? TypeDiscriminatorPropertyName { get; set; }

                    /// <summary>
                    /// Gets or sets the behavior when serializing an undeclared derived runtime type.
                    /// </summary>
                    public System.Text.Json.Serialization.JsonUnknownDerivedTypeHandling UnknownDerivedTypeHandling { get; set; }

                    /// <summary>
                    /// When set to <see langword="true"/>, instructs the deserializer to ignore any
                    /// unrecognized type discriminator id's and reverts to the contract of the base type.
                    /// Otherwise, it will fail the deserialization.
                    /// </summary>
                    public bool IgnoreUnrecognizedTypeDiscriminators { get; set; }
                }
                """
            );
        });

        context.SyntaxProvider.CreateSyntaxProvider(
                predicate: static (node, _) => node is ClassDeclarationSyntax { AttributeLists.Count: > 0 },
                transform: JsonSerializerContextTransformer.GetJsonSerializerContextClassDeclarationSyntax
            )
            .Where(x => !x.Result.JsonSerializableCollection.IsEmpty)
            .Select(JsonSerializerContextConfigurationFilter.FilterJsonSerializerContextConfiguration)
            .SelectAndReportExceptions(GetSourceCode, context, Id)
            .SelectAndReportDiagnostics(context)
            .AddSource(context);
    }

    private static ResultWithDiagnostics<EquatableArray<FileWithName>> GetSourceCode(
        ResultWithDiagnostics<JsonSerializerContextConfiguration> resultObject,
        CancellationToken cancellationToken
    )
    {
        var context = resultObject.Result;

        if (context.JsonSerializableCollection.IsEmpty)
        {
            return new EquatableArray<FileWithName>().ToResultWithDiagnostics(resultObject.Diagnostics);
        }

        var diagnostics = ImmutableArray<Diagnostic>.Empty;

        diagnostics = resultObject.Diagnostics.Concat(diagnostics).ToImmutableArray();

        var files = new List<FileWithName>();
        var converters = new List<string>();

        foreach (var jsonSerializableConfiguration in context.JsonSerializableCollection)
        {
            var attributes = jsonSerializableConfiguration.JsonSerializableAttributeTypeArgument.GetAttributes();

            JsonPolymorphicConfiguration? jsonPolymorphicConfiguration = null;
            var derivedTypes = new List<JsonDerivedTypeConfiguration>();
            foreach (var attribute in attributes)
            {
                if (SymbolEqualityComparer.Default.Equals(attribute.AttributeClass, context.JsonPolymorphicAttributeSymbol))
                {
                    jsonPolymorphicConfiguration = JsonPolymorphicAttributeParser.Parse(attribute);
                }

                if (SymbolEqualityComparer.Default.Equals(attribute.AttributeClass, context.JsonDerivedTypeAttributeSymbol))
                {
                    var jsonDerivedTypeConfiguration = JsonDerivedTypeAttributeParser.Parse(attribute);

                    if (jsonDerivedTypeConfiguration is not null)
                    {
                        derivedTypes.Add(jsonDerivedTypeConfiguration);
                    }
                }
            }

            files.Add(JsonPolymorphicConverterGenerator.Generate(
                jsonSerializableConfiguration,
                jsonPolymorphicConfiguration,
                derivedTypes,
                out var converterName
            ));

            converters.Add(converterName);
        }

        if (converters.Any())
        {
            files.Add(JsonSerializerContextGenerator.Generate(
                context.JsonSerializerContextClassType,
                converters
            ));
        }

        return files.ToImmutableArray().AsEquatableArray().ToResultWithDiagnostics(diagnostics);
    }
}
