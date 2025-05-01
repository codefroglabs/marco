using System.Text.Json.Serialization;
using Marco.Forms.UI.Client.Models;
using Pure.Blazor.Components;

namespace Marco.Forms.UI.Client.Components.Trees;

public class TreeViewItem
{
    public string Id { get; set; } = Uuid.New();
    public string? Name { get; set; }
    public int Level { get; set; }
    public TreeItemType Type { get; set; }
    public string? ParentId { get; set; }
    public string? ParentName { get; set; }
    public List<TreeViewItem> Children { get; set; } = [];
    public bool IsCollapsed { get; set; } = true;
    public bool IsSelected { get; set; } = false;
    public bool HasChildren => Type == TreeItemType.Folder && Children.Any();
    public bool PromptRename { get; set; } = false;
    public string Content { get; set; } = "";

    [JsonIgnore] public PureInput? Reference { get; set; }
}
