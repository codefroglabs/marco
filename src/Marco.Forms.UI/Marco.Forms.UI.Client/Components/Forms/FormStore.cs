using System.Text.Json;
using Marco.Forms.UI.Client.Models;

namespace Marco.Forms.UI.Client.Components.Forms;

public class FormStore(FormsClient client)
{
    private List<FormDto> Forms { get; set; } = [];
    private List<FormSubmission> Submissions { get; set; } = [];

    public void RecordSubmission(FormSubmission submission)
    {
        Console.WriteLine(JsonSerializer.Serialize(submission));
        Submissions.Add(submission);
    }

    public Task<List<FormSubmission>> GetFormSubmissionsAsync(string formId)
    {
        return Task.FromResult(Submissions.Where(s => s.FormId == formId).ToList());
    }

    public async Task<FormDto?> GetFormAsync(string id, CancellationToken ct = default)
    {
        Forms = await client.GetFormsAsync(ct);

        return Forms.FirstOrDefault(p => p.FormId == id);
    }

    public async Task<List<FormDto>> GetFormsAsync(CancellationToken ct = default)
    {
        Forms = await client.GetFormsAsync(ct);

        return Forms;
    }

    public Task<FormDto> CreateFormAsync(FormDto form)
    {
        if (string.IsNullOrWhiteSpace(form.FormId))
        {
            form.FormId = Uuid.New();
        }

        if (Forms.Any(f => f.FormId == form.FormId))
        {
            throw new InvalidOperationException($"Form '{form.FormId}' already exists");
        }

        Forms.Add(form);
        Console.WriteLine(JsonSerializer.Serialize(form));
        return Task.FromResult(form);
    }

    public Task UpdateFormAsync(Form form)
    {
        var existingForm = Forms.FirstOrDefault(f => f.FormId == form.FormId);
        if (existingForm is null)
        {
            throw new InvalidOperationException($"Form '{form.FormId}' not found");
        }

        var updatedForm = existingForm with
        {
            Name = form.Name,
            Version = ++form.Version,
            Status = form.Status,
            ScheduledDate = form.ScheduledDate,
            StartedDate = form.StartedDate,
            CompletedDate = form.CompletedDate,
            Description = form.Description,
            Notes = form.Notes,
            Sections = form.Sections
        };
        Forms[Forms.IndexOf(existingForm)] = updatedForm;
        return Task.CompletedTask;
    }

    public async Task<string?> GeneratePublicLinkAsync(string formFormId)
    {
        var shareLink = await client.GenerateShareLinkAsync(formFormId);
        var form = Forms.FirstOrDefault(f => f.FormId == formFormId);
        if (form is null)
        {
            throw new InvalidOperationException($"Form '{formFormId}' not found");
        }

        form.LinkIdentifier = shareLink.PublicLink;
        return shareLink.PublicLink;
    }
}