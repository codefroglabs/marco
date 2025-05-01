using Marco.Forms.UI.Client.Components;

namespace Marco.Forms.UI.Client.Models;

public static class BlockDefaults
{
    public const int Version = 1;

    public static BlockData CreateBlockData(PageBlock block)
    {
        return block.BlockType switch
        {
            BlockTypes.ContentEditable => new ContentBlockData
            {
                BlockId = block.BlockId,
                BlockType = block.BlockType,
                Version = block.BlockDataVersion,
                Text = "Write something here",
            },
            BlockTypes.Section => new SectionBlockData
            {
                BlockId = block.BlockId, BlockType = block.BlockType, Version = block.BlockDataVersion
            },
            BlockTypes.Root => new RootBlock
            {
                BlockId = block.BlockId, BlockType = block.BlockType, Version = block.BlockDataVersion
            },
            BlockTypes.List => new ListBlockData
            {
                BlockId = block.BlockId, BlockType = block.BlockType, Version = block.BlockDataVersion
            },
            BlockTypes.Navigation => new NavigationBlockData
            {
                BlockId = block.BlockId, BlockType = block.BlockType, Version = block.BlockDataVersion
            },
            BlockTypes.Block => new BlockData
            {
                BlockId = block.BlockId, BlockType = block.BlockType, Version = block.BlockDataVersion
            },
            BlockTypes.Link => new LinkBlockData
            {
                BlockId = block.BlockId, BlockType = block.BlockType, Version = block.BlockDataVersion,
                Text = "Link text",
                Url = "https://example.com"
            },
            _ => new() { BlockId = block.BlockId, BlockType = block.BlockType, Version = block.BlockDataVersion }
        };
    }
}
