using Marco.Forms.UI.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;

namespace Marco.Forms.UI.Client.Components.Blocks;

public class BlockRenderer2(ILogger<BlockRenderer2> logger, IJSRuntime jsRuntime) : ComponentBase
{
    /// <summary>
    /// Reference to the block component that is being rendered
    /// </summary>
    private IBlazorBlock? blockRef;

    private IJSObjectReference? javascript;

    /// <summary>
    /// Indicates if the editable has focus
    /// </summary>
    private bool hasFocus { get; set; }

    protected virtual string ElementId => GetElementId();
    [Parameter, EditorRequired] public required string SiteId { get; set; }
    [Parameter, EditorRequired] public required PageBlock Block { get; set; }
    [Parameter] public EventCallback<PageBlock> BlockChanged { get; set; }
    [Parameter] public MarcoCanvasState? CanvasState { get; set; }
    [Parameter] public EventCallback<MarcoCanvasState> CanvasStateChanged { get; set; }
    [Parameter] public EventCallback<EditorAction> EditorAction { get; set; }
    [Parameter] public EventCallback<PageBlock?> BlockSelected { get; set; }
    private DotNetObjectReference<BlockRenderer2>? reference;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            try
            {
                reference = DotNetObjectReference.Create(this);
                javascript = await jsRuntime.InvokeAsync<IJSObjectReference>("import", "../js/blocks.js");
                await javascript.InvokeVoidAsync("addBlock", reference, ElementId);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to initialize content editable javascript for block {@block}", Block);
            }
        }

        if (blockRef is not null && IsEditing())
        {
            try
            {
                await blockRef.OnEditorActivatedAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to activate editor for block {blockId}", Block.BlockId);
            }
        }
    }

    private string GetElementId() => $"block-{Block.BlockId}";

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");

        // var width = Block.Formatting?.PlacementDefinition.Width ?? 0;
        // var height = Block.Formatting?.PlacementDefinition.Height ?? 0;
        builder.AddAttribute(1, "class",
            $"group/page-block relative focus-within:z-10 overflow-y-hidden"); //  group/page-block relative focus-within:z-10 //

        builder.AddAttribute(2, "tabindex", 0);
        builder.AddAttribute(3, "id", GetElementId());
        builder.AddAttribute(4, "data-block-type", Block.BlockType);

        // builder.AddAttribute(4, "style", $"width: {Math.Max(width, 20)}px; height: {Math.Max(height, 20)}px;");

        // begin rendering the resizable div
        builder.OpenRegion(5);

        if (IsEditing() && blockRef is not null)
        {
            if (Block.BlockType == BlockTypes.ContentEditable)
            {
                RenderFragment f = blockRef.Editor();
                builder.AddContent(3, f);
                builder.CloseRegion();
                builder.CloseElement();
                return;
            }
        }

        // not editing or editing is the same as viewing
        RenderFragment fragment = Block.BlockType switch
        {
            BlockTypes.ContentEditable => b =>
            {
                b.OpenComponent<ContentBlock>(2);
                b.AddComponentParameter(3, nameof(ContentBlock.Block), Block);
                b.AddComponentParameter(4, nameof(ContentBlock.Tag), Block.Tag ?? "div");
                b.AddComponentParameter(7, nameof(ContentBlock.BlockChanged),
                    EventCallback.Factory.Create<PageBlock>(this, OnBlockChanged));
                b.AddComponentParameter(8, nameof(ContentBlock.EditorAction), EditorAction);
                b.AddComponentReferenceCapture(6, instance => blockRef = (IBlazorBlock)instance);
                b.CloseComponent();
            },
            BlockTypes.Section => b =>
            {
                b.OpenComponent<SectionBlock>(2);
                b.AddComponentParameter(3, nameof(SectionBlock.Block), Block);
                b.AddComponentParameter(4, nameof(SectionBlock.BlockChanged),
                    EventCallback.Factory.Create<PageBlock>(this, OnBlockChanged));
                b.AddComponentParameter(5, nameof(SectionBlock.CanvasState), CanvasState);
                b.AddComponentParameter(6, nameof(SectionBlock.CanvasStateChanged),
                    EventCallback.Factory.Create<MarcoCanvasState>(this, CanvasStateChanged));
                b.AddComponentParameter(7, nameof(SectionBlock.SiteId), SiteId);
                b.CloseComponent();
            },
            BlockTypes.List => b =>
            {
                b.OpenComponent<ListBlock>(2);
                b.AddComponentParameter(3, nameof(ListBlock.Block), Block);
                b.CloseComponent();
            },
            BlockTypes.Link => b =>
            {
                b.OpenComponent<LinkBlock>(2);
                b.AddComponentParameter(3, nameof(LinkBlock.Block), Block);
                b.CloseComponent();
            },
            _ => b =>
            {
                b.OpenElement(10, "div");
                b.AddContent(13, "Unknown block type");
                b.CloseElement();
            }
        };
        builder.AddContent(3, fragment);
        builder.CloseRegion();
        builder.CloseElement();
    }

    private PageBlock? blockChange;



    private void OnBlockChanged(PageBlock block)
    {
        // cache the block change so we can broadcast it on blur
        blockChange = block;
    }

    private bool IsEditing()
    {
        return CanvasState?.SelectedBlock?.BlockId == Block.BlockId;
    }

    [JSInvokable]
    public async Task OnFocusOut()
    {
        hasFocus = false;

        // disable resizing
        //javascript?.InvokeVoid("disableResizing", ElementId);

        if (blockChange == null)
        {
            logger.LogInformation("No block change detected for block {blockId}", Block.BlockId);
            return;
        }

        logger.LogInformation("Block change detected for block {blockId}", Block.BlockId);
        // Sync any latest cached block change
        await BlockChanged.InvokeAsync(blockChange);
        await InvokeAsync(StateHasChanged);
    }

    [JSInvokable]
    public async Task OnFocus()
    {
        logger.LogInformation("Focus event received for block {blockId}", Block.BlockId);
        hasFocus = true;
        await BlockSelected.InvokeAsync(Block);

        await InvokeAsync(StateHasChanged);
    }

    [JSInvokable]
    public Task OnFocusIn()
    {
        // logger.LogInformation("Focus in event received for block {blockId}", Block.BlockId);
        return Task.CompletedTask;
    }

    [JSInvokable]
    public void OnResize(double width, double height)
    {
        // logger.LogDebug("Resizing block to {width}x{height}", width, height);
        // var block = Block with { Formatting = Block.Formatting ?? new BlockFormatting() };
        // block.Formatting.PlacementDefinition = new PlacementDefinition { Width = width, Height = height };
        // BlockChanged.InvokeAsync(block);
    }
}
