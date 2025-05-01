using Microsoft.AspNetCore.Components.Web;

namespace Marco.Forms.UI.Client.Components.Shared;

public class DefaultErrorBoundary(ILogger<DefaultErrorBoundary> logger) : ErrorBoundary
{
    protected override Task OnErrorAsync(Exception ex)
    {
        logger.LogError(ex, "An error occurred in a component");
        return Task.CompletedTask;
    }
}
