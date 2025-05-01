using Marco.Forms.UI.Client.Components.Primitives;

namespace Marco.Forms.UI.Client.Components;

public static class SelectionExtensions
{
    /// <summary>
    /// Returns position based on the editor cursor
    /// </summary>
    public static (double X, double Y) GetPosition(this Selection? selection)
    {
        return selection?.AnchorRect is null ? (0, 0) : (selection.AnchorRect.X, selection.AnchorRect.Y);
    }
}