using System.Text.Json.Serialization;
using Marco.Forms.UI.Client.Models;

namespace Marco.Forms.UI.Client.Components;

public record PageBlock
{
    public required string BlockId { get; init; }
    public string? PageId { get; init; }

    /// <summary>
    ///     The type of block, e.g. root, rich text, header, image, etc.
    /// </summary>
    public required string BlockType { get; init; }

    /// <summary>
    ///     The position of the block in the page
    /// </summary>
    public double BlockPosition { get; set; }

    /// <summary>
    ///     JSON serialized block data
    /// </summary>
    public BlockData? BlockData { get; set; }

    public int BlockDataVersion { get; set; }

    public HashSet<string> ChildBlockIds { get; set; } = [];

    public HashSet<string> ParentBlockIds { get; set; } = [];
    public string? Tag { get; set; }

    /// <summary>
    ///     The formatting & styles of the block.
    /// </summary>
    public BlockFormatting? Formatting { get; set; }

    /// <summary>
    ///     The layout of the block.
    /// </summary>
    public BlockLayout? Layout { get; set; }

    /// <summary>
    ///     The label of the block. This is used for navigation and accessibility.
    ///     This label is visible to the user and should be human-readable. It should be unique within the page.
    ///     It is displayed in the block editor and in the block list/tree.k
    ///     It is displayed in the block editor and in the block list/tree.k
    /// </summary>
    public string? BlockLabel { get; set; }

    public virtual bool Equals(PageBlock? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return BlockId == other.BlockId && BlockType == other.BlockType && BlockPosition.Equals(other.BlockPosition) &&
               BlockData == other.BlockData && ChildBlockIds == other.ChildBlockIds &&
               ParentBlockIds == other.ParentBlockIds;
    }

    public override int GetHashCode() => HashCode.Combine(BlockId, BlockType);
}