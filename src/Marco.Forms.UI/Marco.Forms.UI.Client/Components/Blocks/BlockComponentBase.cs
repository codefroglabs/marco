using Marco.Forms.UI.Client.Models;
using Microsoft.AspNetCore.Components;

namespace Marco.Forms.UI.Client.Components.Blocks;

public class BlockComponentBase<T> : ComponentBase where T : BlockData
{
    /// <summary>
    /// Blocks can request the next block to be created by invoking this callback.
    ///
    /// The block editor will handle the creation of the next block. If the block definition is null,
    /// the block editor should choose the next block type, usually a paragraph block.
    /// </summary>
    [Parameter]
    public EventCallback<BlockDefinition?> OnNextBlockRequested { get; set; }

    [Parameter] public EventCallback BlockSelected { get; set; }
    [Parameter] public EventCallback<PageBlock> BlockChanged { get; set; }
    [Parameter, EditorRequired] public required PageBlock Block { get; set; }
}
