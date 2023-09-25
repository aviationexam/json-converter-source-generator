using System.Runtime.CompilerServices;
using VerifyTests;

namespace Aviationexam.JsonConverter.SourceGenerator.Tests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init() => VerifySourceGenerators.Initialize();
}
