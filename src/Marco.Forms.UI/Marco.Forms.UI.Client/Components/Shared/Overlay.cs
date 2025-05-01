using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Marco.Forms.UI.Client.Components.Shared;

public class Overlay : ComponentBase
{
    [Parameter]
    public EventCallback OnClick { get; set; }

    /// <summary>
    /// If true, the overlay dims the background. If false, the overlay is transparent.
    /// </summary>
    [Parameter]
    public bool Dim { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var bgClass = Dim ?  "bg-black opacity-25" : "bg-transparent";
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", $"fixed inset-0 {bgClass} z-30");
        builder.AddAttribute(2, "onclick", OnClick);
        builder.CloseElement();
    }
}
