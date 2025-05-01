using Marco.Forms.UI.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Marco.Forms.UI.Client.Components.Blocks;

public class ListBlock : ComponentBase, IBlazorBlock
{
    [Parameter] public required PageBlock Block { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Block.BlockData is ListBlockData data)
        {
            builder.OpenElement(0, data.ListType == ListType.Ordered ? "ol" : "ul");
            builder.AddAttribute(1, "class",
                $"{(data.ListType == ListType.Ordered ? "list-disc" : "list-decimal")} {Block.Formatting?.ToCssClass()}");
            builder.AddAttribute(2, "style", Block.Formatting?.ToCssStyle());
            foreach (var item in data.Items)
            {
                builder.OpenElement(3, "li");
                builder.AddContent(4, item.Text);
                builder.CloseElement();
            }

            builder.CloseElement();
        }
        else
        {
            builder.OpenElement(5, "div");
            builder.AddContent(6, "Add items to the list");
            builder.CloseElement();
        }
    }
}