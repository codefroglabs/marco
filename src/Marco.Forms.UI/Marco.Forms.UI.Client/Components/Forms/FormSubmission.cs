namespace Marco.Forms.UI.Client.Components.Forms;

public record FormSubmission(string FormSubmissionId, string FormId, string UserId)
{
    /// <summary>
    /// Which version of the form was submitted.
    /// </summary>
    public int FormVersion { get; set; }

    public DateTime SubmittedDate { get; set; }
    public List<FormResponse> Responses { get; set; } = [];
}