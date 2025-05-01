namespace Marco.Forms.UI.Client.Components.Forms;

/// <summary>
/// Union-like type for the different types of values a form field can have.
/// </summary>
public class FieldValue
{
    public FieldValueType ValueType { get; set; }
    public string? TextValue { get; set; }
    public HashSet<FieldValue>? MultiValue { get; set; }
    public DateTime? DateValue { get; set; }
    public decimal? NumberValue { get; set; }
    public bool? BooleanValue { get; set; }
}