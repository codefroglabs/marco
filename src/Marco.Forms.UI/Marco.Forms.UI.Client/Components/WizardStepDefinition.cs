using Microsoft.AspNetCore.Components;

namespace Marco.Forms.UI.Client.Components;

public record WizardStepDefinition
{
    public string? Title { get; set; }
    public RenderFragment? ChildContent { get; set; }
}
