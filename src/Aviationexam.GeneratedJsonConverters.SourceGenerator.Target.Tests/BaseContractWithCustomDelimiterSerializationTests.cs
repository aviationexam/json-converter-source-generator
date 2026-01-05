using Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.ContractWithCustomDelimiter;
using System;
using Xunit;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Tests;

public partial class BaseContractWithCustomDelimiterSerializationTests
{
    public static TheoryData<string, Type> BaseJsonContractData() => new()
    {
        {
            // language=json
            """
            {
                "myCustomDelimiter": "LeafContractWithCustomDelimiter",
                "baseProperty": 1,
                "leafProperty": 2
            }
            """,
            typeof(LeafContractWithCustomDelimiter)
        },
        {
            // language=json
            """
            {
                "baseProperty": 1,
                "myCustomDelimiter": "LeafContractWithCustomDelimiter",
                "leafProperty": 2
            }
            """,
            typeof(LeafContractWithCustomDelimiter)
        },
        {
            // language=json
            """
            {
                "baseProperty": 1,
                "leafProperty": 2,
                "myCustomDelimiter": "LeafContractWithCustomDelimiter"
            }
            """,
            typeof(LeafContractWithCustomDelimiter)
        },
        {
            // language=json
            """
            {
                "baseProperty": 1,
                "leafProperty": "2",
                "myCustomDelimiter": "NullableLeafContractWithCustomDelimiter"
            }
            """,
            typeof(NullableLeafContractWithCustomDelimiter)
        },
        {
            // language=json
            """
            {
                "baseProperty": 1,
                "myCustomDelimiter": "NullableLeafContractWithCustomDelimiter"
            }
            """,
            typeof(NullableLeafContractWithCustomDelimiter)
        }
    };

    public static TheoryData<int, BaseContractWithCustomDelimiter> LeafContractData() => new()
    {
        {
            1,
            new LeafContractWithCustomDelimiter
            {
                BaseProperty = 1,
                LeafProperty = 2
            }
        },
        {
            2,
            new NullableLeafContractWithCustomDelimiter
            {
                BaseProperty = 1,
                LeafProperty = "2",
            }
        },
        {
            3,
            new NullableLeafContractWithCustomDelimiter
            {
                BaseProperty = 1,
                LeafProperty = null,
            }
        }
    };
}
