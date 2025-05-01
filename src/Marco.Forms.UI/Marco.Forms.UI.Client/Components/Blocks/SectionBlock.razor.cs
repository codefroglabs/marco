using Microsoft.AspNetCore.Components;

namespace Marco.Forms.UI.Client.Components.Blocks;

public partial class SectionBlock(
    ILogger<SectionBlock> logger)
{
    [Parameter, EditorRequired] public required string SiteId { get; set; }
    [Parameter, EditorRequired] public required MarcoCanvasState CanvasState { get; set; }
    [Parameter, EditorRequired] public required EventCallback<MarcoCanvasState> CanvasStateChanged { get; set; }

    public IEnumerable<PageBlock> PageBlocks { get; set; } = [];

    public bool HasError { get; set; }

    protected override Task OnInitializedAsync()
    {
        logger.LogInformation("Loading blocks for section {sectionId}", Block.BlockId);
        HasError = false;
        var childBlockIds = Block.ChildBlockIds ?? [];
        PageBlocks = CanvasState.Blocks.Where(p => childBlockIds.Contains(p.BlockId)).ToList().OrderBy(p => p.BlockPosition);
        return Task.CompletedTask;
    }

    protected override void OnParametersSet()
    {
        logger.LogInformation("Setting parameters for section {sectionId}", Block.BlockId);
        var childBlockIds = Block.ChildBlockIds ?? [];

        PageBlocks = CanvasState.Blocks.Where(p => childBlockIds.Contains(p.BlockId)).OrderBy(p => p.BlockPosition).ToList().OrderBy(p => p.BlockPosition);
    }
}
