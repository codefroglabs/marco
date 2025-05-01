using System.Threading.Channels;
using Marco.Forms.UI.Client.Components.Actions;
using Marco.Forms.UI.Client.Models;

namespace Marco.Forms.UI.Client.Components;

public record MarcoCanvasState
{
    private readonly Channel<BlockActionMessage> channel;

    private MarcoCanvasState()
    {
        channel = Channel.CreateUnbounded<BlockActionMessage>();
    }

    public ILogger? Logger { get; set; }
    public required PageBlock? RootBlock { get; set; }
    public required PageBlock? SelectedBlock { get; set; }

    /// <summary>
    /// The blocks that have been added to the page.
    /// </summary>
    public required HashSet<PageBlock> Blocks { get; set; }

    public static MarcoCanvasState New(HashSet<PageBlock> blocks)
    {
        var state = new MarcoCanvasState()
        {
            Blocks = blocks,
            RootBlock = blocks.Single(p => p.BlockType == BlockTypes.Root),
            SelectedBlock = blocks.FirstOrDefault(p => p.BlockType == BlockTypes.Root)
        };

        return state;
    }

    public void Sync(PageBlock block)
    {
        var existingBlock = Blocks.FirstOrDefault(p => p.BlockId == block.BlockId);
        if (existingBlock is null)
        {
            Logger?.LogWarning("Block not found for sync: {BlockId}", block.BlockId);
            channel.Writer.TryWrite(new(BlockActions.Add, block));
            Blocks.Add(block);
            return;
        }

        existingBlock.BlockData = block.BlockData;
        existingBlock.BlockPosition = block.BlockPosition;
        existingBlock.ChildBlockIds = block.ChildBlockIds;
        existingBlock.ParentBlockIds = block.ParentBlockIds;
        existingBlock.Formatting = block.Formatting;
        existingBlock.Layout = block.Layout;
        existingBlock.BlockLabel = block.BlockLabel;
        existingBlock.Tag = block.Tag;
        existingBlock.BlockDataVersion = block.BlockDataVersion;

        // Blocks = [..Blocks.Where(p => p.BlockId != block.BlockId), block];

        channel.Writer.TryWrite(new(BlockActions.Modify, block));
    }

    public void RemoveBlock(string blockId)
    {
        var block = Blocks.FirstOrDefault(p => p.BlockId == blockId);
        if (block is null)
        {
            Logger?.LogWarning("Block not found for removal: {BlockId}", blockId);
            return;
        }

        var parentPageBlock = Blocks.FirstOrDefault(b => b.BlockType == BlockTypes.Root);
        parentPageBlock!.ChildBlockIds!.Remove(block.BlockId!);

        // Create a new HashSet and remove the block from it
        var newBlocks = new HashSet<PageBlock>(Blocks);
        newBlocks.RemoveWhere(p => p.BlockId == block.BlockId);

        // Replace the CanvasState.Blocks with the new HashSet
        Blocks = newBlocks;

        if (SelectedBlock?.BlockId == blockId)
        {
            SelectedBlock = RootBlock;
        }

        channel.Writer.TryWrite(new(BlockActions.Delete, block));
    }

    public void MoveBlockDown(string blockId)
    {
        // find a relative position between the selected block and the next block
        // if there isn't a next block, then move to the bottom
        var block = Blocks.FirstOrDefault(b => b.BlockId == blockId);
        if (block is null)
        {
            Logger?.LogWarning("Block not found for move up: {BlockId}", blockId);
            return;
        }

        var noRoot = Blocks.Where(x => x.BlockType != BlockTypes.Root).ToList();
        var insertAfterElement = noRoot
            .Where(p => p.BlockPosition > block.BlockPosition && p.BlockId != blockId)
            .OrderBy(p => p!.BlockPosition)
            .FirstOrDefault();

        double newPosition = 0;
        if (insertAfterElement != null)
        {
            var insertBeforeElement = noRoot
                .Where(p => p.BlockPosition > insertAfterElement.BlockPosition)
                .OrderBy(p => p!.BlockPosition)
                .FirstOrDefault();

            newPosition = PositionBetween(insertAfterElement, insertBeforeElement);
        }
        else
        {
            // no more after, already at the end
            newPosition = block.BlockPosition;
        }

        Logger?.LogInformation("Moving block from {OldPosition} to new position {NewPosition}", block.BlockPosition, newPosition);

        block.BlockPosition = newPosition;

        channel.Writer.TryWrite(new(BlockActions.Modify, block));
    }

