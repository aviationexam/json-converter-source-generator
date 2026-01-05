using System.Threading.Tasks;
using Xunit;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Tests;

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
    public Task SimpleRecordWorks() => TestHelper.Verify<JsonPolymorphicConverterIncrementalGenerator>(
        // ReSharper disable once HeapView.ObjectAllocation
        """
        using Aviationexam.GeneratedJsonConverters.Attributes;

        namespace ApplicationNamespace.Contracts;

        [JsonPolymorphic]
        [JsonDerivedType(typeof(LeafContract), typeDiscriminator: nameof(LeafContract))]
        [JsonDerivedType(typeof(AnotherLeafContract), typeDiscriminator: 2)]
        [JsonDerivedType(typeof(AnonymousLeafContract))]
        public abstract record BaseContract
        {
        }

        public sealed record LeafContract : BaseContract
        {
        }

        public sealed record AnotherLeafContract : BaseContract
        {
        }

        public sealed record AnonymousLeafContract : BaseContract
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

    [Theory]
    [InlineData(
        "1",
        """
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<FirstLevel1>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<FirstLevel2>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<SecondLevel1A>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<SecondLevel1B>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<SecondLevel2A>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<SecondLevel2B>]
        """
    )]
    [InlineData(
        "2",
        """
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<SecondLevel1A>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<SecondLevel1B>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<FirstLevel1>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<FirstLevel2>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<SecondLevel2A>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<SecondLevel2B>]
        """
    )]
    [InlineData(
        "3",
        """
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<SecondLevel1A>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<SecondLevel1B>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<SecondLevel2B>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<SecondLevel2A>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<FirstLevel1>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<FirstLevel2>]
        """
    )]
    [InlineData(
        "4",
        """
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<SecondLevel1B>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<SecondLevel1A>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<SecondLevel2A>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<SecondLevel2B>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<FirstLevel1>]
        [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<FirstLevel2>]
        """
    )]
    public Task RichInheritanceWorks(
        string snapshotName, string attributes
    ) => TestHelper.ParametrizedVerify<JsonPolymorphicConverterIncrementalGenerator>(
        snapshotName,
        // ReSharper disable once HeapView.ObjectAllocation
        $$"""
          using System.Text.Json.Serialization;

          [Aviationexam.GeneratedJsonConverters.Attributes.JsonPolymorphic]
          {{attributes}}
          public abstract record BaseType(string Name);

          public record FirstLevel1(string Name) : BaseType(Name);

          public record FirstLevel2(string Name) : BaseType(Name);

          public sealed record SecondLevel1A(string Name) : FirstLevel1(Name);
          public sealed record SecondLevel1B(string Name) : FirstLevel1(Name);

          public sealed record SecondLevel2A(string Name) : FirstLevel2(Name);
          public sealed record SecondLevel2B(string Name) : FirstLevel2(Name);

          [JsonSourceGenerationOptions(
              WriteIndented = true,
              PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
              GenerationMode = JsonSourceGenerationMode.Default
          )]
          [JsonSerializable(typeof(BaseType))]
          [JsonSerializable(typeof(FirstLevel1))]
          [JsonSerializable(typeof(FirstLevel2))]
          [JsonSerializable(typeof(SecondLevel1A))]
          [JsonSerializable(typeof(SecondLevel1B))]
          [JsonSerializable(typeof(SecondLevel2A))]
          [JsonSerializable(typeof(SecondLevel2B))]
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

    [Fact]
    public Task GenericLeafsWorks(
    ) => TestHelper.Verify<JsonPolymorphicConverterIncrementalGenerator>(
        // ReSharper disable once HeapView.ObjectAllocation
        """
          using System.Text.Json.Serialization;

          [Aviationexam.GeneratedJsonConverters.Attributes.JsonPolymorphic]
          [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<LeafA<int>>("int_LeafA")]
          [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<LeafB<int>>("int_LeafB")]
          [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<LeafA<string>>("string_LeafA")]
          [Aviationexam.GeneratedJsonConverters.Attributes.JsonDerivedType<LeafB<string>>("string_LeafB")]
          public abstract record BaseType<T>(string Name);

          public sealed record LeafA<T>(string Name) : BaseType<T>(Name);

          public sealed record LeafB<T>(string Name) : BaseType<T>(Name);

          [JsonSourceGenerationOptions(
              WriteIndented = true,
              PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
              GenerationMode = JsonSourceGenerationMode.Default
          )]
          [JsonSerializable(typeof(BaseType))]
          [JsonSerializable(typeof(LeafA<int>))]
          [JsonSerializable(typeof(LeafB<int>))]
          [JsonSerializable(typeof(LeafA<string>))]
          [JsonSerializable(typeof(LeafB<string>))]
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

    [Fact]
    public Task MultipleContextsWorks(
    ) => TestHelper.Verify<JsonPolymorphicConverterIncrementalGenerator>(
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

          [JsonSourceGenerationOptions(
              WriteIndented = true,
              PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
              GenerationMode = JsonSourceGenerationMode.Default
          )]
          [JsonSerializable(typeof(Person))]
          [JsonSerializable(typeof(Student))]
          [JsonSerializable(typeof(Teacher))]
          public partial class SecondProjectJsonSerializerContext : JsonSerializerContext
          {
              static SecondProjectJsonSerializerContext()
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
