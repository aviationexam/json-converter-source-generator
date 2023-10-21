using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Tests;

[UsesVerify]
public class EnumJsonConverterIncrementalGeneratorSnapshotTests
{
    [Fact]
    public Task EmptyWorks() => TestHelper.Verify<EnumJsonConverterIncrementalGenerator>(string.Empty);
}
