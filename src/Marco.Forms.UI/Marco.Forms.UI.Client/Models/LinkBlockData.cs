namespace Marco.Forms.UI.Client.Models;

public record LinkBlockData : BlockData
{
    public string Url { get; set; } = "";
    public string Text { get; set; } = "";
}