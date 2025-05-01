using Marco.Forms.UI.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Pure.Blazor.Components;

namespace Marco.Forms.UI.Client.Components.Blocks;

/// <summary>
///     Renders a block of formatted text.
/// </summary>
public sealed class ContentBlock : ComponentBase, IBlazorBlock
{
    private IJSObjectReference? javascript;
    private DotNetObjectReference<ContentBlock>? reference;
    public string ElementId => GetElementId();
    [Parameter] public string Tag { get; set; } = "p";
    [Parameter] public required PageBlock Block { get; set; }

    [Parameter] public EventCallback<PageBlock> BlockChanged { get; set; }
    [Parameter] public EventCallback<EditorAction> EditorAction { get; set; }
    [Inject] public required ILogger<ContentBlock> Logger { get; set; }
    [Inject] public required IJSRuntime JsRuntime { get; set; }

    private string GetElementId() => $"content-{Block.BlockId}";

    public async Task OnEditorActivatedAsync()
    {
        if (javascript is null)
        {
            Logger.LogWarning("Javascript is not initialized");
            return;
        }

        reference = DotNetObjectReference.Create(this);
        Logger.LogDebug("Activating block {@block}", Block);
        //javascript?.InvokeVoid("placeCursorEnd", ElementId);
        await javascript.InvokeVoidAsync("trackContentEditableChanges", reference, ElementId);
        Logger.LogDebug("Activated block {BlockId}", Block.BlockId);
        await Task.CompletedTask;
    }

    public RenderFragment Editor() =>
        builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "");
            builder.AddAttribute(2, "contenteditable", "false");

            builder.OpenRegion(10);
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "id", ElementId);
            builder.AddAttribute(2, "contenteditable", "true");

            // todo: was used for the /slash command
            builder.AddAttribute(3, "onkeydown", EventCallback.Factory.Create<KeyboardEventArgs>(this, OnKeyDown));
            builder.AddAttribute(4, "onkeydown:preventDefault");


            builder.AddAttribute(6, "tabindex", "0");
            builder.AddAttribute(7, "class", "min-h-4 outline-none");

            // start a region for the content block
            builder.OpenRegion(8);

            builder.OpenComponent<ContentBlock>(2);
            builder.AddComponentParameter(3, nameof(Block), Block);
            builder.AddComponentParameter(4, nameof(Tag), Tag);
            builder.CloseComponent();

            // end region for the content block
            builder.CloseRegion();

            builder.CloseElement();
            builder.CloseRegion();
            builder.CloseElement();
        };

    private Task OnKeyDown(KeyboardEventArgs arg)
    {
        if (arg.Key == "Enter" && EditorAction.HasDelegate)
        {
        }

        return Task.CompletedTask;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        try
        {
            javascript = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "../js/blocks.js");
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Failed to initialize content editable javascript");
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(8, Block.Tag ?? Tag);
        builder.AddAttributeIfNotNullOrEmpty(9, "class", $"{Block.Formatting?.ToCssClass()}");
        var cssStyle = Block.Formatting?.ToCssStyle();
        builder.AddAttributeIfNotNullOrEmpty(10, "style", cssStyle);
        if (Block.BlockData is ContentBlockData blockData)
        {
            builder.AddContent(11, blockData?.Text ?? "Write something here");
        }
        else if (Block.BlockData is not null)
        {
            builder.AddContent(11, "Invalid block");

            Logger.LogWarning("Block data is not a ContentBlockData type");
        }

        builder.CloseElement();
    }

    [JSInvokable]
    public async Task ContentChanged(string? content)
    {
        await OnContentChange(content);
    }

    public async Task OnContentChange(string? content)
    {
        Logger.LogInformation("Content Changed to {content}", content);
        if (Block.BlockData is not ContentBlockData blockData)
        {
            blockData = new() { BlockId = Block.BlockId, BlockType = BlockTypes.ContentEditable };
        }

        var b = Block with { BlockData = blockData with { Text = content ?? "" } };
        await BlockChanged.InvokeAsync(b);
    }
}
