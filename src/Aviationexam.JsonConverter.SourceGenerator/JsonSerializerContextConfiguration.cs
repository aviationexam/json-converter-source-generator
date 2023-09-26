﻿using H.Generators.Extensions;
using Microsoft.CodeAnalysis;

namespace Aviationexam.JsonConverter.SourceGenerator;

public record JsonSerializerContextConfiguration(
    ISymbol JsonSerializerContextClassType,
    INamedTypeSymbol JsonPolymorphicAttributeSymbol,
    INamedTypeSymbol JsonDerivedTypeAttributeSymbol,
    EquatableArray<JsonSerializableConfiguration> JsonSerializableCollection
);
