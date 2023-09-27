using Aviationexam.GeneratedJsonConverters.Attributes;

namespace Aviationexam.GeneratedJsonConverters.SourceGenerator.Target.ContractWithCustomDelimiter;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "myCustomDelimiter")]
[JsonDerivedType(typeof(LeafContractWithCustomDelimiter), typeDiscriminator: nameof(LeafContractWithCustomDelimiter))]
public abstract class BaseContractWithCustomDelimiter
{
    public int BaseProperty { get; set; }
}
