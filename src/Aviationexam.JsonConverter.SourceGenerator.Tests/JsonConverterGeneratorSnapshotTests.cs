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
        // ReSharper disable once HeapView.ObjectAllocation
        """
        using Aviationexam.JsonConverter.Attributes;

        namespace ApplicationNamespace.Contracts;

        [JsonPolymorphic]
        [JsonDerivedType(typeof(LeafContract), typeDiscriminator: nameof(LeafContract))]
        [JsonDerivedType(typeof(AnotherLeafContract), typeDiscriminator: 2)]
        [JsonDerivedType(typeof(AnonymousLeafContract))]
        public abstract class BaseContract
        {
        }

        public sealed class LeafContract : BaseContract
        {
        }

        public sealed class AnotherLeafContract : BaseContract
        {
        }

        public sealed class AnonymousLeafContract : BaseContract
        {
        }
        """,
        """
        using ApplicationNamespace.Contracts;
        using System.Text.Json.Serialization;

        namespace ApplicationNamespace;

        [JsonSerializable(typeof(BaseContract))]
        [JsonSerializable(typeof(LeafContract))]
        [JsonSerializable(typeof(AnotherLeafContract))]
        [JsonSerializable(typeof(AnonymousLeafContract))]
        public partial class MyJsonSerializerContext : JsonSerializerContext
        {
        }
        """
    );

    [Fact]
    public Task CustomDiscriminatorWorks() => TestHelper.Verify(
        // ReSharper disable once HeapView.ObjectAllocation
        """
        using Aviationexam.JsonConverter.Attributes;

        namespace ApplicationNamespace.Contracts;

        [JsonPolymorphic(TypeDiscriminatorPropertyName = "myCustomDiscriminator")]
        [JsonDerivedType(typeof(LeafContract), typeDiscriminator: nameof(LeafContract))]
        public abstract class BaseContract
        {
        }

        public sealed class LeafContract : BaseContract
        {
        }
        """,
        """
        using ApplicationNamespace.Contracts;
        using System.Text.Json.Serialization;

        namespace ApplicationNamespace;

        [JsonSerializable(typeof(BaseContract))]
        [JsonSerializable(typeof(LeafContract))]
        public partial class MyJsonSerializerContext : JsonSerializerContext
        {
        }
        """
    );
}
