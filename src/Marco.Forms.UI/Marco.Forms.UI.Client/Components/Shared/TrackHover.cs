using Microsoft.AspNetCore.Components;

namespace Marco.Forms.UI.Client.Components.Shared;

public class TrackHover : IComponent
{
    private RenderHandle renderHandle;
    private bool currentHoverState;
    private bool pinned;

    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public EventCallback<bool> OnHoverChanged { get; set; }

    public void Attach(RenderHandle handle)
    {
        this.renderHandle = handle;
    }

    public Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);
        Render();
        return Task.CompletedTask;
    }

    private void Render()
    {
        renderHandle.Render(builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "onmouseover", EventCallback.Factory.Create(this, () => HandleHover(true)));
            builder.AddAttribute(2, "onmouseout", EventCallback.Factory.Create(this, () => HandleHover(false)));
            builder.AddAttribute(3, "onclick", EventCallback.Factory.Create(this, HandleHoverClick));
            builder.AddContent(4, ChildContent);
            builder.CloseElement();
        });
    }

    private async Task HandleHoverClick()
    {
        // if the user clicks on the hover component, it should pin the hover state
        // so that it doesn't change until the user clicks again
        // if the user first hovered, then clicked, it should stay open
        if (currentHoverState && !pinned)
        {
            pinned = true;
            return;
        }

        var nextHoverState = !currentHoverState;
        pinned = nextHoverState;

        currentHoverState = nextHoverState;
        if (OnHoverChanged.HasDelegate)
        {
            await OnHoverChanged.InvokeAsync(nextHoverState);
        }
    }

    private async Task HandleHover(bool isHovering)
    {
        if (pinned)
        {
            return;
        }

        currentHoverState = isHovering;
        if (OnHoverChanged.HasDelegate)
        {
            await OnHoverChanged.InvokeAsync(isHovering);
        }
    }
}
