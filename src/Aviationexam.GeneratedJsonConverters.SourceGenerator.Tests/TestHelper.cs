using H.Generators.Tests.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using VerifyXunit;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Tests;

public static class TestHelper
{
    public static Task Verify<TIncrementalGenerator>(
        [StringSyntax("csharp")] params string[] source
    ) where TIncrementalGenerator : class, IIncrementalGenerator, new() => Verify<TIncrementalGenerator>(
        new DictionaryAnalyzerConfigOptionsProvider(),
        source
    );

    public static Task Verify<TIncrementalGenerator>(
        DictionaryAnalyzerConfigOptionsProvider analyzerConfigOptionsProvider,
        [StringSyntax("csharp")] params string[] source
    ) where TIncrementalGenerator : class, IIncrementalGenerator, new()
    {
        // Parse the provided string into a C# syntax tree
        var syntaxTrees = source.Select(x => CSharpSyntaxTree.ParseText(x)).ToArray();

        // Create a Roslyn compilation for the syntax tree.
        var compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: syntaxTrees,
            references: new[]
                {
                    typeof(object).Assembly.Location,
                }
                .Union(GetLocationWithDependencies(typeof(JsonSerializer)))
                .Distinct()
                .Select(x => MetadataReference.CreateFromFile(x))
                .ToArray()
        );

        // Create an instance of our EnumGenerator incremental source generator
        var generator = new TIncrementalGenerator();

        // The GeneratorDriver is used to run our generator against a compilation
        GeneratorDriver driver = CSharpGeneratorDriver.Create(
            new[] { generator.AsSourceGenerator() },
            optionsProvider: analyzerConfigOptionsProvider
        );

        // Run the source generator!
        driver = driver.RunGenerators(compilation);

        // Use verify to snapshot test the source generator output!
        return Verifier.Verify(driver);
    }


    private static IEnumerable<string> GetLocationWithDependencies(Type type) => GetLocationWithDependencies(type.Assembly);

    private static IEnumerable<string> GetLocationWithDependencies(Assembly assembly)
    {
        foreach (var referencedAssembly in assembly.GetReferencedAssemblies())
        {
            foreach (var path in GetLocationWithDependencies(Assembly.Load(referencedAssembly)))
            {
                yield return path;
            }
        }

        yield return assembly.Location;
    }
}
