using Aviationexam.JsonConverter.Attributes;

namespace Aviationexam.JsonConverter.SourceGenerator.Target.ContractWithCustomDelimiter;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "myCustomDelimiter")]
[JsonDerivedType(typeof(LeafContractWithCustomDelimiter), typeDiscriminator: nameof(LeafContractWithCustomDelimiter))]
public abstract class BaseContractWithCustomDelimiter
{
    public int BaseProperty { get; set; }
}
