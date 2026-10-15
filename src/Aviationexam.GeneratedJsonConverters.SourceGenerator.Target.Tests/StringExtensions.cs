#if NETFRAMEWORK
namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Tests;

// string.ReplaceLineEndings is not available on .NET Framework
internal static class StringExtensions
{
    public static string ReplaceLineEndings(this string value, string replacementText) => value
        .Replace("\r\n", "\n")
        .Replace("\r", "\n")
        .Replace("\n", replacementText);
}
#endif
