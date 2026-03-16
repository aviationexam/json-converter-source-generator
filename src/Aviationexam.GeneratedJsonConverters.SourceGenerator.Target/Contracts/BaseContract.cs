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

[JsonPolymorphic(TypeDiscriminatorPropertyName = "myCustomDiscriminator")]
[JsonDerivedType<Leaf1Contract<A>>(typeDiscriminator: "Leaf1")]
[JsonDerivedType<Leaf1Contract<B>>(typeDiscriminator: "Leaf1")]
[JsonDerivedType<Leaf2Contract<A>>(typeDiscriminator: "Leaf2")]
[JsonDerivedType<Leaf2Contract<B>>(typeDiscriminator: "Leaf2")]
public abstract class BaseContract<T> where T : class
{
    public required T Value { get; init; }
}
