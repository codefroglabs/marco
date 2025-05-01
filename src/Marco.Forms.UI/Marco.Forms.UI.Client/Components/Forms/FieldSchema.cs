namespace Marco.Forms.UI.Client.Components.Forms;

public class FieldSchema
{
    public string? FormFieldId { get; set; }
    public string? Label { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public FieldType Type { get; set; }
    public bool Required { get; set; }
    public FieldValue Value { get; set; } = new();
    public List<FormFieldOption> Options { get; set; } = [];
}