namespace Marco.Forms.UI.Client.Models;

public record ListBlockData : BlockData
{
    public ListType ListType { get; set; }
    public List<ListItem> Items { get; set; } = [];
}