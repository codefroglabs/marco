using Marco.Forms.UI.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Marco.Forms.UI.Client.Components.Blocks;

public class LinkBlock : ComponentBase, IBlazorBlock
{
    [Parameter] public required PageBlock Block { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Block.BlockData is LinkBlockData data)
        {
            builder.OpenElement(0, "a");
            builder.AddAttribute(1, "href", data.Url);
            builder.AddAttribute(2, "class", $"hover:underline {Block.Formatting?.ToCssClass()}");
            builder.AddAttribute(3, "style", Block.Formatting?.ToCssStyle());
            builder.AddContent(4, data.Text);
            builder.CloseElement();
        }
        else
        {
            builder.OpenElement(5, "div");
            builder.AddContent(6, "Add a link to the block");
            builder.CloseElement();
        }
    }
}