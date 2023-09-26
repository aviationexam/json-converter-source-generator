using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace Aviationexam.JsonConverter.SourceGenerator.Tests;

[UsesVerify]
public class JsonConverterGeneratorSnapshotTests
{
    [Fact]
    public Task EmptyWorks() => TestHelper.Verify(string.Empty);

    [Fact]
    public Task SimpleWorks() => TestHelper.Verify(
        """
        using System.Text.Json.Serialization;

        namespace ApplicationNamespace;

        [JsonPolymorphic]
        [JsonDerivedType(typeof(LeafContract), typeDiscriminator: nameof(LeafContract))]
        public abstract class BaseContract
        {
        }

        public abstract class LeafContract : BaseContract
        {
        }

        [JsonSerializable(typeof(BaseContract))]
        public partial class MyJsonSerializerContext : JsonSerializerContext
        {
        }
        """
    );
}
