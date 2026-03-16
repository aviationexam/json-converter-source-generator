using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

internal sealed record JsonSerializableConfiguration(
    CSharpCompilation Compilation,
    ITypeSymbol JsonSerializableAttributeTypeArgument
);
