using Marco.Forms.UI.Client.Components.Primitives;
using Microsoft.AspNetCore.Components.Web;

namespace Marco.Forms.UI.Client.Components;

public record ScreenPositioner : IPositioner
{
    public static ScreenPositioner New(MouseEventArgs args)
    {
        return new(args.GetPosition());
    }

    public static ScreenPositioner New(Selection selection)
    {
        return new(selection.GetPosition());
    }
    public ScreenPositioner((double x, double y)? position = null)
    {
        Position = position ?? (0, 0);
    }

    private (double X, double Y) Position { get; set; }

    public void Update((double x, double y) newPosition)
    {
        Position = newPosition;
    }

    public (double X, double Y) GetPosition()
    {
        return Position;
    }
}