using Marco.Forms.UI.Client.Components;

namespace Marco.Forms.UI.Client.Models;

public static class CssUnitExtensions
{
    public static string ToCssString(this CssUnit unit)
    {
        return unit switch
        {
            CssUnit.Px => "px",
            CssUnit.Em => "em",
            CssUnit.Rem => "rem",
            _ => "px"
        };
    }
}