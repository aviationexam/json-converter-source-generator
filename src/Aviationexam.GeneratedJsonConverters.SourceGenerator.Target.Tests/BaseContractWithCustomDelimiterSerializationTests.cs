using Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.ContractWithCustomDelimiter;
using System;
using System.Collections.Generic;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Tests;

public partial class BaseContractWithCustomDelimiterSerializationTests
{
    public static IEnumerable<object[]> BaseJsonContractData()
    {
        yield return
        [
            // language=json
            """
            {
                "myCustomDelimiter": "LeafContractWithCustomDelimiter",
                "baseProperty": 1,
                "leafProperty": 2
            }
            """,
            typeof(LeafContractWithCustomDelimiter)
        ];
        yield return
        [
            // language=json
            """
            {
                "baseProperty": 1,
                "myCustomDelimiter": "LeafContractWithCustomDelimiter",
                "leafProperty": 2
            }
            """,
            typeof(LeafContractWithCustomDelimiter)
        ];
        yield return
        [
            // language=json
            """
            {
                "baseProperty": 1,
                "leafProperty": 2,
                "myCustomDelimiter": "LeafContractWithCustomDelimiter"
            }
            """,
            typeof(LeafContractWithCustomDelimiter)
        ];
    }

    public static IEnumerable<object[]> LeafContractData()
    {
        yield return
        [
            new LeafContractWithCustomDelimiter
            {
                BaseProperty = 1,
                LeafProperty = 2
            },
            // language=json
            """
            {
              "myCustomDelimiter": "LeafContractWithCustomDelimiter",
              "leafProperty": 2,
              "baseProperty": 1
            }
            """.Replace("\r\n", Environment.NewLine)
        ];
    }
}
