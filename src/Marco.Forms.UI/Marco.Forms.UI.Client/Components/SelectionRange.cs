namespace Marco.Forms.UI.Client.Components;

public record SelectionRange
{
    public int StartOffset { get; set; }
    public int EndOffset { get; set; }
    public bool Collapsed { get; set; }
    public Node? StartContainer { get; set; }
    public Node? EndContainer { get; set; }
}