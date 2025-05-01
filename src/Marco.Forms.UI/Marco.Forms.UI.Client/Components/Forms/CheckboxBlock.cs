using Marco.Forms.UI.Client.Components.Blocks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Marco.Forms.UI.Client.Components.Forms;

public class CheckboxBlock : ComponentBase, IBlazorBlock
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
            builder.AddAttribute(1, "class", "form-check flex items-center");

            builder.OpenElement(10, "label");
            builder.AddAttribute(11, "class", "form-check-label text-sm font-medium text-gray-700 px-2");
            builder.AddContent(13, Field.Name);
            builder.CloseElement();
            builder.OpenElement(100, "div");
            builder.AddAttribute(101, "class", "grid auto-cols-max grid-flow-col gap-2");
            foreach (var f in Field.Options)
            {
                builder.OpenElement(14, "div");
                builder.AddAttribute(15, "class", "text-xs text-gray-500 flex gap-2 p-2 text-md");
                // builder.AddContent(16, f.Label);
                builder.OpenRegion(17);
                builder.OpenElement(2, "input");
                builder.AddAttribute(3, "class", "form-check-input");
                builder.AddAttribute(4, "type", "checkbox");
                builder.AddAttribute(5, "id", $"Field.FormFieldId_{f.Label}");
                builder.AddAttribute(6, "name", $"Field.FormFieldId_{f.Label}");
                builder.AddAttribute(7, "value", "true");
                builder.AddAttribute(8, "checked", f.Value.BooleanValue == true);
                builder.AddAttribute(9, "onchange",
                    EventCallback.Factory.Create<ChangeEventArgs>(this, e => OnChange(f, e)));
                builder.CloseElement();

                builder.OpenElement(10, "label");
                builder.AddAttribute(11, "class", "form-check-label text-sm font-medium text-gray-700 px-2");
                builder.AddAttribute(12, "for", f.Label);
                builder.AddContent(13, f.Label);
                builder.CloseElement();
                builder.CloseRegion();

                builder.CloseElement();
            }
            builder.CloseElement();

            builder.CloseElement();
        };
    }

    private void OnChange(FormFieldOption o, ChangeEventArgs e)
    {
        if (e.Value is bool value)
        {
            o.Value.BooleanValue = value;
        }
    }
}