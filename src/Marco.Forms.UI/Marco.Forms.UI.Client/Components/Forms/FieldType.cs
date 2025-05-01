namespace Marco.Forms.UI.Client.Components.Forms;

public enum FieldType
{
    /// <summary>
    /// Used for single line text input, such as names, short answers, etc.
    /// </summary>
    Text = 0,

    /// <summary>
    /// Used for multi-line text input, such as paragraphs, descriptions, etc.
    /// </summary>
    TextArea = 1,

    /// <summary>
    /// Used for selecting a specific date, such as a birthday, appointment date, etc.
    /// </summary>
    Date = 2,

    /// <summary>
    /// Used for selecting a specific time, such as an appointment time, etc.
    /// </summary>
    Time = 3,

    /// <summary>
    /// Used for selecting a specific date and time, such as an appointment date and time, creation time, etc.
    /// </summary>
    DateTime = 4,

    /// <summary>
    /// Used for selecting a specific number, such as age, quantity, etc.
    /// </summary>
    Select = 5,

    /// <summary>
    /// Used when multiple, independent selections are possible or for a simple on/off choice.
    /// Commonly seen in forms for multi-select options (e.g., selecting multiple hobbies or filters),
    /// toggling preferences (e.g., enabling/disabling notifications), or confirming acknowledgments (e.g.,
    /// agreeing to terms and conditions). Avoid using checkboxes when the number if you have a long list (e.g., 20+ items).
    /// </summary>
    Checkbox = 6,

    /// <summary>
    /// Used when a user needs to select one option from a list of options. Typically used when
    /// there are only a few options, making a select dropdown unnecessarily large.
    /// </summary>
    Radio = 7
}