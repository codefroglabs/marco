using Marco.Forms.UI.Client.Components.Blocks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Marco.Forms.UI.Client.Components.Forms;

public class RadioBlock : ComponentBase, IBlazorBlock
{
    [Parameter] public required FieldSchema Field { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var fragment = Render();
        fragment(builder);
    }

    private RenderFragment Render()
    {
        return builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "form-radio flex items-center");
            var optionOffset = 0;
            const int optionSize = 12;
            foreach (var option in Field.Options)
            {
                builder.OpenElement(2 + optionOffset, "input");
                builder.AddAttribute(3 + optionOffset, "class", "form-radio-input");
                builder.AddAttribute(4 + optionOffset, "type", "radio");
                builder.AddAttribute(5 + optionOffset, "id", option.Label);
                builder.AddAttribute(6 + optionOffset, "name", option.Label);
                builder.AddAttribute(7 + optionOffset, "value", option.Value.TextValue);
                builder.AddAttribute(8 + optionOffset, "checked", option.Value.TextValue == Field.Value.TextValue);
                builder.AddAttribute(9 + optionOffset, "onchange",
                    EventCallback.Factory.Create<ChangeEventArgs>(this, OnChange));
                builder.CloseElement();

                builder.OpenElement(10 + optionOffset, "label");
                builder.AddAttribute(11 + optionOffset, "class",
                    "form-radio-label text-sm font-medium text-gray-700 px-2");
                builder.AddAttribute(12 + optionOffset, "for", option.Label);
                builder.AddContent(13 + optionOffset, option.Label);
                builder.CloseElement();

                optionOffset += optionSize;
            }

            builder.CloseElement();
        };
    }

    private void OnChange(ChangeEventArgs e)
    {
        if (e.Value is string value)
        {
            Field.Value.TextValue = value;
        }
    }
}