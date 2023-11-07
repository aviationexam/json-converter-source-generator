using Aviationexam.GeneratedJsonConverters.Attributes;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.Contracts;

[JsonPolymorphic]
[JsonDerivedType(typeof(LeafContract), typeDiscriminator: nameof(LeafContract))]
[JsonDerivedType(typeof(AnotherLeafContract), typeDiscriminator: 2)]
[JsonDerivedType<GenericLeafContract>(typeDiscriminator: nameof(GenericLeafContract))]
public abstract class BaseContract
{
    public int BaseProperty { get; set; }
}
