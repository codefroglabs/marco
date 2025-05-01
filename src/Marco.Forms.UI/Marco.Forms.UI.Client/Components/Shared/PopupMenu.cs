using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Marco.Forms.UI.Client.Components.Shared;

public class PopupMenu : ComponentBase
{
    [Parameter] public Pressable? Trigger { get; set; }

    [Parameter]
    public Type? Component { get; set; }

    [Parameter, EditorRequired] public required RenderFragment Menu { get; set; }

    [Parameter]
    public bool Dim { get; set; }

    private bool isVisible;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        //Foo.Element = "a";
        if (Component is not null)
        {
            Trigger = new Pressable(b => { b.OpenComponent(0, Component); b.CloseComponent(); });
        }

        if (Trigger is not null)
        {
            Trigger.OnPress = EventCallback.Factory.Create(this, ToggleVisibility);
        }

        // overlay
        if (isVisible)
        {
            builder.OpenComponent<Overlay>(0);
            builder.AddAttribute(1, nameof(Overlay.OnClick),
                EventCallback.Factory.Create(this, () => { isVisible = false; }));
            builder.AddAttribute(2, nameof(Overlay.Dim), Dim);
            builder.CloseComponent();
        }

        // menu
        builder.OpenRegion(10);
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", "relative z-40");
        if (Trigger is not null)
        {
            builder.AddContent(2, Trigger.CreateFragment);
        }

        if (isVisible)
        {
            // anchor-name: --my-anchor
            builder.OpenElement(3, "div");
            builder.AddAttribute(4, "class", "absolute flex gap-3 items-center cursor-pointer text-gray-900");
            builder.AddAttribute(5, "style", "");

            // if a menu is clicked, it should close the menu
            builder.AddAttribute(6, "onclick", EventCallback.Factory.Create(this, ToggleVisibility));
            builder.AddContent(7, Menu);
            builder.CloseElement();
        }

        builder.CloseElement();
        builder.CloseRegion();
    }

    private void ToggleVisibility()
    {
        isVisible = !isVisible;
    }
}
