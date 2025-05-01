using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Pure.Blazor.Components;

namespace Marco.Forms.UI.Client.Components.Shared;

public record Pressable
{
    public string? Content { get; set; }

    public Pressable(RenderFragment childContent, string cssClass = "")
    {
        ChildContent = childContent;
        CssClass = cssClass;
    }

    public Pressable(string content, string? cssClass = default, string? cssStyle = default, EventCallback onPress = default)
    {
        this.Content = content;
        CssClass = cssClass;
        CssStyle = cssStyle;
        OnPress = onPress;
    }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string Element { get; set; } = "button";

    public EventCallback OnPress { get; set; }

    public Action? OnPressAction { get; set; }

    [Parameter]
    public string? CssClass { get; set; }

    [Parameter]
    public string? CssStyle { get; set; }

    public RenderFragment CreateFragment => builder =>
    {
        builder.OpenElement(0, Element);
        builder.AddAttributeIfNotNullOrEmpty(1, "class", CssClass);
        builder.AddAttributeIfNotNullOrEmpty(2, "style", CssStyle);
        builder.AddAttribute(3, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, Pressed));
        if (ChildContent is not null)
        {
            builder.AddContent(4, ChildContent);
        }
        else
        {
            builder.AddContent(5, Content);
        }

        builder.CloseElement();
    };

    private async Task Pressed(MouseEventArgs e)
    {
        await OnPress.InvokeAsync(e);
    }
}
