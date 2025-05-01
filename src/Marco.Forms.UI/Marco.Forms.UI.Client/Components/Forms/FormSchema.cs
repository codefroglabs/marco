namespace Marco.Forms.UI.Client.Components.Forms;

public class FormSchema
{
    public string? Title { get; set; }
    public List<FieldSchema> Fields { get; set; } = new();
}