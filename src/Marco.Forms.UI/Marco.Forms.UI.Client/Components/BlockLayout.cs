namespace Marco.Forms.UI.Client.Components;

public record BlockLayout
{
    public int Version { get; set; }
    public BlockLayoutType LayoutType { get; set; }
    public GridLayoutOptions? GridLayoutOptions { get; set; }
    public FixedLayoutOptions? FixedLayoutOptions { get; set; }
    public Spacing Padding { get; set; } = new();
    public Spacing Margin { get; set; } = new();
}