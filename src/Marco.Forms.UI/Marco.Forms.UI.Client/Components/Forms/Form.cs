namespace Marco.Forms.UI.Client.Components.Forms;

public record Form(string FormId, string Name)
{
    public string FormId { get; set; } = FormId;
    public string Name { get; set; } = Name;

    /// <summary>
    /// Tracks the version of the form, incremented each time the form is updated.
    ///
    /// This is useful for tracking changes to the form over time, and can be used to determine if a form submission is still valid - i.e. if the form has changed since the submission was made.
    /// </summary>
    public int Version { get; set; }

    public FormStatus Status { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public DateTime? StartedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }

    /// <summary>
    /// The unique link token that can be used to access the form.
    ///
    /// This is useful for sharing the form with others. This does not provide any security.
    /// </summary>
    public string? LinkIdentifier { get; set; }

    public string? PublicLink { get; set; }

    public string Completion { get; set; } = "";
    public string Updated { get; set; } = "";

    public HashSet<FormSection> Sections { get; set; } = [];
}