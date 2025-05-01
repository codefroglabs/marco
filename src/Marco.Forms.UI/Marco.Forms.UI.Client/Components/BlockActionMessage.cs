using Marco.Forms.UI.Client.Components.Actions;

namespace Marco.Forms.UI.Client.Components;

public record BlockActionMessage
{
    public BlockActionMessage(BlockActions action, PageBlock block)
    {
        Action = action;
        Block = block;
    }

    public BlockActions Action { get; }
    public PageBlock Block { get; }
}
