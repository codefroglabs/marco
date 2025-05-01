using Marco.Forms.UI.Client.Components;

namespace Marco.Forms.UI.Client.Models;

public static class FormattingDefinitionExtensions
{
    // TODO: use inline styles for marco, but convert them to css classes on publish
    // public static string Test()
    // {
    //     // testing / demo code
    //     var css = new CSSObject
    //     {
    //         [".basic"] = new CSSObject
    //         {
    //             Width = 300,
    //             Height = 300,
    //             Border = "1px solid #DDD",
    //             ["& .title"] = new CSSObject
    //             {
    //                 LineHeight = 20,
    //                 Color = "red"
    //             },
    //             ["& .button"] = new CSSObject
    //             {
    //                 Width = "100%",
    //                 Height = "20px",
    //                 TextAlign = "center",
    //                 ["&:hover"] = new CSSObject
    //                 {
    //                     Color = "blue"
    //                 }
    //             }
    //         }
    //     }.ToString();

    //     return css;
    // }

    public static string ToCssClass(this BlockFormatting def)
    {
        var klass = "";

        if (!string.IsNullOrWhiteSpace(def.FontWeight))
        {
            klass += def.FontWeight + " ";
        }

        // if (!string.IsNullOrWhiteSpace(def.TextColor))
        // {
        //     klass += def.TextColor + " ";
        // }

        if (!string.IsNullOrWhiteSpace(def.FontFamily))
        {
            klass += def.FontFamily + " ";
        }

        if (!string.IsNullOrWhiteSpace(def.Tracking))
        {
            klass += def.Tracking + " ";
        }

        if (!string.IsNullOrWhiteSpace(def.BackgroundColor))
        {
            klass += def.BackgroundColor + " ";
        }

        // if (def.PlacementDefinition.X != 0 || def.PlacementDefinition.Y != 0)
        // {
        //     // if we have a placement, we need to set the position to relative
        //     klass += "relative ";
        // }

        if (def.TextAlign != TextAlign.Unset)
        {
            klass += def.TextAlign switch
            {
                TextAlign.Left => "text-left",
                TextAlign.Center => "text-center",
                TextAlign.Right => "text-right",
                _ => ""
            } + " ";
        }

        return klass.Trim();
    }

    public static string ToCssStyle(this BlockFormatting blockFormatting)
    {
        // if (formattingDefinition.Version < 1)
        // {
        //     return "";
        // }

        var fontSizeUnit = blockFormatting.FontSizeUnit.ToCssString();

        var css = "";
        // if FontSize is a number, we need to add it as a style
        // otherwise, it's a class and handled in ToCssClass
        if (blockFormatting.FontSizeV2 > 0)
        {
            css += $"font-size: {blockFormatting.FontSizeV2}{fontSizeUnit};";
        }

        if (blockFormatting.TextColor != null)
        {
            css += $"color: {blockFormatting.TextColor};";
        }

        // if (blockFormatting.PlacementDefinition.Width != 0)
        // {
        //     css += $"width: {blockFormatting.PlacementDefinition.Width}px;";
        // }
        //
        // if (blockFormatting.PlacementDefinition.Height != 0)
        // {
        //     css += $"height: {blockFormatting.PlacementDefinition.Height}px;";
        // }
        //
        // if (blockFormatting.PlacementDefinition.X != 0)
        // {
        //     css += $"left: {blockFormatting.PlacementDefinition.X}px;";
        // }
        //
        // if (blockFormatting.PlacementDefinition.Y != 0)
        // {
        //     css += $"top: {blockFormatting.PlacementDefinition.Y}px;";
        // }

        var spacingUnit = blockFormatting.Padding.Unit.ToCssString();
        if (blockFormatting.Padding.Bottom != 0)
        {
            css += $"padding-bottom: {blockFormatting.Padding.Bottom}{spacingUnit};";
        }

        if (blockFormatting.Padding.Left != 0)
        {
            css += $"padding-left: {blockFormatting.Padding.Left}{spacingUnit};";
        }

        if (blockFormatting.Padding.Right != 0)
        {
            css += $"padding-right: {blockFormatting.Padding.Right}{spacingUnit};";
        }

        if (blockFormatting.Padding.Top != 0)
        {
            css += $"padding-top: {blockFormatting.Padding.Top}{spacingUnit};";
        }

        if (blockFormatting.Margin.Bottom != 0)
        {
            css += $"margin-bottom: {blockFormatting.Margin.Bottom}{spacingUnit};";
        }

        if (blockFormatting.Margin.Left != 0)
        {
            css += $"margin-left: {blockFormatting.Margin.Left}{spacingUnit};";
        }

        if (blockFormatting.Margin.Right != 0)
        {
            css += $"margin-right: {blockFormatting.Margin.Right}{spacingUnit};";
        }

        if (blockFormatting.Margin.Top != 0)
        {
            css += $"margin-top: {blockFormatting.Margin.Top}{spacingUnit};";
        }

        if (blockFormatting.Border is not null)
        {
            var borderUnit = blockFormatting.Border.Unit.ToCssString();
            css += $"border-width: {blockFormatting.Border.Width}{borderUnit};";
            if (blockFormatting.Border.Color is not null)
            {
                css += $"border-color: {blockFormatting.Border.Color};";
            }

            if (blockFormatting.Border.Style is not null)
            {
                css += $"border-style: {blockFormatting.Border.Style};";
            }

            if (blockFormatting.Border.Radius is not null)
            {
                css += $"border-radius: {blockFormatting.Border.Radius};";
            }
        }

        return css;
    }
}