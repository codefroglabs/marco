namespace Marco.Forms.UI.Client.Components.Forms;

public class ServerFormAiClient
{
    public Task ListenForFormRequestAsync(Func<string?, string?, Task> onFormGenerated)
    {
        // implementation removed
        return Task.CompletedTask;
    }

    public Task GenerateFormAsync(string request)
    {
        // implementation removed
        return Task.CompletedTask;
    }
}