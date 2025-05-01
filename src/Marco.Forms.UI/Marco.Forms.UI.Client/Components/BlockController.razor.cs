using Marco.Forms.UI.Client.Models;
using Microsoft.JSInterop;
using Pure.Blazor.Components;

namespace Marco.Forms.UI.Client.Components;

public partial class BlockController(
    ILogger<BlockController> logger,
    DialogService dialogService,
    IJSRuntime jsRuntime)
{
    private async Task OnEditorAction(EditorAction editorAction)
    {
        if (editorAction.Action == EditorActionType.Enter)
        {
            // Create a new block
            var block = CreateBlock(new BlockDefinition{ Id = BlockTypes.ContentEditable, Description = "", Icon = "", Name = ""});

            var parentBlockId = CanvasState.SelectedBlock?.ParentBlockIds.FirstOrDefault();
            if (parentBlockId is not null)
            {
                CanvasState.AddBlockAtRelativePosition(block, parentBlockId, CanvasState.SelectedBlock);
                await CanvasStateChanged.InvokeAsync(CanvasState);
                //CanvasState.SelectedBlock = block;
            }
        }
    }

    private PageBlock CreateBlock(BlockDefinition definition)
    {
        var block = new PageBlock
        {
            BlockId = Uuid.New(),
            PageId = Block.PageId,
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
                Border = new Border { Color = "red", Width = 1, Style = "solid" }
            },
            // only enable layout for section and root blocks
            Layout = definition.Id is BlockTypes.Section or BlockTypes.Root
                ? new BlockLayout() { GridLayoutOptions = new GridLayoutOptions { TemplateColumns = 1, } }
                : null
        };
        block.BlockData = BlockDefaults.CreateBlockData(block);

        return block;
    }
}

public enum EditorActionType
{
    Enter,
    Exit,
    Save,
    Cancel
}

public record EditorAction(EditorActionType Action)
{
    public object? Data { get; set; }
}
