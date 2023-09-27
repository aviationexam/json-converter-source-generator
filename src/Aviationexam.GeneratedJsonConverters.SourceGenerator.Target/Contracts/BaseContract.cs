using Aviationexam.GeneratedJsonConverters.Attributes;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Contracts;

[JsonPolymorphic]
[JsonDerivedType(typeof(LeafContract), typeDiscriminator: nameof(LeafContract))]
[JsonDerivedType(typeof(AnotherLeafContract), typeDiscriminator: 2)]
public abstract class BaseContract
{
    public int BaseProperty { get; set; }
}
