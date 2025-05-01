using Marco.Forms.UI.Client.Components;

namespace Marco.Forms.UI.Client.Models;

public record BlockFormatting
{
    public TextAlign TextAlign { get; set; }
    public string? FontFamily { get; set; }
    public double FontSizeV2 { get; set; }
    public CssUnit FontSizeUnit { get; set; }
    public string? FontWeight { get; set; }
    public string? TextColor { get; set; }
    public string? BackgroundColor { get; set; }
    public string? Tracking { get; set; }
    public Spacing Padding { get; set; } = new();
    public Spacing Margin { get; set; } = new();
    public Border? Border { get; set; }
}