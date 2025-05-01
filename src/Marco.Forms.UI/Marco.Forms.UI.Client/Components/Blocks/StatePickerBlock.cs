using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Marco.Forms.UI.Client.Components.Blocks;

public class StatePickerBlock : ComponentBase, IBlazorBlock
{
    public RenderFragment Editor() =>
        builder =>
        {
            builder.OpenElement(0, "select");
            builder.OpenElement(1, "option");
            builder.AddAttribute(2, "value", "Alaska");
            builder.AddContent(3, "Alaska");
            builder.CloseElement();
            builder.CloseElement();
        };

    protected override void BuildRenderTree(RenderTreeBuilder builder) => builder.AddContent(0, "Alaska");
}