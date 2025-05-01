namespace Marco.Forms.UI.Client.Components.Forms;

public record FormField(string FormFieldId, string Name)
{
    public string Name { get; set; } = Name;
    public string? Description { get; set; }
    public FieldType Type { get; set; }
    public bool Required { get; set; }
    public FieldValue Value { get; set; } = new();
    public List<FormFieldOption> Options { get; set; } = [];
}