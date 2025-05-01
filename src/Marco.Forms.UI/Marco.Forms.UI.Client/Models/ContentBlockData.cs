
namespace Marco.Forms.UI.Client.Models;

public record ContentBlockData : BlockData
{
    public string Tag { get; set; } = "p";
    public string Text { get; set; } = "";
}
