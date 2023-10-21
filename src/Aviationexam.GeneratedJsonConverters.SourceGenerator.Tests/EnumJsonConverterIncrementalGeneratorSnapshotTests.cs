using H.Generators.Tests.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Tests;

[UsesVerify]
public class EnumJsonConverterIncrementalGeneratorSnapshotTests
{
    [Fact]
    public Task EmptyWorks() => TestHelper.Verify<EnumJsonConverterIncrementalGenerator>(string.Empty);


    [Fact]
    public Task SimpleWorks() => TestHelper.Verify<EnumJsonConverterIncrementalGenerator>(
        new DictionaryAnalyzerConfigOptionsProvider(globalOptions: new Dictionary<string, string>
        {
            ["AVI_EJC_DefaultJsonSerializerContext_ClassAccessibility"] = "public",
            ["AVI_EJC_DefaultJsonSerializerContext_Namespace"] = "ApplicationNamespace",
            ["AVI_EJC_DefaultJsonSerializerContext_ClassName"] = "MyJsonSerializerContext",
        }),
        // ReSharper disable once HeapView.ObjectAllocation
        """
        using Aviationexam.GeneratedJsonConverters.Attributes;

        namespace ApplicationNamespace.Contracts;

        [EnumJsonConverter]
        public enum EMyEnum
        {
            A,
            B,
        }

        [DisableEnumJsonConverter]
        public enum EMyIgnoredEnum
        {
            C,
            D,
        }
        """,
        """
        using System.Text.Json.Serialization;

        namespace ApplicationNamespace;

        public partial class MyJsonSerializerContext : JsonSerializerContext
        {
        }
        """
    );
}