    public void MoveBlockUp(string blockId)
    {
        var block = Blocks.FirstOrDefault(b => b.BlockId == blockId);
        if (block is null)
        {
            Logger?.LogWarning("Block not found for move up: {BlockId}", blockId);
            return;
        }

        // get the siblings of the selected block
        var siblings = Blocks.Where(p => p.ParentBlockIds.Contains(block.ParentBlockIds.First())).ToList();
        var insertBeforeElement = siblings
            .Where(p => p.BlockPosition <= block.BlockPosition && p.BlockId != blockId)
            .OrderByDescending(p => p.BlockPosition)
            .FirstOrDefault();

        PageBlock? insertAfterElement = null;
        if (insertBeforeElement != null)
        {
            insertAfterElement = siblings
                .Where(p => p.BlockPosition < insertBeforeElement.BlockPosition)
                .OrderByDescending(p => p.BlockPosition)
                .FirstOrDefault();
        }

        double newPosition = 0;

        if (insertAfterElement is null)
        {
            if (insertBeforeElement == null)
            {
                Logger?.LogInformation("insertBeforeElement is null, setting to minElement");
            }
            else
            {
                newPosition = insertBeforeElement.BlockPosition / 2;
            }
        }
        else
        {
            newPosition = PositionBetween(insertAfterElement, insertBeforeElement);
        }

        block.BlockPosition = newPosition;

        channel.Writer.TryWrite(new(BlockActions.Modify, block));
    }

    /// <summary>
    /// Adds the block to the current selected block or the root block if no block is selected.
    /// </summary>
    /// <param name="block"></param>
    public void AddBlockToCurrentParent(PageBlock block)
    {
        if (Blocks.Contains(block))
        {
            Logger?.LogWarning("Block already added: {BlockId}", block.BlockId);
            return;
        }

        PageBlock parent;
        if (SelectedBlock?.BlockType == BlockTypes.Section)
        {
            Logger?.LogInformation("Adding block to section block {@Block}", SelectedBlock);
            parent = SelectedBlock;
        }
        else if (RootBlock is not null)
        {
            Logger?.LogInformation("Adding block to root block {@Block}", RootBlock);
            parent = RootBlock;
        }
        else
        {
            Logger?.LogError("Root block not found");
            return;
        }

        // get siblings of the selected block
        // set position to end of current parent block
        var siblings = Blocks.Where(p => p.ParentBlockIds.Contains(parent.BlockId)).ToList();
        var lastSibling = siblings.OrderByDescending(p => p.BlockPosition).FirstOrDefault();
        block.BlockPosition = lastSibling?.BlockPosition + 1 ?? 1;

        block.ParentBlockIds = [parent.BlockId];
        Blocks = [..Blocks, block];

        // ensure the parent block has the child block
        // Add this block to the parent's child block ids
        foreach (var parentId in block.ParentBlockIds)
        {
            var parentBlock = Blocks.SingleOrDefault(p => p.BlockId == parentId);
            if (parentBlock is not null)
            {
                Logger?.LogInformation("Linked block {BlockId} to parent block {ParentId}", block.BlockId,
                    parentBlock.BlockId);
                parentBlock.ChildBlockIds ??= [];
                parentBlock.ChildBlockIds.Add(block.BlockId);
            }
        }

        // Add this block to the child's parent block ids
        foreach (var childId in block.ChildBlockIds)
        {
            var childBlock = Blocks.SingleOrDefault(p => p.BlockId == childId);
            if (childBlock is not null)
            {
                Logger?.LogInformation("Linked block {BlockId} to child block {ChildId}", block.BlockId,
                    childBlock.BlockId);
                childBlock.ParentBlockIds ??= [];
                childBlock.ParentBlockIds.Add(block.BlockId);
            }
        }

        channel.Writer.TryWrite(new(BlockActions.Add, block));
    }

