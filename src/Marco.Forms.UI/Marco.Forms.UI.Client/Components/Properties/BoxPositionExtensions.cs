namespace Marco.Forms.UI.Client.Components.Properties;

public static class BoxPositionExtensions
{
    public static string GetTransformCSS(this BoxPosition anchor)
    {
        string css(int xOff, int yOff) => $"transform: translate({xOff}%, {yOff}%)";

        // By default; assuming no other stylings, css treats the top left as an anchor
        // To treat the bottom as the anchor, we use -100% y
        // To treat the right as the anchor, we use -100% x
        const int None = 0;
        const int Half = -50;
        const int Whole = -100;
        return anchor switch
        {
            BoxPosition.TopLeft => css(None, None),
            BoxPosition.MiddleLeft => css(None, Half),
            BoxPosition.BottomLeft => css(None, Whole),

            BoxPosition.TopCenter => css(Half, None),
            BoxPosition.MiddleCenter => css(Half, Half),
            BoxPosition.BottomCenter => css(Half, Whole),

            BoxPosition.TopRight => css(Whole, None),
            BoxPosition.MiddleRight => css(Whole, Half),
            BoxPosition.BottomRight => css(Whole, Whole),

            _ => css(None, None),
        };
    }

}