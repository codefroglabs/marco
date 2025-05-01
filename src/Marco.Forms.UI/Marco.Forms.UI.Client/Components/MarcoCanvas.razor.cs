using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Marco.Forms.UI.Client.Components;

public partial class MarcoCanvas(
    IJSRuntime jsRuntime
)
{
    private IJSObjectReference? module;

    [Parameter, EditorRequired] public required string SiteId { get; set; }
    [Parameter, EditorRequired] public required MarcoCanvasState CanvasState { get; set; }
    [Parameter, EditorRequired] public required EventCallback<MarcoCanvasState> CanvasStateChanged { get; set; }

    private IEnumerable<PageBlock> TopLevelBlocks => CanvasState.RootBlock is null
        ? []
        : CanvasState.Blocks.Where(p => p.ParentBlockIds?.Contains(CanvasState.RootBlock.BlockId) == true);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        module = await jsRuntime.InvokeAsync<IJSObjectReference>("import",
            "./Components/Marco/MarcoCanvas.razor.js");
        await module.InvokeVoidAsync("initMarco");
    }
}
