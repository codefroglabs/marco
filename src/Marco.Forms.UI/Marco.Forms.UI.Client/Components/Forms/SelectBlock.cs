using Marco.Forms.UI.Client.Components.Blocks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Pure.Blazor.Components;

namespace Marco.Forms.UI.Client.Components.Forms;

public class SelectBlock : ComponentBase, IBlazorBlock
{
    [Parameter] public required FieldSchema Field { get; set; }
    [Parameter] public bool ShowLabel { get; set; }
    [Parameter] public EventCallback<string> FieldChanged { get; set; }

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
            builder.AddAttribute(1, "class", "form-group");
            if (ShowLabel)
            {
                builder.OpenElement(2, "label");
                builder.AddAttribute(3, "for", Field.FormFieldId);
                builder.AddAttribute(4, "class", "text-[10px] pb-1 pt-2 font-bold uppercase text-gray-600");
                builder.AddContent(5, Field.Name);
                builder.CloseElement();
            }

            builder.OpenElement(6, "select");
            builder.AddAttribute(7, "class", "border-1 rounded border-brand-600 px-1 py-0.5 text-sm");
            builder.AddAttribute(8, "id", Field.FormFieldId);
            builder.AddAttribute(9, "name", Field.FormFieldId);
            builder.AddAttribute(10, "value", Field.Value);
            builder.AddAttribute(11, "onchange",
                EventCallback.Factory.Create<ChangeEventArgs>(this, OnChange));
            foreach (var option in Field.Options)
            {
                builder.OpenElement(12, "option");

                var value = option.Value.ValueType switch
                {
                    FieldValueType.Boolean => option.Value.BooleanValue.ToString(),
                    FieldValueType.Number => option.Value.NumberValue.ToString(),
                    FieldValueType.Text => option.Value.TextValue,
                    FieldValueType.Date => option.Value.DateValue.ToString(),
                    FieldValueType.Multi => option.Value.MultiValue?.ToString() ?? string.Empty,
                    _ => option.Value.TextValue
                };
                builder.AddAttribute(13, "value", value);
                builder.AddContent(14, option.Label);
                builder.CloseElement();
            }

            builder.OpenRegion(15);
            builder.OpenElement(1, "div");
            builder.AddAttribute(2, "class", "flex gap-2");

            builder.OpenComponent<Input<string>>(3);
            builder.CloseComponent();
            builder.OpenComponent<Button>(4);
            builder.AddComponentParameter(5, nameof(Button.Size), Sizes.Sm);
            builder.AddComponentParameter(6, nameof(Button.Variant), Variants.Primary);
            builder.AddAttribute(7, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, ItemAdded));
            builder.AddAttribute(8, "type", "button");
            builder.AddContent(9, "Add");
            builder.CloseComponent();
            builder.CloseElement();

            builder.CloseRegion();

            builder.CloseElement();
            builder.CloseElement();
        };
    }

    private void ItemAdded(MouseEventArgs e)
    {
    }

    private void OnChange(ChangeEventArgs e)
    {
        if (!FieldChanged.HasDelegate)
        {
            Field.Value.BooleanValue = e.Value?.ToString() == bool.TrueString;
        }
        else
        {
            FieldChanged.InvokeAsync(e.Value?.ToString());
        }
    }
}