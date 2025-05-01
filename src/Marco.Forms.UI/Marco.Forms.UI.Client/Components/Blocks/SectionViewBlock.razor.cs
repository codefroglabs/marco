using Microsoft.AspNetCore.Components;

namespace Marco.Forms.UI.Client.Components.Blocks;

public partial class SectionViewBlock(
    ILogger<SectionViewBlock> logger)
{
    [Parameter] public IEnumerable<PageBlock> PageBlocks { get; set; } = [];

    [Parameter, EditorRequired] public required string SiteId { get; set; }
    public bool HasError { get; set; }

    protected override Task OnInitializedAsync()
    {
        logger.LogInformation("Loading blocks for section {sectionId}", Block.BlockId);
        HasError = false;
        return Task.CompletedTask;
    }
}
