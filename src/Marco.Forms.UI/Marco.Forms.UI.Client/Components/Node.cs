namespace Marco.Forms.UI.Client.Components;

public record Node
{
    public string? Id { get; set; }
    public NodeType NodeType { get; init; }
    public string? NodeName { get; init; }
    public List<Node>? ChildNodes { get; init; }
    public Node? ParentNode { get; init; }
    public string? InnerText { get; init; }
    public string? InnerHTML { get; init; }
}