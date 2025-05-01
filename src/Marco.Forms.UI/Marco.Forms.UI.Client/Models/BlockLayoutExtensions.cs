namespace Marco.Forms.UI.Client.Models;

public static class BlockLayoutExtensions
{
    public static string ToCssClass(this BlockLayout layout)
    {
        return $"{layout.GetCssClass()}"; //{layout.GridLayoutOptions.GetGridCssClass()
    }
    private static string GetCssClass(this BlockLayout layout)
    {
        return layout.LayoutType switch
        {
            BlockLayoutType.Grid => "grid",
            BlockLayoutType.Fixed => "fixed",
            _ => "grid"
        };
    }

    /// <summary>
    /// Returns the inline CSS style for the block layout
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public static string GetGridCssStyle(this GridLayoutOptions? options)
    {
        if (options == null)
        {
            return "grid-template-columns: repeat(12, 1fr);";
        }

        return options.TemplateColumns switch
        {
            1 => "grid-template-columns: repeat(1, 1fr);",
            2 => "grid-template-columns: repeat(2, 1fr);",
            3 => "grid-template-columns: repeat(3, 1fr);",
            4 => "grid-template-columns: repeat(4, 1fr);",
            5 => "grid-template-columns: repeat(5, 1fr);",
            6 => "grid-template-columns: repeat(6, 1fr);",
            7 => "grid-template-columns: repeat(7, 1fr);",
            8 => "grid-template-columns: repeat(8, 1fr);",
            9 => "grid-template-columns: repeat(9, 1fr);",
            10 => "grid-template-columns: repeat(10, 1fr);",
            11 => "grid-template-columns: repeat(11, 1fr);",
            12 => "grid-template-columns: repeat(12, 1fr);",
            _ => "grid-template-columns: repeat(1, 1fr);"
        };
    }

    private static string GetGridCssClass(this GridLayoutOptions? options)
    {
        if (options == null)
        {
            return "grid-cols-12";
        }

        return options.TemplateColumns switch
        {
            1 => "grid-cols-1",
            2 => "grid-cols-2",
            3 => "grid-cols-3",
            4 => "grid-cols-4",
            5 => "grid-cols-5",
            6 => "grid-cols-6",
            7 => "grid-cols-7",
            8 => "grid-cols-8",
            9 => "grid-cols-9",
            10 => "grid-cols-10",
            11 => "grid-cols-11",
            12 => "grid-cols-12",
            _ => "grid-cols-12"
        };
    }
}