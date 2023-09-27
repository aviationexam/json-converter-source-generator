using System.Runtime.CompilerServices;
using VerifyTests;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Tests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init() => VerifySourceGenerators.Initialize();
}
