using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Marco.Forms.UI.Client.Components.Blocks;

public class ContainerBlockView : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public required PageBlock Block { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", $"relative");

        builder.AddContent(3, ChildContent);
        builder.CloseElement();
    }
}
