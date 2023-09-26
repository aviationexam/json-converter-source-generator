using System.Text.Json.Serialization;

namespace Aviationexam.JsonConverter.SourceGenerator.Target;

[JsonSerializable(typeof(BaseContract))]
public partial class MyJsonSerializerContext : JsonSerializerContext
{
}
