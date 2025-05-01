using Microsoft.AspNetCore.Components;

namespace Marco.Forms.UI.Client.Components.Blocks;

public interface IBlazorBlock
{
    /// <summary>
    ///     Optional. Builds the editor view for the block. This is the visual representation of the block when it is
    ///     being edited in Marco.
    /// </summary>
    public RenderFragment Editor() =>
        builder => { };

    /// <summary>
    ///     Optional. Editor activation occurs when the block is selected or focused. This is the point at which the
    ///     block should prepare for user interaction during editing.
    ///     For example, if your editor view contains an input element, you could focus the input element.
    /// </summary>
    public Task OnEditorActivatedAsync() => Task.CompletedTask;
}