    public void AddBlockAtRelativePosition(PageBlock newBlock, string? parentBlockId = null,
        PageBlock? placeBlockAfter = null)
    {
        // ArgumentException.ThrowIfNullOrWhiteSpace(PageId);

        // todo: need to calculate insert position if there's another block at n+0.1
        // e.g. it would be (n, n+0.1, n+0.2) if there are 3 blocks added in sequence
        // if blocks are added out of sequence, it would like look (n, n+0.75, n+0.1) if the 2nd block was added third
        // this is a common pattern for relative sequencing for inserting blocks/text/data

        PageBlock parentBlock;
        if (parentBlockId is null)
        {
            parentBlock = Blocks.Single(p => p.BlockId == RootBlock?.BlockId);
        }
        // add the new block to the current section, unless it's another section
        else if (SelectedBlock?.BlockType == BlockTypes.Section && newBlock.BlockType != BlockTypes.Section)
        {
            parentBlock = SelectedBlock;
        }
        else
        {
            ArgumentNullException.ThrowIfNull(RootBlock);
            parentBlock = RootBlock;
        }

        var siblings = Blocks.Where(p => p.ParentBlockIds.Contains(parentBlock.BlockId)).ToList();

        double position;
        if (placeBlockAfter is null)
        {
            Logger?.LogInformation("No position specified for block placement, finding smallest position for block");

            // place block at start of page
            var smallestPosition = siblings.Count > 0
                ? siblings.Min(p => p.BlockPosition)
                : 0;

            position = smallestPosition / 2;
        }
        else
        {
            Logger?.LogInformation("Looking for position after blockId {BlockId}", placeBlockAfter.BlockId);

            // place block after the selected block and before the next block, if there is one

            var nextBlock = siblings
                .OrderBy(p => p.BlockPosition)
                .FirstOrDefault(p => p.BlockType != BlockTypes.Root &&
                                     p.BlockPosition > placeBlockAfter.BlockPosition);

            if (nextBlock is not null)
            {
                position = (placeBlockAfter.BlockPosition + nextBlock.BlockPosition) / 2;
                Logger?.LogInformation(
                    "Next block found, will place new block between block\n{CurrentBlock}\nand\n{NextBlockId}\nat position: {Position}",
                    placeBlockAfter, nextBlock, position);
            }
            else
            {
                position = placeBlockAfter.BlockPosition + 1;
                Logger?.LogInformation(
                    "No next block found, will place new block after\n{Block}\nat position: {Position}",
                    placeBlockAfter, position);
            }
        }

        newBlock.BlockPosition = position;

        // Add the block to the selected block, if any, otherwise adds to the root block.
        Logger?.LogInformation("Adding block to parent block {@Block}", parentBlock);
        newBlock.ParentBlockIds = [parentBlock.BlockId];

        // Add the block as a child of the parent block.
        parentBlock.ChildBlockIds ??= [];
        parentBlock.ChildBlockIds.Add(newBlock.BlockId);

        Blocks = [..Blocks, newBlock];

        channel.Writer.TryWrite(new(BlockActions.Add, newBlock));
    }

    public void AddBlockDefinitionAtRelativePosition(BlockDefinition blockDefinition, string? parentBlockId = null,
        PageBlock? placeBlockAfter = null)
    {
        Logger?.LogInformation("Creating PageBlock for blockId: {BlockId}", blockDefinition.Id);
        var block = CreateBlock(blockDefinition);
        AddBlockAtRelativePosition(block, parentBlockId, placeBlockAfter);
    }

    private double PositionBetween(PageBlock after, PageBlock? before)
    {
        if (after.BlockPosition == before?.BlockPosition)
        {
            // if the next block is at the same position, then move to the next position
            after.BlockPosition += 0.1;
        }

        // find a relative position between a and b
        // ex 1. if a is 0.1 and b is 0.2, then the new position would be 0.15
        // ex 2. if a is 0.1 and b is null, then the new position would be 0.2

        return before is not null ? (after.BlockPosition + before.BlockPosition) / 2 : after.BlockPosition + 1;

        // if (before is not null)
        // {
        //     var result = (after.BlockPosition + before.BlockPosition) / 2;

        //     return result;
        // }
        // else
        // {
        //     return after.BlockPosition + 1;
        // }
    }

    private PageBlock CreateBlock(BlockDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(RootBlock);

        var block = new PageBlock
        {
            BlockId = Uuid.New(),
            PageId = RootBlock.PageId,
            BlockType = definition.Id,
            Tag = definition.Id is BlockTypes.ContentEditable ? "p" : "div",
            Formatting = new()
            {
                Margin = new(1),
                Padding = new(1),
                FontFamily = "font-sans",
                FontSizeV2 = 1,
                TextColor = "rgba(0,0,0,0.8)",
                FontSizeUnit = CssUnit.Rem,
                Border = new Border { Color = "gray", Width = 0, Style = "solid" }
            },
            // only enable layout for section and root blocks
            Layout = definition.Id is BlockTypes.Section or BlockTypes.Root or BlockTypes.Navigation
                ? new BlockLayout() { GridLayoutOptions = new GridLayoutOptions { TemplateColumns = 1, } }
                : null
        };

        block.BlockData = BlockDefaults.CreateBlockData(block);

        return block;
    }

    public async Task SubscribeAsync(Func<BlockActionMessage, Task> action, CancellationToken cancellationToken)
    {
        await foreach (var message in channel.Reader.ReadAllAsync(cancellationToken))
        {
            await action(message);
        }
    }
}
