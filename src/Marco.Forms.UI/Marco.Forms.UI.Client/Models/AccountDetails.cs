namespace Marco.Forms.UI.Client.Models;

public record AccountDetails
{
    public required string AccountId { get; set; }
    public required int AccountStatus { get; set; }
}