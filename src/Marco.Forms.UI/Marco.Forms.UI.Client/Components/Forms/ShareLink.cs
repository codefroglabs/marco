namespace Marco.Forms.UI.Client.Components.Forms;

public record ShareLink
{
    public required string FormId { get; set; }
    public required string PublicLink { get; set; }
}