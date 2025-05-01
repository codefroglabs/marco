namespace Marco.Forms.UI.Client.Components.Forms;

public record FormResponse(string FormResponseId, string FormSubmissionId, string FormFieldId)
{
    public FieldValue Value { get; set; } = new();
}