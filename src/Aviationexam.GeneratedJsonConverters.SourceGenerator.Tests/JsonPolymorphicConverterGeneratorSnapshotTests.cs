using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Tests;

[UsesVerify]
public class JsonPolymorphicConverterGeneratorSnapshotTests
{
    [Fact]
    public Task EmptyWorks() => TestHelper.Verify<JsonPolymorphicConverterIncrementalGenerator>(string.Empty);

    [Fact]
    public Task SimpleWorks() => TestHelper.Verify<JsonPolymorphicConverterIncrementalGenerator>(
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
    public Task CustomDiscriminatorWorks() => TestHelper.Verify<JsonPolymorphicConverterIncrementalGenerator>(
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
    public Task GenericBaseContractWorks() => TestHelper.Verify<JsonPolymorphicConverterIncrementalGenerator>(
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

    [Fact]
    public Task GenericAttributeWorks() => TestHelper.Verify<JsonPolymorphicConverterIncrementalGenerator>(
        // ReSharper disable once HeapView.ObjectAllocation
        """
        using Aviationexam.GeneratedJsonConverters.Attributes;

        namespace ApplicationNamespace.Contracts;

        [JsonPolymorphic(TypeDiscriminatorPropertyName = "myCustomDiscriminator")]
        [JsonDerivedType<IntLeafContract>(typeDiscriminator: nameof(IntLeafContract))]
        [JsonDerivedType<StringLeafContract>(typeDiscriminator: nameof(StringLeafContract))]
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

    [Fact]
    public Task TopLevelStatementWorks() => TestHelper.Verify<JsonPolymorphicConverterIncrementalGenerator>(
        // ReSharper disable once HeapView.ObjectAllocation
        """
        using System.Text.Json.Serialization;

        [Aviationexam.GeneratedJsonConverters.Attributes.JsonPolymorphic]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<Student>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<Teacher>]
        public abstract record Person(string Name);

        public sealed record Student(string Name) : Person(Name);

        public sealed record Teacher(string Name) : Person(Name);

        [JsonSourceGenerationOptions(
            WriteIndented = true,
            PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
            GenerationMode = JsonSourceGenerationMode.Default
        )]
        [JsonSerializable(typeof(Person))]
        [JsonSerializable(typeof(Student))]
        [JsonSerializable(typeof(Teacher))]
        public partial class ProjectJsonSerializerContext : JsonSerializerContext
        {
            static ProjectJsonSerializerContext()
            {
                foreach (var converter in GetPolymorphicConverters())
                {
                    s_defaultOptions.Converters.Add(converter);
                }
            }
        }
        """
    );
}
