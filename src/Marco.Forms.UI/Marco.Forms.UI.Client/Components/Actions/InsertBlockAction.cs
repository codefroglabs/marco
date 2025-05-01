
using Marco.Forms.UI.Client.Models;

namespace Marco.Forms.UI.Client.Components.Actions;

public record InsertBlockAction(string BlockId) : BlockAction(BlockId)
{
    /// <summary>
    /// Whether to insert the block before or after the current block.
    ///
    /// By default, the block is inserted after the current block.
    /// </summary>
    public bool InsertBefore { get; set; }

    /// <summary>
    /// The block definition to insert.
    /// </summary>
    public required BlockDefinition BlockDefinition { get; set; }
}
