using System.Text.Json.Serialization;

namespace Marco.Forms.UI.Client.Components.Primitives;

/// <summary>
///  Represents the HTML Selection Event object.
/// </summary>
public record Selection
{
    [JsonPropertyName("anchorNode")] public Node? AnchorNode { get; init; }
    [JsonPropertyName("anchorOffset")] public int AnchorOffset { get; init; }

    /// <summary>
    /// The type of selection: "None", "Caret" or "Range".
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The direction of the selection: "forward", "backward" or "none".
    /// </summary>
    public string? Direction { get; init; }

    public Node? FocusNode { get; init; }
    public int FocusOffset { get; init; }
    public bool IsCollapsed { get; init; }
    public int RangeCount { get; init; }
    public SelectionRange? Range { get; set; }
    public DomRect? AnchorRect { get; init; }
}
