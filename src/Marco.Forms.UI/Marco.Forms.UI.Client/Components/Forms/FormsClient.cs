namespace Marco.Forms.UI.Client.Components.Forms;

public class FormsClient
{
    public Task<ShareLink> GenerateShareLinkAsync(string formFormId)
    {
        return Task.FromResult(new ShareLink
        {
            FormId = formFormId,
            PublicLink = $"https://example.com/forms/{formFormId}/share"
        });
    }

    public Task<List<FormDto>> GetFormsAsync(CancellationToken ct)
    {
        List<FormDto> forms = [];
        return Task.FromResult(forms);
    }
}