using Marco.Forms.UI.Client.Components;

namespace Marco.Forms.UI.Client.Models;

public record FixedLayoutOptions
{
    /// <summary>
    ///     The width of the element.
    /// </summary>
    public double Width { get; set; }

    /// <summary>
    ///     The height of the element.
    /// </summary>
    public double Height { get; set; }

    /// <summary>
    ///     The minimum width of the element.
    /// </summary>
    public double? MinWidth { get; set; }

    /// <summary>
    ///     The minimum height of the element.
    /// </summary>
    public double? MinHeight { get; set; }

    /// <summary>
    ///     The maximum width of the element.
    /// </summary>
    public double? MaxWidth { get; set; }

    /// <summary>
    ///     The maximum height of the element.
    /// </summary>
    public double? MaxHeight { get; set; }

    /// <summary>
    ///     The x position of the element, relative to its parent.
    /// </summary>
    public double? X { get; set; }

    /// <summary>
    ///     The y position of the element, relative to its parent.
    /// </summary>
    public double? Y { get; set; }

    /// <summary>
    ///     The z-index of the element.
    /// </summary>
    public int? ZIndex { get; set; }

    /// <summary>
    ///     The CSS unit for the element's size properties.
    /// </summary>
    public CssUnit WidthUnit { get; set; }
}