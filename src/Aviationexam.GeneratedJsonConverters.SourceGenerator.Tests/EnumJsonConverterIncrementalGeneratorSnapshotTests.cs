using H.Generators.Tests.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Tests;

public class EnumJsonConverterIncrementalGeneratorSnapshotTests
{
    [Fact]
    public Task EmptyWorks() => TestHelper.Verify<EnumJsonConverterIncrementalGenerator>(string.Empty);

    [Fact]
    public Task SimpleWorks() => TestHelper.Verify<EnumJsonConverterIncrementalGenerator>(
        new DictionaryAnalyzerConfigOptionsProvider(globalOptions: new Dictionary<string, string>
        {
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_ClassAccessibility"] = "public",
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_Namespace"] = "ApplicationNamespace",
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_ClassName"] = "MyJsonSerializerContext",
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

    [Fact]
    public Task EnumWithConfigurationWorks() => TestHelper.Verify<EnumJsonConverterIncrementalGenerator>(
        new DictionaryAnalyzerConfigOptionsProvider(globalOptions: new Dictionary<string, string>
        {
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_ClassAccessibility"] = "public",
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_Namespace"] = "ApplicationNamespace",
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_ClassName"] = "MyJsonSerializerContext",
        }),
        // ReSharper disable once HeapView.ObjectAllocation
        """
        using Aviationexam.GeneratedJsonConverters;
        using Aviationexam.GeneratedJsonConverters.Attributes;

        namespace ApplicationNamespace.Contracts;

        [EnumJsonConverter(
            SerializationStrategy = EnumSerializationStrategy.BackingType,
            DeserializationStrategy = EnumDeserializationStrategy.UseBackingType
        )]
        public enum EBackingEnum
        {
            A,
            B,
        }

        [EnumJsonConverter(
            SerializationStrategy = EnumSerializationStrategy.FirstEnumName,
            DeserializationStrategy = EnumDeserializationStrategy.UseEnumName
        )]
        public enum EPropertyEnum : byte
        {
            C,
            D,
        }

        [EnumJsonConverter(
            SerializationStrategy = EnumSerializationStrategy.FirstEnumName,
            DeserializationStrategy = EnumDeserializationStrategy.UseBackingType | EnumDeserializationStrategy.UseEnumName
        )]
        public enum EPropertyWithBackingEnum
        {
            E,
            F,
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

    [Fact]
    public Task EnumWithEnumMemberWorks() => TestHelper.Verify<EnumJsonConverterIncrementalGenerator>(
        new DictionaryAnalyzerConfigOptionsProvider(globalOptions: new Dictionary<string, string>
        {
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_ClassAccessibility"] = "public",
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_Namespace"] = "ApplicationNamespace",
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_ClassName"] = "MyJsonSerializerContext",
        }),
        // ReSharper disable once HeapView.ObjectAllocation
        """
        using Aviationexam.GeneratedJsonConverters;
        using Aviationexam.GeneratedJsonConverters.Attributes;
        using System.Runtime.Serialization;

        namespace ApplicationNamespace.Contracts;

        [EnumJsonConverter(
            SerializationStrategy = EnumSerializationStrategy.FirstEnumName,
            DeserializationStrategy = EnumDeserializationStrategy.UseEnumName
        )]
        public enum EMyEnum
        {
            [EnumMember(Value = "C")]
            A,
            [EnumMember(Value = "D")]
            B,
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

    [Fact]
    public Task EnumWithDuplicatedFieldWorks_FirstEnumName() => TestHelper.Verify<EnumJsonConverterIncrementalGenerator>(
        new DictionaryAnalyzerConfigOptionsProvider(globalOptions: new Dictionary<string, string>
        {
            ["build_property.AVI_EJC_DefaultEnumSerializationStrategy"] = "BackingType",
            ["build_property.AVI_EJC_DefaultEnumDeserializationStrategy"] = "UseBackingType|UseEnumName",
        }),
        // ReSharper disable once HeapView.ObjectAllocation
        """
        using Aviationexam.GeneratedJsonConverters;
        using Aviationexam.GeneratedJsonConverters.Attributes;
        using System.Runtime.Serialization;

        namespace ApplicationNamespace.Contracts;

        [EnumJsonConverter]
        public enum EMyEnum
        {
            [EnumMember(Value = "C")]
            A = 0,
            [EnumMember(Value = "D")]
            B = 1,
            [EnumMember(Value = "E")]
            C = 1,
        }
        """
    );

    [Fact]
    public Task EnumWithDuplicatedFieldWorks_BackingType() => TestHelper.Verify<EnumJsonConverterIncrementalGenerator>(
        new DictionaryAnalyzerConfigOptionsProvider(globalOptions: new Dictionary<string, string>
        {
            ["build_property.AVI_EJC_DefaultEnumSerializationStrategy"] = "BackingType",
            ["build_property.AVI_EJC_DefaultEnumDeserializationStrategy"] = "UseBackingType|UseEnumName",
        }),
        // ReSharper disable once HeapView.ObjectAllocation
        """
        using Aviationexam.GeneratedJsonConverters;
        using Aviationexam.GeneratedJsonConverters.Attributes;
        using System.Runtime.Serialization;

        namespace ApplicationNamespace.Contracts;

        [EnumJsonConverter]
        public enum EMyEnum
        {
            [EnumMember(Value = "C")]
            A = 0,
            [EnumMember(Value = "D")]
            B = 1,
            [EnumMember(Value = "E")]
            C = 1,
        }
        """
    );

    [Fact]
    public Task ProjectConfigurationWorks_UseEnumName() => TestHelper.Verify<EnumJsonConverterIncrementalGenerator>(
        new DictionaryAnalyzerConfigOptionsProvider(globalOptions: new Dictionary<string, string>
        {
            ["build_property.AVI_EJC_DefaultEnumSerializationStrategy"] = "FirstEnumName",
            ["build_property.AVI_EJC_DefaultEnumDeserializationStrategy"] = "UseEnumName",
        }),
        // ReSharper disable once HeapView.ObjectAllocation
        """
        using Aviationexam.GeneratedJsonConverters;
        using Aviationexam.GeneratedJsonConverters.Attributes;
        using System.Runtime.Serialization;

        namespace ApplicationNamespace.Contracts;

        [EnumJsonConverter]
        public enum EMyEnum
        {
            [EnumMember(Value = "C")]
            A,
            [EnumMember(Value = "D")]
            B,
        }
        """
    );

    [Fact]
    public Task ProjectConfigurationWorks_UseBackingType() => TestHelper.Verify<EnumJsonConverterIncrementalGenerator>(
        new DictionaryAnalyzerConfigOptionsProvider(globalOptions: new Dictionary<string, string>
        {
            ["build_property.AVI_EJC_DefaultEnumSerializationStrategy"] = "BackingType",
            ["build_property.AVI_EJC_DefaultEnumDeserializationStrategy"] = "UseBackingType",
        }),
        // ReSharper disable once HeapView.ObjectAllocation
        """
        using Aviationexam.GeneratedJsonConverters;
        using Aviationexam.GeneratedJsonConverters.Attributes;
        using System.Runtime.Serialization;

        namespace ApplicationNamespace.Contracts;

        [EnumJsonConverter]
        public enum EMyEnum
        {
            [EnumMember(Value = "C")]
            A,
            [EnumMember(Value = "D")]
            B,
        }
        """
    );

    [Fact]
    public Task ProjectConfigurationWorks_UseBackingType_UseEnumName() => TestHelper.Verify<EnumJsonConverterIncrementalGenerator>(
        new DictionaryAnalyzerConfigOptionsProvider(globalOptions: new Dictionary<string, string>
        {
            ["build_property.AVI_EJC_DefaultEnumSerializationStrategy"] = "BackingType",
            ["build_property.AVI_EJC_DefaultEnumDeserializationStrategy"] = "UseBackingType|UseEnumName",
        }),
        // ReSharper disable once HeapView.ObjectAllocation
        """
        using Aviationexam.GeneratedJsonConverters;
        using Aviationexam.GeneratedJsonConverters.Attributes;
        using System.Runtime.Serialization;

        namespace ApplicationNamespace.Contracts;

        [EnumJsonConverter]
        public enum EMyEnum
        {
            [EnumMember(Value = "C")]
            A,
            [EnumMember(Value = "D")]
            B,
        }
        """
    );

    [Fact]
    public Task NotAnnotatedEnumReportingWorks() => TestHelper.Verify<EnumJsonConverterIncrementalGenerator>(
        // ReSharper disable once HeapView.ObjectAllocation
        """
        using Aviationexam.GeneratedJsonConverters;

        namespace ApplicationNamespace.Contracts;

        public enum EMyEnum
        {
            A,
            B,
        }
        """
    );

    [Fact]
    public Task ProjectConfigurationWorks_FlagsArray() => TestHelper.Verify<EnumJsonConverterIncrementalGenerator>(
        new DictionaryAnalyzerConfigOptionsProvider(globalOptions: new Dictionary<string, string>
        {
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_ClassAccessibility"] = "public",
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_Namespace"] = "ApplicationNamespace",
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_ClassName"] = "MyJsonSerializerContext",
            ["build_property.AVI_EJC_DefaultEnumSerializationStrategy"] = "FirstEnumName|FlagsArray",
            ["build_property.AVI_EJC_DefaultEnumDeserializationStrategy"] = "UseEnumName",
        }),
        // language=cs
        """
        using Aviationexam.GeneratedJsonConverters.Attributes;

        namespace ApplicationNamespace.Contracts;

        [System.Flags]
        [EnumJsonConverter]
        public enum EFlagsEnum
        {
            None = 0,
            Read = 1 << 0,
            Write = 1 << 1,
            Execute = 1 << 2,
            ReadWrite = Read | Write,
        }

        [EnumJsonConverter]
        public enum ENonFlagsEnum
        {
            A,
            B,
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

    [Fact]
    public Task ProjectConfigurationWorks_FlagsArrayOnly() => TestHelper.Verify<EnumJsonConverterIncrementalGenerator>(
        new DictionaryAnalyzerConfigOptionsProvider(globalOptions: new Dictionary<string, string>
        {
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_ClassAccessibility"] = "public",
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_Namespace"] = "ApplicationNamespace",
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_ClassName"] = "MyJsonSerializerContext",
            ["build_property.AVI_EJC_DefaultEnumSerializationStrategy"] = "FlagsArray",
            ["build_property.AVI_EJC_DefaultEnumDeserializationStrategy"] = "UseEnumName",
        }),
        // language=cs
        """
        using Aviationexam.GeneratedJsonConverters.Attributes;

        namespace ApplicationNamespace.Contracts;

        [System.Flags]
        [EnumJsonConverter]
        public enum EFlagsEnum
        {
            None = 0,
            Read = 1 << 0,
            Write = 1 << 1,
            Execute = 1 << 2,
            ReadWrite = Read | Write,
        }

        [EnumJsonConverter]
        public enum ENonFlagsEnum
        {
            A,
            B,
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

    [Fact]
    public Task FlagsEnumWithFirstEnumNameWorks() => TestHelper.Verify<EnumJsonConverterIncrementalGenerator>(
        new DictionaryAnalyzerConfigOptionsProvider(globalOptions: new Dictionary<string, string>
        {
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_ClassAccessibility"] = "public",
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_Namespace"] = "ApplicationNamespace",
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_ClassName"] = "MyJsonSerializerContext",
        }),
        // language=cs
        """
        using Aviationexam.GeneratedJsonConverters.Attributes;

        namespace ApplicationNamespace.Contracts;

        [System.Flags]
        [EnumJsonConverter(
            SerializationStrategy = EnumSerializationStrategy.FirstEnumName | EnumSerializationStrategy.FlagsArray,
            DeserializationStrategy = EnumDeserializationStrategy.UseEnumName
        )]
        public enum EFlagsEnum
        {
            None = 0,
            Read = 1 << 0,
            Write = 1 << 1,
            Execute = 1 << 2,
            ReadWrite = Read | Write,
        }
        """
    );

    [Fact]
    public Task FlagsEnumWithBackingTypeWorks() => TestHelper.Verify<EnumJsonConverterIncrementalGenerator>(
        new DictionaryAnalyzerConfigOptionsProvider(globalOptions: new Dictionary<string, string>
        {
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_ClassAccessibility"] = "public",
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_Namespace"] = "ApplicationNamespace",
            ["build_property.AVI_EJC_DefaultJsonSerializerContext_ClassName"] = "MyJsonSerializerContext",
        }),
        // language=cs
        """
        using Aviationexam.GeneratedJsonConverters.Attributes;

        namespace ApplicationNamespace.Contracts;

        [System.Flags]
        [EnumJsonConverter(
            SerializationStrategy = EnumSerializationStrategy.BackingType | EnumSerializationStrategy.FlagsArray,
            DeserializationStrategy = EnumDeserializationStrategy.UseBackingType
        )]
        public enum EFlagsBackingEnum
        {
            None = 0,
            Read = 1 << 0,
            Write = 1 << 1,
            Execute = 1 << 2,
            ReadWrite = Read | Write,
        }
        """
    );
}
