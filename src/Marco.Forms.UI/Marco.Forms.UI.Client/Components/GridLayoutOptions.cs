namespace Marco.Forms.UI.Client.Components;

public record GridLayoutOptions
{
    /// <summary>
    ///     Use the grid-cols-* utilities to create grids with n equally sized columns.
    /// </summary>
    public int? TemplateColumns { get; set; }

    /// <summary>
    ///     Use the grid-rows-* utilities to create grids with n equally sized rows.
    /// </summary>
    public int? TemplateRows { get; set; }

    /// <summary>
    ///     Make an element span n rows.
    /// </summary>
    public int? RowSpan { get; set; }

    /// <summary>
    ///     Use the row-start-* and row-end-* utilities to make an element start or end at the nth grid line. These can also be
    ///     combined with the row-span-* utilities to span a specific number of rows.
    /// </summary>
    /// <remarks>
    ///     Note that CSS grid lines start at 1, not 0, so a full-height element in a 3-row grid would start at line 1 and end
    ///     at line 4.
    /// </remarks>
    public int? RowStart { get; set; }

    /// <summary>
    ///     Use the row-start-* and row-end-* utilities to make an element start or end at the nth grid line. These can also be
    ///     combined with the row-span-* utilities to span a specific number of rows.
    /// </summary>
    /// <remarks>
    ///     Note that CSS grid lines start at 1, not 0, so a full-height element in a 3-row grid would start at line 1 and end
    ///     at line 4.
    /// </remarks>
    public int? RowEnd { get; set; }

    /// <summary>
    ///     Make an element span n columns.
    /// </summary>
    public int? ColumnSpan { get; set; }

    /// <summary>
    ///     Use the col-start-* and col-end-* utilities to make an element start or end at the nth grid line. These can also be
    ///     combined with the col-span-* utilities to span a specific number of columns.
    /// </summary>
    /// <remarks>
    ///     Note that CSS grid lines start at 1, not 0, so a full-width element in a 6-column grid would start at line 1 and
    ///     end at line 7.
    /// </remarks>
    public int? ColumnStart { get; set; }

    /// <summary>
    ///     Use the col-start-* and col-end-* utilities to make an element start or end at the nth grid line. These can also be
    ///     combined with the col-span-* utilities to span a specific number of columns.
    /// </summary>
    /// <remarks>
    ///     Note that CSS grid lines start at 1, not 0, so a full-width element in a 6-column grid would start at line 1 and
    ///     end at line 7.
    /// </remarks>
    public int? ColumnEnd { get; set; }

    /// <summary>
    ///     Use the gap-x-* and gap-y-* utilities to change the gap between columns and rows independently.
    /// </summary>
    public int ColumnGap { get; set; }

    /// <summary>
    ///     Use the gap-x-* and gap-y-* utilities to change the gap between columns and rows independently.
    /// </summary>
    public int RowGap { get; set; }

    /// <summary>
    ///     Use the gap-* utilities to change the gap between both rows and columns in grid and flexbox layouts.
    /// </summary>
    public int Gap { get; set; }

    /// <summary>
    ///     Use the grid-flow-* utilities to control how the auto-placement algorithm works for a grid layout.
    /// </summary>
    public GridAutoFlow AutoFlow { get; set; }

    /// <summary>
    ///     Use the auto-cols-* utilities to control the size of implicitly-created grid columns.
    /// </summary>
    public GridAutoColumns AutoColumns { get; set; }

    /// <summary>
    ///     Use the auto-rows-* utilities to control the size of implicitly-created grid rows.
    /// </summary>
    public GridAutoRows AutoRows { get; set; }
}