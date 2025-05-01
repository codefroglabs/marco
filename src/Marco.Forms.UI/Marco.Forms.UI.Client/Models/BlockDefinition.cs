namespace Marco.Forms.UI.Client.Models;

public record BlockDefinition
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Icon { get; set; }
}