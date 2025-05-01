namespace Marco.Forms.UI.Client.Components;

public record Spacing()
{
    public Spacing(double top, double right, double bottom, double left) : this()
    {
        Top = top;
        Right = right;
        Bottom = bottom;
        Left = left;
    }

    public Spacing(double all) : this()
    {
        Top = all;
        Right = all;
        Bottom = all;
        Left = all;
    }

    public double Top { get; set; } = 0;
    public double Right { get; set; } = 0;
    public double Bottom { get; set; } = 0;
    public double Left { get; set; } = 0;
    public CssUnit Unit { get; set; } = CssUnit.Rem;
}