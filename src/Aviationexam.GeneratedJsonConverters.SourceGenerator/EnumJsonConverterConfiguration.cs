using H.Generators.Extensions;
using Microsoft.CodeAnalysis;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator;

internal sealed record EnumJsonConverterConfiguration(
    ITypeSymbol JsonSerializableAttributeTypeArgument
);
