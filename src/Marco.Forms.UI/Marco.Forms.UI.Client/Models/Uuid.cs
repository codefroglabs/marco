using System.Diagnostics;

namespace Marco.Forms.UI.Client.Models;


[DebuggerDisplay("{ToString()}")]
public struct Uuid
{
    public string Value { get; set; }

    public override string ToString() => Value;

    public static string New() => Ulid.NewUlid().ToString();
}
