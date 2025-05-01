namespace Marco.Forms.UI.Client.Models;

// [JsonConverter(typeof(BlockDataJsonConverter))]
public record SectionBlockData : BlockData
{
    public List<string> ChildBlockIds { get; set; } = [];
}
