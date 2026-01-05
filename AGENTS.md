# Agent Guidelines for JSON Converter Source Generator

This repository contains a C# source generator for JSON converters supporting polymorphic contracts and enum serialization.

## Build & Test Commands

### Basic Commands
```bash
# Restore dependencies
dotnet restore --nologo

# Build entire solution (Release)
dotnet build --no-restore --nologo --configuration Release

# Build specific project
dotnet build src/Aviationexam.GeneratedJsonConverters.SourceGenerator/Aviationexam.GeneratedJsonConverters.SourceGenerator.csproj --configuration Release

# Run all tests
dotnet test --no-build --configuration Release --results-directory TestResults --report-trx

# Run specific test project
dotnet test src/Aviationexam.GeneratedJsonConverters.SourceGenerator.Tests/Aviationexam.GeneratedJsonConverters.SourceGenerator.Tests.csproj --configuration Release

# Run single test by fully qualified name
dotnet test --filter "FullyQualifiedName~EnumJsonConverterIncrementalGeneratorSnapshotTests.SimpleWorks" --configuration Release

# Run all tests in a class
dotnet test --filter "FullyQualifiedName~EnumJsonConverterIncrementalGeneratorSnapshotTests" --configuration Release
```

### Code Style & Linting
```bash
# Check code formatting (CI validation)
dotnet format --no-restore --verify-no-changes -v diag

# Apply code formatting
dotnet format --no-restore
```

## Project Structure

- **SourceGenerator**: Core source generator implementation (`netstandard2.0`)
- **SourceGenerator.Tests**: Snapshot tests for generator output (xUnit v3, Microsoft Testing Platform)
- **SourceGenerator.Target**: Sample target project demonstrating usage
- **SourceGenerator.Target.Tests**: Integration tests for generated code

## Code Style Guidelines

### File & Namespace Conventions
- **Line endings**: CRLF (enforced by `.editorconfig`)
- **Encoding**: UTF-8 with BOM for `.cs` files
- **Namespaces**: File-scoped namespaces (C# 14.0 / .NET 10)
- **Namespace structure**: Should match folder structure (`dotnet_style_namespace_match_folder = true`)

### C# Formatting
```csharp
// Indentation: 4 spaces for .cs files
// Max line length: 160 characters
// Space after cast: true
// System directives: NOT sorted first (dotnet_sort_system_directives_first = false)

// Example formatting:
namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

[Generator]
public class EnumJsonConverterIncrementalGenerator : IIncrementalGenerator
{
    public const string Id = "AVI_EJC";
    
    private const EnumSerializationStrategy DefaultEnumSerializationStrategy = 
        EnumSerializationStrategy.FirstEnumName;
}
```

### XML/Project Files
- **Indentation**: 2 spaces for `.csproj`, `.targets`, `.props`
- **Max line length**: 120 characters

### Import Organization
```csharp
// Standard ordering (System namespaces NOT first)
using Aviationexam.GeneratedJsonConverters.SourceGenerator.Extensions;
using H.Generators.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
```

### Naming Conventions
- **Enums**: Prefix with `E` (e.g., `EMyEnum`, `EBackingEnum`)
- **Interfaces**: Prefix with `I` (e.g., `IDiscriminatorStruct`)
- **Constants**: PascalCase with clear descriptors (e.g., `DefaultEnumSerializationStrategy`)
- **Private fields**: Use `_camelCase` for instance fields (not shown in samples but standard C# practice)
- **Method parameters**: camelCase
- **Local variables**: camelCase

### Type Usage
- **Nullable reference types**: ENABLED (`<Nullable>enable</Nullable>`)
- **LangVersion**: 14.0 (C# 14)
- **Target frameworks**: `netstandard2.0` (generator), `net8.0;net9.0;net10.0` (tests)
- Use modern C# features: file-scoped namespaces, target-typed `new()`, collection expressions `[..]`
- Prefer `ImmutableArray` for generator collections
- Use `SymbolEqualityComparer` for Roslyn symbol comparisons

### Comments & Documentation
- XML documentation not required for internal generator code (`GenerateDocumentationFile = false`)
- Use clear, descriptive names over excessive comments
- Comment complex logic (e.g., configuration parsing, symbol transformations)

### Error Handling
- Use nullable patterns and null-coalescing operators
- Throw `ArgumentNullException` for critical null cases in generators
- Use `out` parameters for converter names and diagnostic results
- Leverage `ResultWithDiagnostics<T>` pattern for incremental generators

### Testing Conventions
- **Test framework**: xUnit v3 with Microsoft Testing Platform
- **Snapshot testing**: Use `Verify` library for generator output validation
- **Test naming**: `MethodName_Scenario` or `ScenarioWorks` pattern
- **Theory data**: Use `[InlineData]` for parameterized tests
- Disable specific warnings with `#pragma warning disable/restore` when intentional (e.g., `xUnit1025` for duplicate test values)

### Generator-Specific Patterns
```csharp
// Use IncrementalValueProvider patterns
context.SyntaxProvider.CreateSyntaxProvider(
    predicate: static (node, _) => node is EnumDeclarationSyntax,
    transform: TransformerMethod
)
.Collect()
.Combine(otherProvider)
.SelectAndReportExceptions(GetSourceCode, context, Id)
.SelectAndReportDiagnostics(context)
.AddSource(context);

// Embed resources for generated attributes
i.AddEmbeddedResources<GeneratorClass>([
    "AttributeName",
    "OtherTypeName",
]);
```

### Build Configuration
- **MSBuild properties prefix**: `AVI_EJC_` for this generator
- **Configuration access**: Via `AnalyzerConfigOptionsProvider.GetGlobalOption()`
- Example: `build_property.AVI_EJC_DefaultJsonSerializerContext_Namespace`

## Common Pitfalls
1. **Roslyn symbol comparisons**: Always use `SymbolEqualityComparer.Default.Equals()`, never `==`
2. **Incremental generators**: Ensure all data structures implement proper equality for caching
3. **Embedded resources**: Must be registered in `.csproj` and loaded in `PostInitialization`
4. **netstandard2.0 constraints**: Be mindful of API availability in generator projects
5. **Line endings**: Always CRLF (use `.editorconfig` settings)

## Critical Diagnostic Rules (Error Severity)
- CA1507: Use nameof when possible
- CA2000, CA2012, CA2016: Dispose/async patterns
- CA2213, CA2215, CA2217, CA2234: Dispose and base calls
- CA53xx, CA59xx: Security-related analyzers

## Testing Strategy
- **Snapshot tests**: Verify generated source code exactly matches approved snapshots
- **Integration tests**: Validate serialization/deserialization with actual JSON
- **Configuration tests**: Test various MSBuild property combinations
- Run tests with `--configuration Release` in CI to match production build

## Version Control
- Use GitVersion for semantic versioning
- All changes must pass `dotnet format` validation
- Tests must pass on .NET 8, 9, and 10
