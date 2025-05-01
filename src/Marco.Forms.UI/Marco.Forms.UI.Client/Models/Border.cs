using Marco.Forms.UI.Client.Components;

namespace Marco.Forms.UI.Client.Models;

public record Border
{
    public double Width { get; set; }
    public string? Color { get; set; }
    public string? Style { get; set; }
    public string? Radius { get; set; }
    public CssUnit Unit { get; set; }
}