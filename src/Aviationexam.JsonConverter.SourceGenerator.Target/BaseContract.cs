using Aviationexam.JsonConverter.Attributes;

namespace Aviationexam.JsonConverter.SourceGenerator.Target;

[JsonPolymorphic]
[JsonDerivedType(typeof(LeafContract), typeDiscriminator: nameof(LeafContract))]
[JsonDerivedType(typeof(AnotherLeafContract), typeDiscriminator: 2)]
public abstract class BaseContract
{
    public int BaseProperty { get; set; }
}
