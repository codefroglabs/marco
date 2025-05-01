namespace Marco.Forms.UI.Client.Components.Forms;

public record FormSection(string FormSectionId, string Name)
{
    public string Name { get; set; } = Name;
    public string? Description { get; set; }
    public List<FieldSchema> Fields { get; set; } = [];
}