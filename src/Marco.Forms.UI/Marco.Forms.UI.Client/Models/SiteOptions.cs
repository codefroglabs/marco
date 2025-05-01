namespace Marco.Forms.UI.Client.Models;

public record SiteOptions
{
    /// <summary>
    /// The endpoint for API operations
    /// </summary>
    public string ApiEndpoint { get; set; } = "https://localhost:7083";
}