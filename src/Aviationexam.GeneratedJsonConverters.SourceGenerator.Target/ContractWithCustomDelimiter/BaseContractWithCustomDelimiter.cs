using Aviationexam.GeneratedJsonConverters.Attributes;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.ContractWithCustomDelimiter;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "myCustomDelimiter")]
[JsonDerivedType(typeof(LeafContractWithCustomDelimiter), typeDiscriminator: nameof(LeafContractWithCustomDelimiter))]
[JsonDerivedType(typeof(NullableLeafContractWithCustomDelimiter), typeDiscriminator: nameof(NullableLeafContractWithCustomDelimiter))]
public abstract class BaseContractWithCustomDelimiter
{
    public int BaseProperty { get; set; }
}
