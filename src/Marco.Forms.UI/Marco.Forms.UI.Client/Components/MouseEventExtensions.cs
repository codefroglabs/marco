using Microsoft.AspNetCore.Components.Web;

namespace Marco.Forms.UI.Client.Components;

public static class MouseEventExtensions
{
    public static (double X, double Y) GetPosition(this MouseEventArgs args)
    {
        return (args.ClientX, args.ClientY);
    }
}