using System.Text.Json.Serialization;

namespace Marco.Forms.UI.Client.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "BlockType")]
[JsonDerivedType(typeof(SectionBlockData), typeDiscriminator: BlockTypes.Section)]
[JsonDerivedType(typeof(ContentBlockData), typeDiscriminator: BlockTypes.ContentEditable)]
[JsonDerivedType(typeof(RootBlock), typeDiscriminator: BlockTypes.Root)]
[JsonDerivedType(typeof(BlockData), typeDiscriminator: BlockTypes.Block)]
[JsonDerivedType(typeof(NavigationBlockData), typeDiscriminator: BlockTypes.Navigation)]
[JsonDerivedType(typeof(ListBlockData), typeDiscriminator: BlockTypes.List)]
[JsonDerivedType(typeof(LinkBlockData), typeDiscriminator: BlockTypes.Link)]
public record BlockData
{
    public required string BlockId { get; set; }
    public required string BlockType { get; set; }
    public int Version { get; set; }
}
