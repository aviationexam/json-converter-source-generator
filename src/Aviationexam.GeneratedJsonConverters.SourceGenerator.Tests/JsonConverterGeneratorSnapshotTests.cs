using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Tests;

[UsesVerify]
public class JsonPolymorphicConverterGeneratorSnapshotTests
{
    [Fact]
    public Task EmptyWorks() => TestHelper.Verify(string.Empty);

    [Fact]
    public Task SimpleWorks() => TestHelper.Verify(
        // ReSharper disable once HeapView.ObjectAllocation
        """
        using Aviationexam.GeneratedJsonConverters.Attributes;

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
        using Aviationexam.GeneratedJsonConverters.Attributes;

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

    [Fact]
    public Task GenericBaseContractWorks() => TestHelper.Verify(
        // ReSharper disable once HeapView.ObjectAllocation
        """
        using Aviationexam.GeneratedJsonConverters.Attributes;

        namespace ApplicationNamespace.Contracts;

        [JsonPolymorphic(TypeDiscriminatorPropertyName = "myCustomDiscriminator")]
        [JsonDerivedType(typeof(IntLeafContract), typeDiscriminator: nameof(IntLeafContract))]
        [JsonDerivedType(typeof(StringLeafContract), typeDiscriminator: nameof(StringLeafContract))]
        public abstract class BaseContract<T>
        {
        }

        public sealed class IntLeafContract : BaseContract<int>
        {
        }

        public sealed class StringLeafContract : BaseContract<string>
        {
        }
        """,
        """
        using ApplicationNamespace.Contracts;
        using System.Text.Json.Serialization;

        namespace ApplicationNamespace;

        [JsonSerializable(typeof(BaseContract<int>))]
        [JsonSerializable(typeof(BaseContract<int>))] // test that duplicated attribute does not kill generator
        [JsonSerializable(typeof(BaseContract<string>))]
        [JsonSerializable(typeof(IntLeafContract))]
        [JsonSerializable(typeof(StringLeafContract))]
        public partial class MyJsonSerializerContext : JsonSerializerContext
        {
        }
        """
    );
}
