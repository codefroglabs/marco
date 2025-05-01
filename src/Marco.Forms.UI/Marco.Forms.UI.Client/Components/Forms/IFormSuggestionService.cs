namespace Marco.Forms.UI.Client.Components.Forms;

public interface IFormSuggestionService
{
    // Task<List<string>> GetSuggestionsAsync(string fieldName);
    Task<FieldSchema?> GenerateFieldAsync(string fieldPrompt);
    Task<string?> GenerateFieldAsJsonAsync(string fieldPrompt);

}