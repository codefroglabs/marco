using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace Marco.Forms.UI.Client.Models;

public class ServerClient
{
    private readonly HttpClient client;
    private readonly ILogger<ServerClient> logger;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public ServerClient(HttpClient client, ILogger<ServerClient> logger, IOptions<SiteOptions> siteOptions)
    {
        this.client = client;
        this.logger = logger;
        client.BaseAddress = new Uri(siteOptions.Value.ApiEndpoint);
    }

    public async Task<AccountDetails?> GetAccountDetailsAsync(CancellationToken ct = default)
    {
        var response = await client.GetFromJsonAsync<AccountDetails>("/api/accounts/me", cancellationToken: ct);
        return response;
    }
}